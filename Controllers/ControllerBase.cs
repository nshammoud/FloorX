using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using KQF.Floor.Web.Configuration;
using Microsoft.Extensions.Options;

namespace KQF.Floor.Web.Controllers
{
    public class ControllerBase : Controller
    {
        protected readonly ILogger<ControllerBase> _logger;
        protected readonly Auth.LocationProvider _locationProvider;
        protected readonly UIConfiguration _uiConfig;
        protected readonly FeatureFlagProvider _featureFlags;
        protected readonly Services.UserSettingsService _userSettingsService;


        public ControllerBase(
            ILogger<ControllerBase> logger, 
            Auth.LocationProvider locationProvider, 
            IOptions<UIConfiguration> uiConfig, 
            Services.UserSettingsService userSettingsService,
            FeatureFlagProvider featureFlags)
        {
            _logger = logger;
            _locationProvider = locationProvider;
            _uiConfig = uiConfig.Value;
            _featureFlags = featureFlags;
            _userSettingsService = userSettingsService; 
        }

        protected string CurrentLocation
        {
            get
            {
                return _locationProvider.CurrentLocation;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewData["AppDisplayName"] = _uiConfig.AppDisplayName;
            ViewData["ViewportScale"] = _uiConfig.ViewportScale;
            ViewData["ViewportWidth"] = _uiConfig.ViewportWidth;
            

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                if (context.RouteData.Values["controller"] as String != "Account")
                {

                    //Make sure the session didn't expire and the user credentials match

                    var credentials = User.GetCredentials();
                    if (credentials == null || credentials.Username != User.UserName())
                    {
                        context.Result = new RedirectToRouteResult(new RouteValueDictionary(
                            new
                            {
                                controller = "Account",
                                action = "Logout"
                            }));
                        return;
                    }
                }
                var locationClaim = User.Claims.FirstOrDefault(x => x.Type.ToLower() == $"access.{_locationProvider.CurrentLocation}".ToLower() && x.Value.ToLower() == "supervisor");


                ViewData["IsSupervisor"] = locationClaim == null ? false : true;
                ViewData["CurrentLocation"] = _locationProvider.CurrentLocation;
                ViewData["Locations"] = _locationProvider.Locations;
                ViewData["EnabledFlags"] = _featureFlags.GetAllEnabledFlags(context.HttpContext.User);
                ViewData["EoD"] = _locationProvider.LocationDayOffsetInSeconds;

                SetContextPrinterData(false, false);
      
            }
            base.OnActionExecuting(context);
        }

        protected void SetContextPrinterData(bool refreshCurrentPrinter, bool refreshPrinterList)
        {
             
                var printerKey = $"{_locationProvider.CurrentLocation}-ASNPRINTERS";
                var currentPrinters = HttpContext.Session.GetString(printerKey);
                if (string.IsNullOrEmpty(currentPrinters) || refreshPrinterList)
                {
                    var printers = _userSettingsService.GetPrinters().GetAwaiter().GetResult();
                    var strPrinters = String.Join(',', printers);
                    HttpContext.Session.SetString(printerKey, strPrinters);
                    ViewData["Printers"] = printers;
                }
                else
                {
                    ViewData["Printers"] = currentPrinters.Split(',', StringSplitOptions.RemoveEmptyEntries);
                }

                var currentPrinterKey = $"{_locationProvider.CurrentLocation}-CurrentPrinter";
                var currentPrinter = HttpContext.Session.GetString(currentPrinterKey);
                if (string.IsNullOrEmpty(currentPrinter) || refreshCurrentPrinter)
                {
                    var printer = _userSettingsService.GetCurrentPrinter().GetAwaiter().GetResult();
                    printer = String.IsNullOrEmpty(printer) ? "Not Set" : printer;

                    HttpContext.Session.SetString(currentPrinterKey, printer);
                    ViewData["CurrentPrinter"] = printer;
                }
                else
                {
                    ViewData["CurrentPrinter"] = currentPrinter;
                }
             
        }

    }
}
