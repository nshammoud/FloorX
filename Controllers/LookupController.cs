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

        public async Task<IActionResult> Index(string number)
        {
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
                var apiUrl = $"{_businessApis.Value.BaseUrl}{_businessApis.Value.LookupManagementBin}";
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
                            lookupModel.LookupResults = result;
                            if (result.Bin != null)
                            {
                                lookupModel.SearchTerm = number;
                            }
                            else
                            {
                                result.Bin = new List<Models.BusinessCentralApi_Models.BinLookupBin>().ToArray();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return View(lookupModel);
            }
            else
            {
                return View(new LookupModel() { SearchTerm = number, Results = new List<LookupResult>() });
            }
        }


        public async Task<LookupModel> DoLookup(string number)
        {
            var model = new LookupModel()
            {
                SearchTerm = number,
                Results = new List<LookupResult>()
            };

            //lookup everything
            var binResults = await _lookupClient.BinLookupAsync(new BinLookup1()
            {
                pLocation = _locationProvider.CurrentLocation.ToUpper(),

                pBinCode = number,
                pBinLookupXML = new Web.NavServices.Lookup.BinLookup()
                {
                    Bin = new Bin[] { },
                    Text = new string[] { }
                },
                pItemNo = "",
                pLotNo = ""
            });
            var bins = binResults.pBinLookupXML?.Bin?.Where(x => x.LocationCode.ToUpper() == _locationProvider.CurrentLocation.ToUpper()).ToArray();
            var itemResults = await _lookupClient.ItemLookupAsync(new ItemLookup1()
            {
                pBin = "",
                pItemLookup = new ItemLookup()
                {
                },
                pItemNo = number,
                pLocation = _locationProvider.CurrentLocation.ToUpper(),
                pLotNo = ""
            });


            var items = itemResults.pItemLookup.Item?.Where(x => !string.IsNullOrEmpty(x.ItemNo)).ToArray();


            var containerResults = await _lookupClient.ContainerLookupAsync(new ContainerLookup1()
            {
                pContainerNo = number,
                pContainerLookupXML = new ContainerLookup()
                {
                    Container = new Container[] { },
                    Text = new string[] { }
                }
            });
            var containers = containerResults.pContainerLookupXML?.Container?.Where(x => x.LocationCode.ToUpper() == _locationProvider.CurrentLocation.ToUpper()).ToArray();

            //map results
            if ((bins?.Length ?? 0) > 0)
            {
                model.Results.AddRange(_mapper.Map<Bin[], LookupBin[]>(bins));
            }

            if ((containers?.Length ?? 0) > 0)
            {
                model.Results.AddRange(_mapper.Map<Container[], LookupContainer[]>(containers));
            }

            if ((items?.Length ?? 0) > 0)
            {
                model.Results.AddRange(_mapper.Map<Item1[], LookupItemResult[]>(items));
            }

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
