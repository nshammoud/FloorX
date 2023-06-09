﻿using AutoMapper;
using KQF.Floor.Web.Configuration;
using KQF.Floor.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using KQF.Floor.Web.Models.BusinessCentralApi_Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using KQF.Floor.Web.Helpers;

namespace KQF.Floor.Web.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class MaterialManagementController : ControllerBase
    {
        private readonly NavServices.ProductionMgmt.ProductionMgmtClient _prodMgmtClient;
        private readonly NavServices.MoveContainerMgm.MoveContainerMgmClient _moveContainerMgmClient;
        private readonly IOptions<BusinessCentralApis> _businessApis;
        private readonly IOptions<LoginBaseSettingConfig> _config;

        public MaterialManagementController(
          KQF.Floor.Data.FloorDataContext context,
        Microsoft.Extensions.Options.IOptions<UIConfiguration> uiConfig,
        NavServices.ProductionMgmt.ProductionMgmtClient prodMgmtClient,
        NavServices.MoveContainerMgm.MoveContainerMgmClient moveContainerMgmClient,
        IMapper mapper,
        ILogger<LookupController> logger,
        Services.UserSettingsService userSettingsService,
        Auth.LocationProvider locationProvider, FeatureFlagProvider featureFlags, 
        IOptions<LoginBaseSettingConfig> config, IOptions<BusinessCentralApis> businessApis) : base(logger, locationProvider, uiConfig, userSettingsService, featureFlags) {
            _prodMgmtClient = prodMgmtClient;
            _moveContainerMgmClient = moveContainerMgmClient;
            _config = config;
            _businessApis = businessApis;
        }

        public IActionResult Index()
        {
            // NSH Commented out code until refactor locations...
            //if(!_featureFlags.FlagEnabled("material-mgmt", HttpContext.User))
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            return View();
        }

        [HttpGet("Singling")]
        public IActionResult Singling(string source, string destination)
        {
            // NSH Commented out code until refactor locations...
            //if (!_featureFlags.FlagEnabled("material-mgmt", HttpContext.User))
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            
            return View(new SplitPostModel() { Source = source, Destination = destination, Weight =0m, NoOfRods = 0 });
        }

        [HttpPost("Singling")]
        public async Task<IActionResult> Singling(SplitPostModel model)
        {
            try
            {
                // NSH Commented out code for refactor ...
                //var result = await _prodMgmtClient.SplitCNTAsync(model.Source, model.Destination, model.NoOfRods, model.Weight);
                var companyId = HttpContext.Session.GetString("CompanyID");
                var apiUrl = $"{_businessApis.Value.BaseUrl}{_businessApis.Value.productionManagement}";
                var binaApiUrl = string.Format(apiUrl, companyId, "Microsoft.NAV.splitCart");
                var obj = new
                {
                    pCartNo = model.Source,
                    pNewCartNo = model.Destination,
                    pNumberOfROD = model.NoOfRods,
                    pGrossWeight = model.Weight
                };

                var results = new BusinessCentralApiHelper(_config.Value, HttpContext).PostApiResponse<GenericResponse2<string>>(binaApiUrl, obj);
                model.IsSuccess = true;
                model.Message = $"Posted {model.Weight}lbs / {model.NoOfRods} rods from {model.Source} to {model.Destination}";
            }catch(Exception ex)
            {
                model.IsSuccess = false;
                model.Message = ex.Message;
                model.Meta = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace };
            }
            
            return View(model);
        }


        [HttpGet("Doubling")]
        public IActionResult Doubling(string source, string destination)
        {
            // << NSH Commented out code for refactor ...
            //if (!_featureFlags.FlagEnabled("material-mgmt", HttpContext.User))
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            // NSH >> Commented out code for refactor ...

            return View(new MaterialPostModel() { Source = source, Destination = destination });
        }

        [HttpPost("Doubling")]
        public async Task<IActionResult> Doubling(MaterialPostModel model)
        {
            // << NSH  Commented out code for refactor ...
            //var req = new NavServices.ProductionMgmt.MergeCART()
            //{
            //    pCartNo = model.Source,
            //    pMsg = "",
            //    pNewCartNo = model.Destination
            //};
            // NSH >>
            try
            {
                var companyId = HttpContext.Session.GetString("CompanyID");
                var apiUrl = $"{_businessApis.Value.BaseUrl}{_businessApis.Value.productionManagement}";
                var binaApiUrl = string.Format(apiUrl, companyId, "Microsoft.NAV.mergeCarts");
                var obj = new
                {
                    pCartNo = model.Source,
                    pNewCartNo = model.Destination
                };

                var results = new BusinessCentralApiHelper(_config.Value, HttpContext).PostApiResponse<GenericResponse2<string>>(binaApiUrl, obj);
                //var result = await _prodMgmtClient.MergeCARTAsync(req);  // NSH >>
                model.Message = results.ToString();
                model.IsSuccess = true;
            }catch(Exception ex)
            {
                model.IsSuccess = false;
                model.Message = ex.Message;
                model.Meta = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace };
            }
           

            return View(model);
        }

        [HttpGet("Container")]
        public IActionResult Container(string ctr)
        {
            // << NSH   Commented out code for refactor ...
            //if (!_featureFlags.FlagEnabled("material-mgmt", HttpContext.User))
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            //  NSH >>   Commented out code for refactor ...

            return View(new MaterialPostModel() { Source = ctr ?? "" });
        }

        [HttpPost("Container")]
        public async Task<IActionResult> Container(MaterialPostModel model)
        {
            try
            {
                //var result = await _prodMgmtClient.MoveCNTAsync(model.Source, model.Destination); //NSH >>
                var companyId = HttpContext.Session.GetString("CompanyID");
                var apiUrl = $"{_businessApis.Value.BaseUrl}{_businessApis.Value.productionManagement}";
                var binaApiUrl = string.Format(apiUrl, companyId, "Microsoft.NAV.mergeCarts");
                var obj = new
                {
                    pCartNo = model.Source,
                    pNewCartNo = model.Destination
                };
                var results = new BusinessCentralApiHelper(_config.Value, HttpContext).PostApiResponse<GenericResponse2<string>>(binaApiUrl, obj);
                model.Message = $"Moved contents from {model.Source} to {model.Destination}";
                model.IsSuccess =true;
            }catch(Exception ex)
            {
                model.IsSuccess = false;
                model.Message = ex.Message;
                model.Meta = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace };
            }

            return View(model);
        }

        [HttpGet("MoveBetweenBins")]
        public IActionResult MoveBetweenBins(string bin, string item, string lot, decimal? qty)
        {
            // << NSH  Commented out code for refactor ...
            //if (!_featureFlags.FlagEnabled("material-mgmt", HttpContext.User))
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            // NSH >>  Commented out code for refactor ...

            return View(new MoveItemPostModel() { Source = bin, ItemNo = item, LotNo = lot, Qty = qty ?? 0.0m });
        }

        [HttpPost("MoveBetweenBins")]
        public async Task<IActionResult> MoveBetweenBins(MoveItemPostModel model)
        {
            try
            {
                //var result = await _moveContainerMgmClient.MoveBetweenBinsAsync(_locationProvider.CurrentLocation, model.ItemNo,
                //    model.LotNo, model.Source, model.Qty, model.Destination); // NSH >>
                var currentLocation = HttpContext.Session.GetString("Location_Code");
                var companyId = HttpContext.Session.GetString("CompanyID");
                var apiUrl = $"{_businessApis.Value.BaseUrl}{_businessApis.Value.packageManagement}";
                var binaApiUrl = string.Format(apiUrl, companyId, "Microsoft.NAV.moveBetweenBins");
                var obj = new
                {
                    pLocationCode = model.Source,
                    pItemNo = model.ItemNo,
                    pLotNo = model.LotNo,
                    pSourceBin = model.Source,
                    pQty = model.Qty,
                    pDestinationBin = model.Destination
                };
                var result = new BusinessCentralApiHelper(_config.Value, HttpContext).PostApiResponse<GenericResponse2<string>>(binaApiUrl, obj);
                model.Message = $"Moved {model.Qty} items with item number {model.ItemNo} from lot number {model.LotNo} from {model.Source} to {model.Destination}";
                model.IsSuccess = true;
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Message = ex.Message;
                model.Meta = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace };
            }

            return View(model);
        }

        [HttpGet("MoveItemsToContainer")]
        public IActionResult MoveItemsToContainer(string bin, string item, string lot, decimal? qty)
        {
            // << NSH   Commented out code for refactor ...
            //if (!_featureFlags.FlagEnabled("material-mgmt", HttpContext.User))
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            // NSH >>  Commented out code for refactor ...

            return View(new MoveItemPostModel() { Source = bin, ItemNo = item, LotNo = lot, Qty = qty ?? 0.0m });
        }

        [HttpPost("MoveItemsToContainer")]
        public async Task<IActionResult> MoveItemsToContainer(MoveItemPostModel model)
        {
            try
            {
                //var result = await _moveContainerMgmClient.MoveItemsToContainerAsync(_locationProvider.CurrentLocation, model.ItemNo,
                //    model.LotNo, model.Source, model.Qty, model.Destination); // NSH
                var currentLocation = HttpContext.Session.GetString("Location_Code");
                var companyId = HttpContext.Session.GetString("CompanyID");
                var apiUrl = $"{_businessApis.Value.BaseUrl}{_businessApis.Value.packageManagement}";
                var binaApiUrl = string.Format(apiUrl, companyId, "Microsoft.NAV.moveItemsToPackage");
                var obj = new
                {
                    pLocationCode = model.Source,
                    pItemNo = model.ItemNo,
                    pLotNo = model.LotNo,
                    pSourceBin = model.Source,
                    pQty = model.Qty,
                    pPackageNo = model.Destination
                };
                var result = new BusinessCentralApiHelper(_config.Value, HttpContext).PostApiResponse<GenericResponse2<string>>(binaApiUrl, obj);
                model.Message = $"Moved {model.Qty} items with item number {model.ItemNo} from lot number {model.LotNo} from {model.Source} to {model.Destination}";
                model.IsSuccess = true;
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Message = ex.Message;
                model.Meta = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace };
            }

            return View(model);
        }

        [HttpGet("MoveItemsBetweenContainers")]
        public IActionResult MoveItemsBetweenContainers(string item, string ctr, string lot, decimal? qty)
        {
            // << NSH   Commented out code for refactor ...
            //if (!_featureFlags.FlagEnabled("material-mgmt", HttpContext.User))
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            // NSH >> Commented out code for refactor ...

            return View(new MoveBetweenPostModel() { Source = ctr, ItemNo = item, LotNo = lot, Qty = qty ?? 0.0m, MovePartial = true });
        }

        [HttpPost("MoveItemsBetweenContainers")]
        public async Task<IActionResult> MoveItemsBetweenContainers(MoveBetweenPostModel model)
        {
            if (!model.MovePartial)
            {
                model.ItemNo = string.Empty;
                model.LotNo = string.Empty;
                model.Qty = 0.0m;
            }
            
            try
            {
                //var result = await _moveContainerMgmClient.MoveItemsBetweenContainersAsync(_locationProvider.CurrentLocation, model.ItemNo,
                //    model.LotNo, model.Qty, string.Empty, model.Source, model.Destination, model.MovePartial); // NSH
                var currentLocation = HttpContext.Session.GetString("Location_Code");
                var companyId = HttpContext.Session.GetString("CompanyID");
                var apiUrl = $"{_businessApis.Value.BaseUrl}{_businessApis.Value.packageManagement}";
                var binaApiUrl = string.Format(apiUrl, companyId, "Microsoft.NAV.moveItemsBetweenPackages");
                var obj = new
                {
                    pLocationCode = model.Source,
                    pItemNo = model.ItemNo,
                    pLotNo = model.LotNo,
                    pQty = model.Qty,
                    pSourcePackageNo = model.Source,
                    pDestPackageNo = model.Destination,
                    pMovePartial = model.MovePartial,
                };
                var result = new BusinessCentralApiHelper(_config.Value, HttpContext).PostApiResponse<GenericResponse2<string>>(binaApiUrl, obj);
                model.Message = model.MovePartial 
                    ? $"Moved {model.Qty} items with item number {model.ItemNo} from lot number {model.LotNo} from {model.Source} to {model.Destination} with partial move"
                    : $"Moved items from {model.Source} to {model.Destination} without partial move"; ;
                model.IsSuccess = true;
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Message = ex.Message;
                model.Meta = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace };
            }

            return View(model);
        }

        [HttpGet("MoveContainerToBin")]
        public IActionResult MoveContainerToBin(string ctr)
        {
            // NSH >> Commented out code for refactor ...
            //if (!_featureFlags.FlagEnabled("material-mgmt", HttpContext.User))
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            // << NSH Commented out code for refactor ...

            return View(new MaterialPostModel() { Source = ctr });
        }

        [HttpPost("MoveContainerToBin")]
        public async Task<IActionResult> MoveContainerToBin(MaterialPostModel model)
        {
            try
            {
                //NSH
                //var result = await _moveContainerMgmClient.MoveContainerToBinAsync(_locationProvider.CurrentLocation, model.Destination, model.Source);
                var currentLocation = HttpContext.Session.GetString("Location_Code");
                var companyId = HttpContext.Session.GetString("CompanyID");
                var apiUrl = $"{_businessApis.Value.BaseUrl}{_businessApis.Value.packageManagement}";
                var binaApiUrl = string.Format(apiUrl, companyId, "Microsoft.NAV.movePackageToBin");
                var obj = new
                {
                    pLocationCode = model.Source,
                    pDestinationBin = model.Destination,
                    pSourcePackageNo = model.Source,
                };
                var result = new BusinessCentralApiHelper(_config.Value, HttpContext).PostApiResponse<GenericResponse2<string>>(binaApiUrl, obj);
                model.Message = $"Moved container {model.Source} to {model.Destination}";
                model.IsSuccess = true;
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Message = ex.Message;
                model.Meta = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace };
            }

            return View(model);
        }

        [HttpGet("RemoveContentOfContainer")]
        public IActionResult RemoveContentOfContainer(string item, string ctr, string lot, decimal? qty)
        {
            // << NSH Commented out code for refactor ...
            //if (!_featureFlags.FlagEnabled("material-mgmt", HttpContext.User))
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            // NSH >> Commented out code for refactor ...

            return View(new MoveBetweenPostModel() { Source = ctr, ItemNo = item, LotNo = lot, Qty = qty ?? 0.0m, MovePartial = true });
        }

        [HttpPost("RemoveContentOfContainer")]
        public async Task<IActionResult> RemoveContentOfContainer(MoveBetweenPostModel model)
        {
            if (!model.MovePartial)
            {
                model.ItemNo = string.Empty;
                model.LotNo = string.Empty;
                model.Qty = 0.0m;
            }

            try
            {
                //var result = await _moveContainerMgmClient.RemoveContentOfContainerAsync(_locationProvider.CurrentLocation, model.ItemNo,
                //    model.LotNo, string.Empty, model.Qty, string.Empty, model.Source, string.Empty, model.MovePartial); //NSH
                var currentLocation = HttpContext.Session.GetString("Location_Code");
                var companyId = HttpContext.Session.GetString("CompanyID");
                var apiUrl = $"{_businessApis.Value.BaseUrl}{_businessApis.Value.packageManagement}";
                var binaApiUrl = string.Format(apiUrl, companyId, "Microsoft.NAV.removeContentOfPackage");
                var obj = new
                {
                    pLocationCode = currentLocation,
                    pItemNo= model.ItemNo,
                    pLotNo = model.LotNo,
                    pQty= model.Qty,
                    pSourcePackageNo = model.Source,
                    pMovePartial = model.MovePartial
                };
                var result = new BusinessCentralApiHelper(_config.Value, HttpContext).PostApiResponse<GenericResponse2<string>>(binaApiUrl, obj);

                model.Message = model.MovePartial
                    ? $"Removed {model.Qty} items with item number {model.ItemNo} from lot number {model.LotNo} from {model.Source} with partial move"
                    : $"Removed items from {model.Source} without partial move"; 
                model.IsSuccess = true;
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Message = ex.Message;
                model.Meta = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace };
            }

            return View(model);
        }
    }
}
