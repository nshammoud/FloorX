using KQF.Floor.Web.Auth.Services;
using KQF.Floor.Web.Configuration;
using KQF.Floor.Web.Helpers;
using KQF.Floor.Web.Hubs;
using KQF.Floor.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Web.Controllers
{
    [Authorize]
    public class HomeController : ControllerBase
    {

        private readonly IOptions<LoginBaseSettingConfig> _config;
        private readonly Auth.Services.IAuthenticationService _auth;
        private readonly IHubContext<ConsumptionHub> _hub;

        public HomeController(ILogger<HomeController> logger, Auth.Services.IAuthenticationService authSvc, Auth.LocationProvider locationProvider,
            Microsoft.Extensions.Options.IOptions<UIConfiguration> uiConfig,
            Services.UserSettingsService userSettingsService,
            IHubContext<ConsumptionHub> hub, FeatureFlagProvider featureFlags, IOptions<LoginBaseSettingConfig> config) : base(logger, locationProvider, uiConfig, userSettingsService, featureFlags)
        {
            _auth = authSvc;
            _hub = hub;
            _config = config;
        }

        public IActionResult Index()
        {
            var tokenRespone = HttpContext.User.Claims.FirstOrDefault(c=>c.Type == "access_token_response")?.Value;
            if (!string.IsNullOrEmpty(tokenRespone))
            {
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(tokenRespone);
                if (tokenResponse != null)
                {
                    var companies = new BusinessCentralApiHelper(_config.Value).GetCompanyApiResponse(tokenResponse.Access_token);
                    return View(companies);
                }
            }
            return View();
        }

        [AllowAnonymous]
        public IActionResult callback(string code)
        {
            //var tokenRespone = HttpContext.Session.GetString("access_token_response");
            //HttpContext.Session.SetString("refresh_token", code);
            //if (string.IsNullOrEmpty(tokenRespone))
            //{
            //    var tokenResponse = BusinessCentralApiHelper.GetToken(code);
            //    var jsonResponse = JsonConvert.SerializeObject(tokenResponse);
            //    HttpContext.Session.SetString("access_token_response", jsonResponse);
            //}
            return RedirectToAction("index");
        }

        [Route("signin-oidc")]
        [AllowAnonymous]
        public IActionResult signin_oidc(string code)
        {
            //var tokenResponse = BusinessCentralApiHelper.GetTokenV2();
            return RedirectToAction("index");
        }

        [Route("signin-microsoft")]
        [AllowAnonymous]
        public IActionResult signin_microsoft(string code)
        {
            //var token = BusinessCentralApiHelper.GetToken(code);
            //var claims = HttpContext.User.Claims;
            return RedirectToAction("index");
        }

        //[Authorize("Supervisor")]
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Policy = "Administrator.Location")]
        [HttpPost("clearEoD")]
        public IActionResult ClearEoD()
        {
            _locationProvider.ClearCache();
            return Json(_locationProvider.LocationDayOffsetInSeconds);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> TestMsg(string m)
        {
            var s = m.Split(":").Select(x => x.ToLower()).ToArray();
            if (s[0] == "p" && s.Length > 2)
            {
                await _hub.Clients.All.SendAsync("JobPostedNotification", "test.user", s[1], s[2]);
            }
            else if (s[0] == "u" && s.Length > 2)
            {
                await _hub.Clients.All.SendAsync("JobUpdateNotification", "test.user", s[1], s[2]);

            }


            return Json(new { ok = true });
        }
    }
}
