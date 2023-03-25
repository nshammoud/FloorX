using AutoMapper;
using KQF.Floor.NavServices.ProductionOrders;
using KQF.Floor.Web.Configuration;
using KQF.Floor.Web.Helpers;
using KQF.Floor.Web.Models;
using KQF.Floor.Web.Models.BusinessCentralApi_Models;
using KQF.Floor.Web.NavServices.ProductionMgmt;
using KQF.Floor.Web.NavServices.WCResources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Web.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class OutputController : ControllerBase
    {
        private readonly ProductionOrderClient _productOrders;
        private readonly ProductionMgmtClient _prodMgmtClient;
        private readonly WCResourcesClient _mixerClient;
        private readonly IMapper _mapper;
        private readonly IOptions<LoginBaseSettingConfig> _loginApiConfig;
        private readonly IOptions<BusinessCentralApis> _businessApis;
        private readonly IOptions<LoginBaseSettingConfig> _config;

        public OutputController(
            ProductionOrderClient poClient,
            ProductionMgmtClient mgClient,
            WCResourcesClient mixerClient,
            Microsoft.Extensions.Options.IOptions<UIConfiguration> uiConfig,
            IMapper mapper,
            ILogger<OutputController> logger,
            Services.UserSettingsService userSettingsService,
            FeatureFlagProvider featureFlags,
            Auth.LocationProvider locationProvider, IOptions<LoginBaseSettingConfig> loginApiConfig,
            IOptions<BusinessCentralApis> businessApis) : base(logger, locationProvider, uiConfig, userSettingsService, featureFlags)
        {
            _productOrders = poClient;
            _prodMgmtClient = mgClient;
            _mapper = mapper;
            _mixerClient = mixerClient;
            _loginApiConfig = loginApiConfig;
            _businessApis = businessApis;
        }

        public DateTime CurrentDate
        {
            get
            {
                var date = HttpContext.Session.GetString("Output.CurrentDate");
                if (date == null || date == string.Empty)
                {
                    date = DateTime.Now.ToString("yyyy-MM-dd");
                    HttpContext.Session.SetString("Output.CurrentDate", date);
                }
                return DateTime.Parse(date);
            }
            set
            {
                HttpContext.Session.SetString("Output.CurrentDate", value.ToString("yyyy-MM-dd"));

            }
        }

        public async Task<IActionResult> Index(DateTime? dateFilter)
        {
            ViewData["Title"] = "Output Journal";
            var date = dateFilter ?? CurrentDate;
            if (date != dateFilter && dateFilter.HasValue)
            {
                CurrentDate = dateFilter.Value;
            }

            var companyId = HttpContext.Session.GetString("CompanyID");
            var locationCode = HttpContext.Session.GetString("Location_Code");
            TempData["companyId"] = companyId;
            TempData["locationCode"] = locationCode;
            return View(date);
        }

        public async Task<IActionResult> ProductionOrder(string orderNo)
        {
            try
            {
                var updatedOrder = await _productOrders.ReadAsync(orderNo);

                return Ok(new
                {
                    success = true,
                    result = _mapper.Map<Models.ProductionOrder>(updatedOrder.MWSProductionOrderListV2),
                    message = ""
                });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }

        public async Task<IActionResult> ProductionOrders(DateTime? date)
        {
            var d = date ?? CurrentDate;
            if (date != CurrentDate && date.HasValue)
            {
                CurrentDate = date.Value;
            }
            var apiHelper = new BusinessCentralApiHelper(_loginApiConfig.Value, HttpContext);
            var companyId = HttpContext.Session.GetString("CompanyID");
            var locationCode = HttpContext.Session.GetString("Location_Code");
            var startingDate = CurrentDate.ToString("yyyy-MM-dd");
            var ordersApi = string.Format(_businessApis.Value.productionOrderList, companyId);
            var apiUrl = $"{_businessApis.Value.BaseUrl}{ordersApi}";
            var ordersList = apiHelper.GetApiResponse<GenericResponse<ProductionOrderBuissnessCentral>>(apiUrl);

            //Floor.NavServices.ProductionOrders.ReadMultiple_Result results;
            var today = (date ?? DateTime.Now).ToString("yyyy-MM-dd");
            var yesterday = (date ?? DateTime.Now).AddDays(-1).ToString("yyyy-MM-dd");

            //var shiftChange = CurrentDate.AddSeconds(_locationProvider.LocationDayOffsetInSeconds);
            
            //>>NSH
            var sessionStoredCompanies = HttpContext.Session.GetString("LocationsList_" + companyId);
            var locations = new GenericResponse<Location>();

            if (!string.IsNullOrEmpty(sessionStoredCompanies))
            {
                locations = JsonConvert.DeserializeObject<GenericResponse<Location>>(sessionStoredCompanies);
            }
            else
            {
                var locationApi = string.Format(_businessApis.Value.Location, companyId);
                var apiUrl_ = $"{_businessApis.Value.BaseUrl}{locationApi}";
                locations = new BusinessCentralApiHelper(_config.Value, HttpContext).GetApiResponse<GenericResponse<Location>>(apiUrl);
                HttpContext.Session.SetString($"LocationsList_{companyId}", JsonConvert.SerializeObject(locations));
            }

            //>>NSH

            var list_ = locations.ReturnList;
            var locationItem = new Location();
            if (list_ != null && !string.IsNullOrEmpty(locationCode))
            {
                locationItem = list_.FirstOrDefault(x => x.Code.Contains(locationCode));
            }


            var shiftChange = CurrentDate.AddSeconds(Convert.ToDouble(locationItem.EndOfDayOffset));
            var showPreviousDay = DateTime.Now <= shiftChange;
            //var criteria = showPreviousDay ? $"{yesterday}..{today}" : $"{today}"; //NSH
            DateTime? StrDateFrom = null; 
            DateTime? StrDateTo = null;
            if (showPreviousDay)
            {
                StrDateFrom = Convert.ToDateTime(yesterday);
                StrDateTo = Convert.ToDateTime(today);
            } else
            {
                StrDateFrom = Convert.ToDateTime(today);
                StrDateTo = Convert.ToDateTime(today);
            }

            
            //NSH >>
            //results = await _productOrders.ReadMultipleAsync(new MWSProductionOrderListV2_Filter[] {
            //     new MWSProductionOrderListV2_Filter()
            //    {
            //        Field = MWSProductionOrderListV2_Fields.Starting_Date,
            //        Criteria = showPreviousDay ? $"{yesterday}..{today}" : $"={today}"
            //    },

            //    new MWSProductionOrderListV2_Filter()
            //    {
            //        Field = MWSProductionOrderListV2_Fields.Location_Code,
            //        Criteria = $"={CurrentLocation.ToUpper()}"
            //    }
            //}, "", 500);

            //var wh = User.GetWarehouseRecords(WarehouseRecordType.Output);
            //var iccs = wh.Where(x => x.Location.ToLower() == CurrentLocation.ToLower()).Select(x => x.ItemCategory.ToLower()).ToArray();

            //var models = _mapper.Map<Models.ProductionOrder[]>(results.ReadMultiple_Result1)
            //    .Where(x => iccs.Contains(x.ItemCategory.ToLower()))
            //     .OrderBy(x => x.StartDate)
            //    .ToArray();
            //<<NSH

            var list = ordersList.ReturnList;
            if(list != null && !string.IsNullOrEmpty(locationCode))
            {
                list = list.Where(x => x.LocationCode.Contains(locationCode)).ToArray();
            }

            //>> NSH
            //if (list != null && !string.IsNullOrEmpty(criteria)) 
            //{
            //    list = list.Where(x => x.StartingDate >= StrDateFrom).ToArray();
            //}

            if (list != null && StrDateFrom != null)
            {
                list = list.Where(x => x.StartingDate >= StrDateFrom && x.StartingDate <= StrDateTo).ToArray();
            }
            //<< NSH

            return PartialView(list);
        }

        public async Task<IActionResult> MixerList(string code)
        {
            var mixers = await _mixerClient.ReadMultipleAsync(new MWSWCresourceList_Filter[] {
                new MWSWCresourceList_Filter()
                {
                    Field = MWSWCresourceList_Fields.Work_Center,
                    Criteria = $"={code}"
                }
            }, "", 10); ;

            return Json(mixers.ReadMultiple_Result1.Select(x => new
            {
                Name = x.WC_Resource,
                x.Key
            }).ToArray());
        }

        [HttpPost]
        public async Task<IActionResult> PostJob(OutputPostModel model)
        {
            try
            {
                var obj = new
                {
                    pProdOrderNo = model.JobNumber,
                    pWorkShiftCode = "", // we don't have this yet
                    pPackageNo = model.ContainerNo,
                    pLotNo = "", // we don't have this yet
                    pSerialNo = string.Empty,
                    pQuantity = model.Quantity,
                    pWCResource = "", // we don't have this yet
                    pNoRodsBolos = model.BolosOrRods,
                    pCartID = model.UseCartId.ToString(), // was a bool but our API accepts string so i converted it into string
                };
                var companyId = HttpContext.Session.GetString("CompanyID");
                var apiHelper = new BusinessCentralApiHelper(_loginApiConfig.Value, HttpContext);
                // we will our own API here
                var ordersDetailApi = string.Format(_businessApis.Value.productionManagement, companyId, "Microsoft.NAV.postOutputJournal");
                var orderDetailApi = $"{_businessApis.Value.BaseUrl}{ordersDetailApi}";
                var result = apiHelper.PostApiResponse<dynamic>(orderDetailApi, obj);

                // NSH
                //var result = await _prodMgmtClient.PostOutputAsync(new PostOutput()
                //{
                //    pCartID = model.UseCartId ? (string.IsNullOrEmpty(model.ContainerNo) ? " " : model.ContainerNo) : " ",
                //    pcodContainerNo = model.UseCartId ? " " : (string.IsNullOrEmpty(model.ContainerNo) ? " " : model.ContainerNo),
                //    pcodLotNo = "",
                //    pcodProdOrderNo = model.JobNumber,
                //    pcodSerialNo = "",
                //    pcodWorkShiftCode = "",
                //    pNoRodsBolos = model.BolosOrRods ?? 0m,
                //    pQuantity = model.Quantity,
                //    ptxtMsg = "",
                //    pWCResource = String.IsNullOrEmpty(model.MixerLine) ? " " : model.MixerLine // the space is intentional.

                //});

                return Ok(new { success = true, message = "Updated Successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ValidateItemNo(OutputItemNumberValidation model)
        {
            if (model.ScannedItemNumber.Length > 14 && model.ItemCategoryCode?.ToUpper() == "FG")
            {
                var number = model.ScannedItemNumber.Substring(2, 14);
                model.ScannedItemNumber = number;
            }

            var result = await _prodMgmtClient.IsCrossRefItemExistAsync(model.JobItemNumber, model.ScannedItemNumber);
            model.IsValid = result.return_value;
            return Json(model);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewData["ProductionOrderListSource"] = "Output";

            base.OnActionExecuting(context);
        }
    }
}
