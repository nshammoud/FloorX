using AutoMapper;
using KQF.Floor.Web.Configuration;
using KQF.Floor.Web.Helpers;
using KQF.Floor.Web.Models;
using KQF.Floor.Web.Models.BusinessCentralApi_Models;
using KQF.Floor.Web.NavServices.Lookup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace KQF.Floor.Web.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class LookupController : ControllerBase
    {
        private readonly LookupClient _lookupClient;
        private readonly IMapper _mapper;
        KQF.Floor.Data.FloorDataContext _context;
        private readonly IOptions<LoginBaseSettingConfig> _config;
        private readonly IOptions<BusinessCentralApis> _businessApis;

        public LookupController(
            KQF.Floor.Data.FloorDataContext context,
          LookupClient lookupClient,
          Microsoft.Extensions.Options.IOptions<UIConfiguration> uiConfig,
          IMapper mapper,
          ILogger<LookupController> logger,
          Services.UserSettingsService userSettingsService,
          Auth.LocationProvider locationProvider, FeatureFlagProvider featureFlags, IOptions<LoginBaseSettingConfig> config,
          IOptions<BusinessCentralApis> businessApis) : base(logger, locationProvider, uiConfig, userSettingsService, featureFlags)
        {

            _mapper = mapper;
            _lookupClient = lookupClient;
            _context = context;
            _config = config;
            _businessApis = businessApis;
        }

        public async Task<IActionResult> Index_Obselete(string number)
        {
            if (!string.IsNullOrEmpty(number))
            {
                var model = await DoLookup(number);
                return View(model);
            }
            else
            {
                return View(new LookupModel() { SearchTerm = "", Results = new List<LookupResult>() });
            }

        }

        public async Task<IActionResult> Index_NSH(string number)
        {
            if (!string.IsNullOrEmpty(number))
            {
                HttpContext.Session.SetString("Location_Code", number);
            }

            if (string.IsNullOrEmpty(number))
            {
                number = HttpContext.Session.GetString("Location_Code");
            }

            ViewBag.code = number;

            var lookupModel = new LookupModel
            {
                Results = new List<LookupResult>()
            };
            if (!string.IsNullOrEmpty(number))
            {
                try
                {
                    lookupModel = await DoLookup(number);
                }
                catch (Exception)
                {
                }

                var companyId = HttpContext.Session.GetString("CompanyID");
                var apiUrl = $"{_businessApis.Value.BaseUrl}{_businessApis.Value.LookupManagement}";
                apiUrl = string.Format(apiUrl, companyId);
                var obj = new
                {
                    pLocation = string.Empty,
                    pBinCode = string.Empty,
                    pItemNo = number,
                    pLotNo = string.Empty
                };
                var lookupResults = new BusinessCentralApiHelper(_config.Value, HttpContext).PostApiResponse<GenericResponse2<string>>(apiUrl, obj);
                if (!string.IsNullOrEmpty(lookupResults.ReturnList))
                {
                    try
                    {
                        var serializer = new XmlSerializer(typeof(Models.BusinessCentralApi_Models.BinLookup));
                        using (TextReader reader = new StringReader(lookupResults.ReturnList))
                        {
                            var result = (Models.BusinessCentralApi_Models.BinLookup)serializer.Deserialize(reader);
                            //lookupModel.LookupResults = result;
                            if (result.Bin != null)
                            {
                                lookupModel.SearchTerm = number;
                            }
                            else
                            {
                                //result.Bin = new List<Models.BusinessCentralApi_Models.BinLookupBin>().ToArray();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else
                {
                    //lookupModel.LookupResults = new Models.BusinessCentralApi_Models.BinLookup
                    //{
                    //    Bin = new List<BinLookupBin>().ToArray()
                    //};
                }
                return View(lookupModel);
            }
            else
            {
                return View(new LookupModel() { SearchTerm = number, Results = new List<LookupResult>() });
            }
        }

        public async Task<IActionResult> Index(string number)
        {

            ViewBag.code = number;
            var companyId = HttpContext.Session.GetString("CompanyID");
            var locationCode = HttpContext.Session.GetString("Location_Code");
            TempData["companyId"] = companyId;
            TempData["locationCode"] = locationCode;

            if (string.IsNullOrEmpty(number))
            {
                ViewBag.SearchInputEmpty = true;
                var model = new LookupModel();
                return View(model);
            }


            var lookupModel = new LookupModel
            {
                Results = new List<LookupResult>()
            };
            if (!string.IsNullOrEmpty(number))
            {
                //try
                //{
                    lookupModel = await DoLookup(number);
                //}
                //catch (Exception ex)
                //{
                   // ViewBag.Error = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace };
                //}

                return View(lookupModel);
            }
            else
            {
                var model = new LookupModel
                {
                    SearchTerm = number,
                    Results = new List<LookupResult>(),
                    //LookupResults = new Models.BusinessCentralApi_Models.BinLookup()
                };
                return View(model);
            }
        }

        public async Task<LookupModel> DoLookup(string number)
        {
            var model = new LookupModel()
            {
                SearchTerm = number,
                Results = new List<LookupResult>()
            };

            //string pLocation = _locationProvider.CurrentLocation.ToUpper();
            string[] pLocations = _locationProvider.Locations; 
            //_locationProvider need to load // ok

            var currentLocation = HttpContext.Session.GetString("Location_Code");

            //lookup everything .. look up bin, items, containers (packages).
            //https://api.businesscentral.dynamics.com/v2.0/sandboxAPITest/api/kqf/kqfFloor/v2.0/companies(a2db7307-078b-ed11-aad8-000d3a21edc2)/lookupManagement(971c94c3-d1b0-ed11-9a88-0022485317e7)/Microsoft.NAV.binLookup
            var companyId = HttpContext.Session.GetString("CompanyID");
            var apiUrl = $"{_businessApis.Value.BaseUrl}{_businessApis.Value.LookupManagement}";
            var binaApiUrl = string.Format(apiUrl, companyId, "Microsoft.NAV.binLookup");
            var obj = new
            {
                pLocation = currentLocation,
                pBinCode = number,
                pItemNo = string.Empty,
                pLotNo = string.Empty
            };

            var lookupResults = new BusinessCentralApiHelper(_config.Value, HttpContext).PostApiResponse<GenericResponse2<string>>(binaApiUrl, obj);
            var serializer = new XmlSerializer(typeof(Models.BusinessCentralApi_Models.BinLookup));
            Models.BusinessCentralApi_Models.BinLookup binResults2 = null;
            var LookupBin_ = new LookupBin_();

            using (TextReader reader = new StringReader(lookupResults.ReturnList))
            {
                binResults2 = (Models.BusinessCentralApi_Models.BinLookup)serializer.Deserialize(reader);
                LookupBin_.BinLookupResults = binResults2;
                if (LookupBin_.BinLookupResults.Bin != null) {
                    LookupBin_.ResultType = "Bin";
                    LookupBin_.Title = LookupBin_.BinLookupResults.Bin.BinCode;
                    model.Results.Add(LookupBin_);
                }
            }

            //// here is the second API. 
            ////https://api.businesscentral.dynamics.com/v2.0/sandboxAPITest/api/kqf/kqfFloor/v2.0/companies(a2db7307-078b-ed11-aad8-000d3a21edc2)/lookupManagement(971c94c3-d1b0-ed11-9a88-0022485317e7)/Microsoft.NAV.itemLookup
            var itemLookupApiUrl = string.Format(apiUrl, companyId, "Microsoft.NAV.itemLookup");
            var objItemLookUp = new
            {
                pItemNo = number,
                pLocation = currentLocation,
                pBin = string.Empty,
                pLotNo = string.Empty
            };

            var itemLookupResults = new BusinessCentralApiHelper(_config.Value, HttpContext).PostApiResponse<GenericResponse2<string>>(itemLookupApiUrl, objItemLookUp);
            var serializer1 = new XmlSerializer(typeof(Models.BusinessCentralApi_Models.ItemLookup));

            Models.BusinessCentralApi_Models.ItemLookup ItemResults2 = null;
            var LookupItem_ = new LookupItem_();
            using (TextReader reader2 = new StringReader(itemLookupResults.ReturnList))
            {
                ItemResults2 = (Models.BusinessCentralApi_Models.ItemLookup)serializer1.Deserialize(reader2);
                LookupItem_.ItemLookupResults = ItemResults2;
                if (!String.IsNullOrEmpty(LookupItem_.ItemLookupResults.Item.ItemNo))
                {
                    LookupItem_.ResultType = "Item";
                    LookupItem_.Title = LookupItem_.ItemLookupResults.Item.ItemNo;
                    model.Results.Add(LookupItem_);
                }
            }

            //// below is 3rd API 
            ////https://api.businesscentral.dynamics.com/v2.0/sandboxAPITest/api/kqf/kqfFloor/v2.0/companies(a2db7307-078b-ed11-aad8-000d3a21edc2)/lookupManagement(971c94c3-d1b0-ed11-9a88-0022485317e7)/Microsoft.NAV.packageLookup

            var packageLookupApiUrl = string.Format(apiUrl, companyId, "Microsoft.NAV.packageLookup");
            var objpackageLookup = new
            {
                pPackageNo = number
            };

            var packageLookupResults = new BusinessCentralApiHelper(_config.Value, HttpContext).PostApiResponse<GenericResponse2<string>>(packageLookupApiUrl, objpackageLookup);
            var serializer2 = new XmlSerializer(typeof(Models.BusinessCentralApi_Models.PackageLookup));

            Models.BusinessCentralApi_Models.PackageLookup ContainerResult = null;
            var LookupContainer_ = new LookupContainer_();
            using (TextReader reader2 = new StringReader(packageLookupResults.ReturnList))
            {
                ContainerResult = (Models.BusinessCentralApi_Models.PackageLookup)serializer2.Deserialize(reader2);
                LookupContainer_.ContainerLookupResults = ContainerResult;
                if (!String.IsNullOrEmpty(LookupContainer_.ContainerLookupResults.Package[0].PackageNo))
                {
                    //group the packages by ICC
                    var groups = LookupContainer_.ContainerLookupResults.Package
                        .Where(x => x.ItemCategory.ICC_Code != null)
                        .GroupBy(x => x.ItemCategory.ICC_Code)
                        .Select(x => x.Select(y => y.ItemCategory.ICC_Code));
                    LookupContainer_.ResultType = "Container";
                    LookupContainer_.Title = LookupContainer_.ContainerLookupResults.Package[0].PackageNo;
                    //LookupContainer_.ContainerLookupResults.Package = groups;
                    model.Results.Add(LookupContainer_);
                }
            }



            #region commented Old Code
            //Old Code 
            //var binResults = await _lookupClient.BinLookupAsync(new BinLookup1()
            //{
            //    pLocation = _locationProvider.CurrentLocation.ToUpper(),

            //    pBinCode = number,
            //    pBinLookupXML = new Web.NavServices.Lookup.BinLookup()
            //    {
            //        Bin = new Bin[] { },
            //        Text = new string[] { }
            //    },
            //    pItemNo = "",
            //    pLotNo = ""
            //});

            //// below retrun result is filtered by current location slected. 
            //var bins = binResults.pBinLookupXML?.Bin?.Where(x => x.LocationCode.ToUpper() == _locationProvider.CurrentLocation.ToUpper()).ToArray();

            //var bins2 = binResults2.Bin?.Where(x => x.LocationCode.ToUpper() == currentLocation.ToUpper()).ToArray();

            //// here is the second API. 
            ////https://api.businesscentral.dynamics.com/v2.0/sandboxAPITest/api/kqf/kqfFloor/v2.0/companies(a2db7307-078b-ed11-aad8-000d3a21edc2)/lookupManagement(971c94c3-d1b0-ed11-9a88-0022485317e7)/Microsoft.NAV.itemLookup

            //var itemLookupApiUrl = string.Format(apiUrl, companyId, "Microsoft.NAV.itemLookup");

            //var objItemLookUp = new
            //{
            //    pItemNo = number,
            //    pLocation = currentLocation,
            //    pBin = string.Empty,
            //    pLotNo = string.Empty
            //};
            //var itemLookupResults = new BusinessCentralApiHelper(_config.Value, HttpContext).PostApiResponse<GenericResponse2<string>>(itemLookupApiUrl, objItemLookUp);

            //var itemResults = await _lookupClient.ItemLookupAsync(new ItemLookup1()
            //{
            //    pBin = "",
            //    pItemLookup = new ItemLookup()
            //    {
            //    },
            //    pItemNo = number,
            //    pLocation = _locationProvider.CurrentLocation.ToUpper(),
            //    pLotNo = ""
            //});


            //var items = itemResults.pItemLookup.Item?.Where(x => !string.IsNullOrEmpty(x.ItemNo)).ToArray();

            //// below is 3rd API 
            ////https://api.businesscentral.dynamics.com/v2.0/sandboxAPITest/api/kqf/kqfFloor/v2.0/companies(a2db7307-078b-ed11-aad8-000d3a21edc2)/lookupManagement(971c94c3-d1b0-ed11-9a88-0022485317e7)/Microsoft.NAV.packageLookup

            //var packageLookupApiUrl = string.Format(apiUrl, companyId, "Microsoft.NAV.packageLookup");
            //var objpackageLookup = new
            //{
            //    pPackageNo = number
            //};

            //var packageLookupResults = new BusinessCentralApiHelper(_config.Value, HttpContext).PostApiResponse<GenericResponse2<string>>(packageLookupApiUrl, objpackageLookup);

            //var containerResults = await _lookupClient.ContainerLookupAsync(new ContainerLookup1()
            //{
            //    pContainerNo = number,
            //    pContainerLookupXML = new ContainerLookup()
            //    {
            //        Container = new Container[] { },
            //        Text = new string[] { }
            //    }
            //});
            //var containers = containerResults.pContainerLookupXML?.Container?.Where(x => x.LocationCode.ToUpper() == _locationProvider.CurrentLocation.ToUpper()).ToArray();

            ////map results
            //// here are added all into a list.
            //// we need to try not to change below mapping 
            //if ((bins?.Length ?? 0) > 0)
            //{
            //    model.Results.AddRange(_mapper.Map<Bin[], LookupBin[]>(bins));
            //}

            //if ((containers?.Length ?? 0) > 0)
            //{
            //    model.Results.AddRange(_mapper.Map<Container[], LookupContainer[]>(containers));
            //}

            //if ((items?.Length ?? 0) > 0)
            //{
            //    model.Results.AddRange(_mapper.Map<Item1[], LookupItemResult[]>(items));
            //}
            #endregion
            // lastly return model
            return model;

        }

        [HttpPost]
        public async Task<IActionResult> JobHistory(JobHistoryArgs args)
        {
            WarehouseRecordType wt = args.Source.ToLower() == "consumption" ? WarehouseRecordType.Consumption : WarehouseRecordType.Output;
            var wh = User.GetWarehouseRecords(wt);
            var iccs = wh.Where(x => x.Location.ToLower() == CurrentLocation.ToLower()).Select(x => (x.ItemCategory ?? "").Trim().ToLower()).ToArray();

            var history = (await _context.SPJFJobHistory(args.JobNo))
                .Where(x => x.SrcSubType.ToLower() == args.Source.ToLower() &&
                iccs.Contains(x.ItemCategoryCode.ToLower()))
                .ToList();

            history.ForEach(x =>
            {
                //-	In the user field, can we remove the KQF\ from the beginning of the username? We should also only display the first 10 characters, we don’t need the full name, just enough to tell
                x.User = x.User.Replace("KQF\\", "").Replace("kqf\\", "").Truncate(10);

            });
            return Json(history.ToArray());
        }
    }
}
