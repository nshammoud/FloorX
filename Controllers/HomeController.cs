using KQF.Floor.Web.Configuration;
using KQF.Floor.Web.Helpers;
using KQF.Floor.Web.Hubs;
using KQF.Floor.Web.Models;
using KQF.Floor.Web.Models.BusinessCentralApi_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
        private readonly IOptions<BusinessCentralApis> _businessApis;

        public HomeController(ILogger<HomeController> logger, Auth.Services.IAuthenticationService authSvc, Auth.LocationProvider locationProvider,
            IOptions<UIConfiguration> uiConfig,
            Services.UserSettingsService userSettingsService,
            IHubContext<ConsumptionHub> hub, FeatureFlagProvider featureFlags, IOptions<LoginBaseSettingConfig> config, IOptions<BusinessCentralApis> businessApis)
            : base(logger, locationProvider, uiConfig, userSettingsService, featureFlags)
        {
            _auth = authSvc;
            _hub = hub;
            _config = config;
            _businessApis = businessApis;
        }

        public IActionResult Index(string companyId)
        {
            if (!string.IsNullOrEmpty(companyId))
            {
                HttpContext.Session.SetString("CompanyID", companyId);
            }
            else
            {
                HttpContext.Session.SetString("CompanyID", _businessApis.Value.DefaultCompanyId);
            }
            return View();
        }

        [HttpGet]
        public IActionResult GetCompanies()
        {
            try
            {

                var sessionStoredCompanies = HttpContext.Session.GetString("CompaniesList");
                if (!string.IsNullOrEmpty(sessionStoredCompanies))
                {
                    var companies = JsonConvert.DeserializeObject<GenericResponse<Company_Detail>>(sessionStoredCompanies);
                    return PartialView("_CompaniesList", companies);
                }
                else
                {
                    var apiUrl = $"{_businessApis.Value.BaseUrl}{_businessApis.Value.Companies}";
                    var companies = new BusinessCentralApiHelper(_config.Value, HttpContext).GetApiResponse<GenericResponse<Company_Detail>>(apiUrl);
                    HttpContext.Session.SetString("CompaniesList", JsonConvert.SerializeObject(companies));
                    return PartialView("_CompaniesList", companies);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        public IActionResult Locations()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveLocationInSession(string locationCode)
        {
            HttpContext.Session.SetString("Location_Code", locationCode);
            return Json(true);
        }

        [HttpGet]
        public IActionResult GetLocationsByCompanyId(string companyId, bool jsonOnly = false)
        {
            var sessionStoredCompanies = HttpContext.Session.GetString("LocationsList_" + companyId);
            var locations = new GenericResponse<Location>();

            if (!string.IsNullOrEmpty(sessionStoredCompanies))
            {
                locations = JsonConvert.DeserializeObject<GenericResponse<Location>>(sessionStoredCompanies);
            }
            else
            {
                var locationApi = string.Format(_businessApis.Value.Location, companyId);
                var apiUrl = $"{_businessApis.Value.BaseUrl}{locationApi}";
                locations = new BusinessCentralApiHelper(_config.Value, HttpContext).GetApiResponse<GenericResponse<Location>>(apiUrl);

                HttpContext.Session.SetString($"LocationsList_{companyId}", JsonConvert.SerializeObject(locations));
            }
            
            if (jsonOnly)
            {
                return Json(locations);
            }
            else
            {
                return PartialView("_LocationList", locations);
            }
        }

        [AllowAnonymous]
        public IActionResult callback(string code)
        {
            return RedirectToAction("index");
        }

        [Route("signin-oidc")]
        [AllowAnonymous]
        public IActionResult signin_oidc(string code)
        {
            return RedirectToAction("index");
        }

        [Route("signin-microsoft")]
        [AllowAnonymous]
        public IActionResult signin_microsoft(string code)
        {
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
