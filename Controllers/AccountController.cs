using KQF.Floor.Web.Auth.Services;
using KQF.Floor.Web.Configuration;
using KQF.Floor.Web.Helpers;
using KQF.Floor.Web.Models.BusinessCentralApi_Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IAuthenticationService = KQF.Floor.Web.Auth.Services.IAuthenticationService;

namespace KQF.Floor.Web.Controllers
{
    public class AccountController : ControllerBase
    {

        private readonly IAuthenticationService _userService;
        private readonly IServiceProvider _serviceProvider;


        public AccountController(ILogger<AccountController> logger,
            IAuthenticationService authSvc,
            Auth.LocationProvider location,
            Microsoft.Extensions.Options.IOptions<UIConfiguration> uiConfig,
            Services.UserSettingsService userSettingsService,
            IServiceProvider provider, FeatureFlagProvider featureFlags) : base(logger, location, uiConfig, userSettingsService, featureFlags)
        {

            _userService = authSvc;
            _serviceProvider = provider;
        }
        
        [HttpGet]
        public async Task Login(string returnUrl)
        {
            returnUrl = "/signin-microsoft";
            await HttpContext.ChallengeAsync("Microsoft", new AuthenticationProperties() { RedirectUri = returnUrl });
            //return View();
        }



        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            _logger.LogInformation("Starting login for " + userName);
            try
            {
                var user = await _userService.Login(userName, password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid username or password");
                    return View();
                }

                if (!(user.LocationAccess?.Any() ?? false))
                {
                    ModelState.AddModelError("", "Your account does not have access to this application. Please contact an admin.");
                    return View();
                }

                _logger.LogInformation("Adding claims for " + userName);
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, user.DisplayName));
                identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName));
                identity.AddClaim(new Claim(ClaimTypes.Surname, user.LastName));
                identity.AddClaim(new Claim(ClaimTypes.WindowsAccountName, user.UserName));
                identity.AddClaim(new Claim(KQFClaimTypes.CredentialsKey, Newtonsoft.Json.JsonConvert.SerializeObject(user.Credentials)));

                foreach (var whe in user.WarehouseEmployeeRecords)
                {
                    identity.AddClaim(new Claim(KQFClaimTypes.WarehouseEmployeeRecord, Newtonsoft.Json.JsonConvert.SerializeObject(whe)));
                }

                foreach (var role in user.LocationAccess)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role.ToString().ToLower()));
                    identity.AddClaim(new Claim($"access.{role.Location.ToLower()}", role.Role.ToLower()));
                }

                var locations = user.LocationAccess.Select(x => x.Location).Distinct().ToArray();
                foreach (var location in locations)
                {
                    identity.AddClaim(new Claim("location", location));
                }

                this._locationProvider.Locations = locations; // okay then we need to call .. 
                // ....
                this._locationProvider.CurrentLocation = locations.FirstOrDefault();

                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                _logger.LogInformation("Redirecting " + userName);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //return Redirect("https://login.microsoftonline.com/common/oauth2/v2.0/logout");
            return RedirectToAction(nameof(Login));
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize]
        public ActionResult SwitchTo(string location, string redirect)
        {
            if (_locationProvider.Locations.Select(x => x.ToLower()).Contains(location.ToLower()))
            {
                _locationProvider.CurrentLocation = location;
            }

            return Redirect(redirect);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePrinter(string printer)
        {
            var r = await base._userSettingsService.SetCurrentPrinter(printer);
            SetContextPrinterData(true, false);
            return Json(new { success = r });
        }
    }
}
