using KQF.Floor.NavServices.ProductionOrders;
using KQF.Floor.NavServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Security.Claims;
using KQF.Floor.Web.NavServices;
using KQF.Floor.Web.NavServices.ProductionMgmt;
using KQF.Floor.Web.NavServices.WarehouseEmployees;
using KQF.Floor.Web.NavServices.WCResources;
using KQF.Floor.Web.NavServices.Components;
using Microsoft.EntityFrameworkCore;
using KQF.Floor.Web.NavServices.ItemSubstitution;
using KQF.Floor.Web.Hubs;
using KQF.Floor.Web.NavServices.ItemLotList;
using KQF.Floor.Web.NavServices.WarehouseEmployeesConsumption;
using KQF.Floor.Web.Configuration;
using KQF.Floor.Web.NavServices.PostedConsumption;
using KQF.Floor.Web.NavServices.Lookup;
using KQF.Floor.Web.NavServices.LabelPrinter;
using KQF.Floor.Web.NavServices.LocationList;
using KQF.Floor.Web.NavServices.WarehouseEmployeesPage;
using KQF.Floor.Web.NavServices.MoveContainerMgm;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using KQF.Floor.Web.Helpers;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using KQF.Floor.Web.Models.BusinessCentralApi_Models;

namespace KQF.Floor.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(28800);
                // options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddControllersWithViews();
            services.Configure<Auth.LDAPConfig>(Configuration.GetSection("Ldap"));
            services.Configure<NavServiceConfig>(Configuration.GetSection("Nav"));
            services.Configure<ConsumptionConfiguration>(Configuration.GetSection("Consumption"));
            services.Configure<UIConfiguration>(Configuration.GetSection("UI"));
            services.Configure<FeatureFlagConfiguration>(Configuration.GetSection("FeatureFlags"));
            services.Configure<LoginBaseSettingConfig>(Configuration.GetSection("LoginBaseConfig"));
            services.Configure<BusinessCentralApis>(Configuration.GetSection("BusinessCentralApis"));

            services.AddScoped<Auth.Services.IAuthenticationService, Auth.Services.LdapAuthenticationService>();
            services.AddTransient<IAuthorizationHandler, Auth.ClaimsHandling.LocationClaimsHandler>();
            services.AddTransient<Auth.LocationProvider>();
            services.AddTransient<ItemNumberClient>();
            services.AddTransient<FeatureFlagProvider>();

            services.AddHttpContextAccessor();

            services.AddTransient<ClaimsPrincipal>(s =>
           {
               return s.GetRequiredService<Microsoft.AspNetCore.Http.IHttpContextAccessor>().HttpContext.User;
           });

            // NAV CLIENTS
            services.AddTransient<ProductionOrderClient>();
            services.AddTransient<ProductionMgmtClient>();
            services.AddTransient<WarehouseEmployeesClient>();
            services.AddTransient<WarehouseEmployeesConsumptionClient>();
            services.AddTransient<WCResourcesClient>();
            services.AddTransient<ComponentsClient>();
            services.AddTransient<ItemSubstitutionListClient>();
            services.AddTransient<ItemLotListClient>();
            services.AddTransient<PostedConsumptionClient>();
            services.AddTransient<LookupClient>();
            services.AddTransient<LabelPrinterClient>();
            services.AddTransient<LocationListClient>();
            services.AddTransient<WarehouseEmployeePageClient>();
            services.AddTransient<MoveContainerMgmClient>();
            services.AddTransient<Services.UserSettingsService>();


            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                //options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
                //options.LoginPath = "/Account/Login";
            }).AddOpenIdConnect("Microsoft", options =>
            {
                options.Authority = Configuration["LoginBaseConfig:BaseUrl"]  + Configuration["LoginBaseConfig:Tenant_id"];
                options.ClientId = Configuration["LoginBaseConfig:Client_id"];
                options.ClientSecret = Configuration["LoginBaseConfig:Client_secret"];
                options.ResponseType = OpenIdConnectResponseType.Code;
                options.Scope.Clear();
                options.Scope.Add("https://api.businesscentral.dynamics.com/.default");
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.Events = new OpenIdConnectEvents
                {
                    OnTokenValidated =  async context =>
                    {
                        context.HandleResponse();
                        var refresh_token = context.TokenEndpointResponse.RefreshToken;
                        if (!context.HttpContext.User.Claims.Any())
                        {
                            //var jwt = new JwtSecurityTokenHandler().ReadJwtToken(context.TokenEndpointResponse.IdToken);
                            var claims = context.SecurityToken.Claims;
                            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                            foreach (var claim in claims)
                            {
                                if (claim.Type == "name")
                                {
                                    identity.AddClaim(new Claim(ClaimTypes.Name, claim.Value));
                                }
                                else
                                {
                                    identity.AddClaim(claim);
                                }
                            }

                            identity.AddClaim(new Claim("refresh_token", refresh_token));
                            var settings = Configuration.GetSection("LoginBaseConfig").Get<LoginBaseSettingConfig>();
                            var tokenResponse = new BusinessCentralApiHelper(settings).GetToken(refresh_token);
                            if(tokenResponse != null)
                            {
                                var jsonResponse = JsonConvert.SerializeObject(tokenResponse);
                                identity.AddClaim(new Claim("access_token_response", jsonResponse));
                            }

                            var principal = new ClaimsPrincipal(identity);
                            await context.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                        }
                        context.Response.Redirect($"/home");
                        //return Task.CompletedTask;
                    }
                };
            });

            // tested but have issues so changed into ODIC....
            //.AddMicrosoftAccount(options =>
            //{
            //    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.ClientId = Configuration["client_id"];
            //    options.ClientSecret = Configuration["client_secret"];
            //    options.AuthorizationEndpoint = $"https://login.microsoftonline.com/{Configuration["tenant_id"]}/oauth2/v2.0/authorize";
            //    options.TokenEndpoint = $"https://login.microsoftonline.com/{Configuration["tenant_id"]}/oauth2/v2.0/token";
            //    options.SaveTokens = true;
            //    options.UsePkce = true;
            //    options.Events = new Microsoft.AspNetCore.Authentication.OAuth.OAuthEvents
            //    {
            //        OnCreatingTicket = async context =>
            //        {
            //            var token = context.TokenResponse.AccessToken;
            //            var codke = context.HttpContext.Request.Query["code"].ToString();
            //            BusinessCentralApiHelper.GetToken(context.AccessToken);
            //            //BusinessCentralApiHelper.GetCompanyApiResponse(token);

            //            var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
            //            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

            //            var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
            //            response.EnsureSuccessStatusCode();

            //            var contents = await response.Content.ReadAsStringAsync();
            //            var user = JsonDocument.Parse(contents);
            //            context.RunClaimActions(user.RootElement);
            //        }
            //    };
            //    options.CallbackPath = new Microsoft.AspNetCore.Http.PathString("/signin-microsoft");
            //});

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Supervisor.Location", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new Auth.ClaimsHandling.LocationClaimRoleRequirement("Supervisor"));
                });

                options.AddPolicy("Employee.Location", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new Auth.ClaimsHandling.LocationClaimRoleRequirement("Employee"));
                });

                options.AddPolicy("Administrator.Location", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new Auth.ClaimsHandling.LocationClaimRoleRequirement("Administrator"));
                });

                options.AddPolicy("Supervisor", policy => policy.RequireClaim(ClaimTypes.Role, "Supervisor", "supervisor"));
                options.AddPolicy("Employee", policy => policy.RequireClaim(ClaimTypes.Role, "Employee", "employee"));
                options.AddPolicy("Administrator", policy => policy.RequireClaim(ClaimTypes.Role, "Administrator", "administrator"));
            });


            services.AddControllersWithViews(x => x.SuppressAsyncSuffixInActionNames = false).AddRazorRuntimeCompilation();
            services.AddAutoMapper(GetType().Assembly);
            services.AddSignalR();

            services.AddDbContext<Floor.Data.FloorDataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //    app.UseHsts();
            }

            app.UseSession();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();
            //app.UseCookiePolicy(new CookiePolicyOptions
            //{
            //    MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Strict
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHub<ConsumptionHub>("/consumptionhub");
            });


        }
    }
}
