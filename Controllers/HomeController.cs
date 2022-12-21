using KQF.Floor.Web.Auth.Services;
using KQF.Floor.Web.Configuration;
using KQF.Floor.Web.Hubs;
using KQF.Floor.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
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



        private readonly IAuthenticationService _auth;
        private readonly IHubContext<ConsumptionHub> _hub;

        public HomeController(ILogger<HomeController> logger, IAuthenticationService authSvc, Auth.LocationProvider locationProvider,
            Microsoft.Extensions.Options.IOptions<UIConfiguration> uiConfig,
            Services.UserSettingsService userSettingsService,
            IHubContext<ConsumptionHub> hub, FeatureFlagProvider featureFlags) : base(logger, locationProvider, uiConfig, userSettingsService, featureFlags)
        {
            _auth = authSvc;
            _hub = hub;

        }

        public IActionResult Index()
        {

            return View();
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
            return Json( _locationProvider.LocationDayOffsetInSeconds);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> TestMsg(string m)
        {
            var s = m.Split(":").Select(x => x.ToLower()).ToArray();
            if(s[0] == "p" && s.Length > 2)
            {
                await _hub.Clients.All.SendAsync("JobPostedNotification", "test.user", s[1], s[2]);
            }else if (s[0] == "u" && s.Length > 2)
            {
                await _hub.Clients.All.SendAsync("JobUpdateNotification", "test.user", s[1], s[2]);

            }


            return Json(new { ok = true });
        }
    }
}
