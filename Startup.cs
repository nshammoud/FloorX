using KQF.Floor.NavServices.ProductionOrders;
using KQF.Floor.NavServices;
using KQF.Floor.Web.Auth;
using KQF.Floor.Web.Auth.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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

            services.AddScoped<IAuthenticationService, LdapAuthenticationService>();
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

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
                options.LoginPath = "/Account/Login";

            });

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

            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseHttpsRedirection();

            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Strict });

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
