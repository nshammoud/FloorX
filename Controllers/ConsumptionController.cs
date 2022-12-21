using AutoMapper;
using KQF.Floor.NavServices.ProductionOrders;
using KQF.Floor.Web.Configuration;
using KQF.Floor.Web.Hubs;
using KQF.Floor.Web.Models;
using KQF.Floor.Web.NavServices;
using KQF.Floor.Web.NavServices.Components;
using KQF.Floor.Web.NavServices.ItemLotList;
using KQF.Floor.Web.NavServices.ItemSubstitution;
using KQF.Floor.Web.NavServices.PostedConsumption;
using KQF.Floor.Web.NavServices.ProductionMgmt;
using KQF.Floor.Web.NavServices.WCResources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace KQF.Floor.Web.Controllers
{
    [Authorize]
    public class ConsumptionController : ControllerBase
    {

        private readonly ProductionOrderClient _productOrders;
        private readonly ProductionMgmtClient _prodMgmtClient;
        private readonly WCResourcesClient _mixerClient;
        private readonly ItemNumberClient _itemNumberClient;
        private readonly ComponentsClient _componentsClient;
        private readonly ItemSubstitutionListClient _itemSubClient;
        private readonly ItemLotListClient _itemLotListClient;
        private readonly PostedConsumptionClient _postedConsumptionClient;
        private readonly KQF.Floor.Data.FloorDataContext _context;
        private readonly ConsumptionConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IHubContext<ConsumptionHub> _hub;

        public ConsumptionController(ProductionOrderClient poClient,
            ProductionMgmtClient mgClient,
            ItemNumberClient itemNumberClient,
            ComponentsClient componentsClient,
            ItemSubstitutionListClient itemSubClient,
            WCResourcesClient mixerClient,
            ItemLotListClient itemLotListClient,
            PostedConsumptionClient postedConsumptionClient,
            KQF.Floor.Data.FloorDataContext context,
            Microsoft.Extensions.Options.IOptions<ConsumptionConfiguration> config,
            Microsoft.Extensions.Options.IOptions<UIConfiguration> uiConfig,
            IHubContext<ConsumptionHub> hub,
            IMapper mapper,
            ILogger<OutputController> logger,
            Auth.LocationProvider locationProvider,
            Services.UserSettingsService userSettingsService,
            FeatureFlagProvider featureFlags) : base(logger, locationProvider, uiConfig, userSettingsService, featureFlags)
        {

            _productOrders = poClient;
            _prodMgmtClient = mgClient;
            _mapper = mapper;
            _mixerClient = mixerClient;
            _itemNumberClient = itemNumberClient;
            _componentsClient = componentsClient;
            _context = context;
            _itemSubClient = itemSubClient;
            _config = config.Value;
            _hub = hub;
            _itemLotListClient = itemLotListClient;
            _postedConsumptionClient = postedConsumptionClient;
        }


        public DateTime CurrentDate
        {
            get
            {
                var date = HttpContext.Session.GetString("Input.CurrentDate");
                if (date == null || date == string.Empty)
                {
                    date = DateTime.Now.ToString("yyyy-MM-dd");
                    HttpContext.Session.SetString("Input.CurrentDate", date);
                }
                return DateTime.Parse(date);
            }
            set
            {
                HttpContext.Session.SetString("Input.CurrentDate", value.ToString("yyyy-MM-dd"));

            }
        }

        public async Task<IActionResult> Index(DateTime? dateFilter)
        {

            ViewData["Title"] = "Consumption";
            var date = dateFilter ?? CurrentDate;
            if (date != dateFilter && dateFilter.HasValue)
            {
                CurrentDate = dateFilter.Value;
            }

            return View(date);
        }

        public async Task<IActionResult> Components(string orderNumber, string line, bool allowOverReportConsumption = true)
        {

            var models = await GetComponents(orderNumber, line, allowOverReportConsumption);
            return View(models);

        }

        private async Task<ProductionOrderComponent[]> GetComponents(string orderNumber, string line, bool allowOverReportConsumption = true)
        {
            var wh = User.GetWarehouseRecords(WarehouseRecordType.Consumption);
            var iccs = wh.Where(x => x.Location.ToLower() == CurrentLocation.ToLower()).Select(x => x.ItemCategory?.ToLower()).ToArray();


            var results = await this._componentsClient.ReadMultipleAsync(new MWSProdComponentList_Filter[] { new MWSProdComponentList_Filter()
            {
                Field = MWSProdComponentList_Fields.Prod_Order_No,
                Criteria = $"={orderNumber}"
            } }, "", 50);

            var models = _mapper.Map<Models.ProductionOrderComponent[]>(results.ReadMultiple_Result1)
              .OrderBy(x => x.ItemNo)
              .ToArray();

            //Load transactions
            var unpostedTx = await _context.Transactions
                .AsNoTracking()
                .Where(x => x.IsPosted == false && x.MixerId == line && x.ProductionOrderNumber == orderNumber && x.LocationCode == CurrentLocation)
                .ToArrayAsync();

            // should all have same runID
            var runNumber = unpostedTx.FirstOrDefault()?.RunNumber ?? "";

            //get any posted transactions with matching run number
            var transactions = (await _context.Transactions
                .AsNoTracking()
                .Where(x => x.IsPosted == true && x.MixerId == line && x.ProductionOrderNumber == orderNumber && x.LocationCode == CurrentLocation && x.RunNumber == runNumber)
                .ToArrayAsync()).Union(unpostedTx).Distinct().ToArray();

            var status = new Dictionary<string, int>();

            //load substitues
            var substitutions = await _itemSubClient.ReadMultipleAsync(new MWSSubstitudeItemList_Filter[] {
                new MWSSubstitudeItemList_Filter()
                {
                    Field = MWSSubstitudeItemList_Fields.No,
                    Criteria = "=" + String.Join('|', models.Select(x=> x.ItemNo).Distinct().ToArray())
                }
            }, string.Empty, 500);

            var subModels = _mapper.Map<Models.ItemSubstitution[]>(substitutions.ReadMultiple_Result1)
             .OrderBy(x => x.ItemNo)
                .ThenBy(x => x.SubstituteItemNo)
             .ToArray();

            foreach (var component in models)
            {

                component.Transactions = _mapper.Map<List<Models.ComponentTransactionModel>>(
                    transactions.Where(x => x.ParentItemNumber == component.ItemNo
                    ).ToList());

                component.Substituions = subModels.Where(x => x.ItemNo == component.ItemNo).ToList();

                component.IsAuthorized = iccs.Contains(component.ItemCategory.ToLower());
                component.NumberOfUnits = 0;
                component.DecimalNumberOfUnits = 0;

                var groupedByItem = component.Transactions.GroupBy(x => x.ItemNumber);

                foreach (var txGroup in groupedByItem)
                {
                    var sub = subModels.FirstOrDefault(x => x.SubstituteItemNo == txGroup.Key);
                    if ((sub?.ReworkPercentage ?? 0) > 0)
                    {
                        var componentTotal = txGroup.Select(x => x.Quantity).DefaultIfEmpty(0m).Sum();
                        var max = componentTotal * (sub.ReworkPercentage.Value / 100m);
                        var groupTotal = txGroup.Select(x => x.Quantity).DefaultIfEmpty(0m).Sum();

                        if (groupTotal > max)
                        {
                            txGroup.ToList().ForEach(s =>
                            {
                                s.ReworkOverAllocatedQty = groupTotal - max;
                            });
                        }
                    }
                }

                if (component.IsConsumable)
                {
                    var quantity = component.Transactions.Where(x => !x.IsWaste).Select(x => x.Quantity).DefaultIfEmpty(0).Sum();
                    if (quantity > 0)
                    {
                        var numberOfUnits = quantity / component.QuantityPerUnit ?? 1;
                        component.DecimalNumberOfUnits = numberOfUnits;
                        component.NumberOfUnits = Convert.ToInt32(numberOfUnits);
                    }
                }
            }

            var itemGroups = models.Where(x => x.IsConsumable).GroupBy(x => x.ItemCategory);

            var groupMaxes = itemGroups.Select(x => new
            {
                Category = x.Key,
                Max = x.Select(y => y.DecimalNumberOfUnits).DefaultIfEmpty(0).Max(y => y),
                Sum = x.Select(y => y.DecimalNumberOfUnits).DefaultIfEmpty(0).Sum(),
                DecimalSum = x.Select(y => y.DecimalNumberOfUnits).DefaultIfEmpty(0).Sum(),
                Count = x.Count(),
                Consolidate = _config.ConsolidateStatusItems.Contains(x.Key.ToLower())
            }).Select(x => new
            {
                x.Category,
                DecimalMax = x.Consolidate ?
                    (x.Count > 0 ? (x.DecimalSum / (decimal)x.Count) : 0m) :
                    (decimal)x.Max,
                Max = x.Consolidate ?
                    Convert.ToInt32((x.Count > 0 ? ((decimal)x.DecimalSum / (decimal)x.Count) : 0m) + 0.1m) :  //force round up on .5
                    x.Max,
                x.Consolidate
            })
            .ToArray();

            var maxValue = groupMaxes.Where(x => !x.Consolidate).Select(x => x.Max).DefaultIfEmpty(1).Max();
            if (groupMaxes.Count(x => !x.Consolidate) == 0)
            {
                maxValue = groupMaxes.Max(x => x.Max);
            }

            //var groupRange = maxValue < 2 ? new int[] { maxValue } : new int[] { maxValue, maxValue - 1, maxValue + 1 };
            var validNumberOfBatches = Convert.ToInt32(maxValue);
            var hasAnyTrans = transactions.Any(t => !t.IsWaste);

            foreach (var group in itemGroups)
            {
                if (_config.ConsolidateStatusItems.Contains(group.Key.ToLower()))
                {
                    var groupMax = Convert.ToInt32(groupMaxes.First(x => x.Category == group.Key).Max);

                    if (hasAnyTrans)
                    {
                        if (group.Any(x => x.IsGroupedConsumption) && validNumberOfBatches > 0)
                        {
                            var targetWeight = group.Select(x => x.QuantityPerUnit ?? 0m).DefaultIfEmpty(0m).Sum(x => x) * validNumberOfBatches;
                            var allowedPercent = (group.Where(x => x.GroupedConsumptionAllowedPercentage > 0).FirstOrDefault()?.GroupedConsumptionAllowedPercentage ?? 0m) / 100m;
                            var allowed = targetWeight * allowedPercent;
                            var minWeight = targetWeight - allowed;
                            var maxWeight = targetWeight + allowed;
                            var totalWeight = group.SelectMany(x => x.Transactions).Where(x => !x.IsWaste).Select(x => x.Quantity).DefaultIfEmpty(0).Sum();

                            group.ToList().ForEach(x =>
                            {
                                x.IsStatusValid = (totalWeight >= minWeight && totalWeight <= maxWeight);
                            });
                        }
                        else
                        {
                            group.ToList().ForEach(x =>
                            {
                                x.IsStatusValid = validNumberOfBatches == groupMax;
                            });
                        }
                    }
                }
                else
                {
                    if (hasAnyTrans && maxValue > 0)
                    {
                        group.ToList().ForEach(x =>
                        {
                            x.IsStatusValid = x.NumberOfUnits == validNumberOfBatches;

                        });
                    }
                }
            }

            //for grouped ICCs (isgroupdedwhatever)
            // our max calculated #of units for non consolidated * qty per (sumed across group) = perfect batch size
            // acceptable range = +/- (perfect batch * allowed percent)
            // validate against no grouped max batches as a range

            if (!allowOverReportConsumption)
            {
                // if the components are all remaining = 0 you can't post unless its rework
                var consolidateComponents = models.Where(x => x.IsConsumable && _config.ConsolidateStatusItems.Contains(x.ItemCategory.ToLower())).ToList();
                consolidateComponents.ForEach(x => x.IsConsolidated = true);

                if (!consolidateComponents.Any(x => (x.RemainingQuantity - x.MovedQuantity) > 0))
                {
                    //all transactions needs to be rework 
                    consolidateComponents.ForEach(x =>
                    {
                        x.ReworkOnly = true;
                        var isValid = x.IsStatusValid;

                        foreach (var t in x.Transactions.Where(y => !y.IsLocked && !y.IsWaste && !y.IsPosted))
                        {
                            var sub = subModels.FirstOrDefault(y => y.ItemNo == t.ItemNumber);
                            if ((sub?.ReworkPercentage ?? 0m) == 0)
                            {
                                isValid = false;
                                break;
                            }
                        }

                    });
                }
            }

            return models;
        }

        [HttpPost]
        public async Task<IActionResult> SubstitutionList(string[] itemNumbers)
        {
            var substitutions = await _itemSubClient.ReadMultipleAsync(new MWSSubstitudeItemList_Filter[] {
                new MWSSubstitudeItemList_Filter()
                {
                    Field = MWSSubstitudeItemList_Fields.No,
                    Criteria = "=" + String.Join('|', itemNumbers)
                }
            }, string.Empty, 500);
            var models = _mapper.Map<Models.ItemSubstitution[]>(substitutions.ReadMultiple_Result1)
             .OrderBy(x => x.ItemNo)
                .ThenBy(x => x.SubstituteItemNo)
             .ToArray();

            return Json(models);

        }

        public IActionResult ItemNumbers()
        {
            return Json(_itemNumberClient.Get());
        }

        public async Task<IActionResult> ProductionOrders(DateTime? date)
        {
            var d = date ?? CurrentDate;
            if (date != CurrentDate && date.HasValue)
            {
                CurrentDate = date.Value;
            }

            var shiftChange = CurrentDate.AddSeconds(_locationProvider.LocationDayOffsetInSeconds);
            var showPreviousDay = DateTime.Now <= shiftChange;

            Floor.NavServices.ProductionOrders.ReadMultiple_Result results;
            var today = (date ?? DateTime.Now).ToString("MMddyyyy");
            var yesterday = (date ?? DateTime.Now).AddDays(-1).ToString("MMddyyyy");



            results = await _productOrders.ReadMultipleAsync(new MWSProductionOrderListV2_Filter[] {
                 new MWSProductionOrderListV2_Filter()
                {
                    Field = MWSProductionOrderListV2_Fields.Starting_Date,
                    Criteria = showPreviousDay ? $"{yesterday}..{today}" : $"={today}"
                },

                new MWSProductionOrderListV2_Filter()
                {
                    Field = MWSProductionOrderListV2_Fields.Location_Code,
                    Criteria = $"={CurrentLocation.ToUpper()}"
                }
            }, "", 500);

            var wh = User.GetWarehouseRecords(WarehouseRecordType.Output);
            var iccs = wh.Where(x => x.Location.ToLower() == CurrentLocation.ToLower()).Select(x => x.ItemCategory.ToLower()).ToArray();
            

            var models = _mapper.Map<Models.ProductionOrder[]>(results.ReadMultiple_Result1)
                .Where(x => iccs.Contains(x.ItemCategory?.ToLower()))               
                .OrderBy(x => x.StartDate)
                .ToArray();
            return PartialView(models);
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

            var mixerNames = mixers.ReadMultiple_Result1.Select(x => x.WC_Resource).ToArray();
            // var lockedMixers = new ConsumptionTransaction[] { };// _context.Transactions
            //.Where(x => !x.IsPosted && !x.IsWaste && mixerNames.Contains(x.MixerId))
            //.Select(x => new { x.MixerId, x.ProductionOrderNumber }).Distinct().ToArray();



            return Json(mixers.ReadMultiple_Result1.Select(x => new
            {
                Name = x.WC_Resource,
                Caption = x.Work_Center,
                x.Key,
                ProductionOrderNumber = "" //lockedMixers.FirstOrDefault(m => m.MixerId.ToLower() == x.WC_Resource.ToLower())?.ProductionOrderNumber ?? ""
            }).ToArray());
        }


        [HttpPost()]
        public async Task<IActionResult> AddTx(ConsumptionTransaction model)
        {

            //make sure Mixer isn't in use first:
            //var lockedMixers = _context.Transactions
            //    .Where(x => !x.IsPosted
            //    && !x.IsWaste
            //    && model.JobNumber.ToLower() != x.ProductionOrderNumber.ToLower()
            //    && model.MixerId.ToLower() == x.MixerId.ToLower()).Select(x => x.ProductionOrderNumber).ToArray();

            //if (lockedMixers.Length > 0)
            //{
            //    return Json(new { Success = false, Message = $"Mixer {model.MixerId} is already in use by Job {lockedMixers[0]}", Exception = "" });
            //}


            //TODO: validate and store tx
            try
            {
                var results = await this._componentsClient.ReadMultipleAsync(new MWSProdComponentList_Filter[] { new MWSProdComponentList_Filter()
                {
                    Field = MWSProdComponentList_Fields.Prod_Order_No,
                    Criteria = $"={model.JobNumber.Trim()}"
                } }, "", 50);

                var component = results.ReadMultiple_Result1.FirstOrDefault(x => x.Item_No.Trim().ToLower() == model.ItemNumber.Trim().ToLower());
                MWSSubstitudeItemList sub = null;

                if (component == null)
                {
                    // see if its a subtitution
                    var substitutions = await _itemSubClient.ReadMultipleAsync(new MWSSubstitudeItemList_Filter[] {
                    new MWSSubstitudeItemList_Filter()
                        {
                            Field = MWSSubstitudeItemList_Fields.No,
                            Criteria = "=" + String.Join('|', results.ReadMultiple_Result1.Select(x=> x.Item_No.Trim()).ToArray())
                        }
                    }, string.Empty, 500);

                    sub = substitutions.ReadMultiple_Result1.FirstOrDefault(x => x.Substitute_No.Trim().ToLower() == model.ItemNumber.Trim().ToLower());
                    if (sub != null)
                    {
                        component = results.ReadMultiple_Result1.FirstOrDefault(x => x.Item_No.Trim().ToLower() == sub.No.Trim().ToLower());
                    }
                }

                if (component != null)
                {
                    var mapped = _mapper.Map<Models.ProductionOrderComponent>(component);

                    // Convert from base UOM to componentn UOM
                    var conversion = await _prodMgmtClient.NicConvertItemUOMAsync(new NicConvertItemUOM()
                    {
                        pExpectedUOM = mapped.UoM.Trim(),
                        pItemNo = mapped.ItemNo.Trim(),
                        pQtyInBaseUOM = model.Quantity,
                        qtyInExpectedUOM = 0m
                    });


                    var wh = User.GetWarehouseRecords(WarehouseRecordType.Consumption);
                    var iccs = wh.Where(x => x.Location.ToLower() == CurrentLocation.ToLower()).Select(x => (x.ItemCategory ?? "").Trim().ToLower()).ToArray();
                    var authorized = iccs.Contains(mapped.ItemCategory.Trim().ToLower());

                    //if its item # lookup, then validate the quantity && lot #
                    if (String.IsNullOrEmpty(model.ContainerNumber))
                    {
                        try
                        {
                            var result = await _prodMgmtClient.GetItemLotListAsync(new GetItemLotList()
                            {
                                pItemNo = model.ItemNumber.Trim(),
                                pLocationCode = CurrentLocation,
                                mWSItemLotList = new ItemLots()
                            });

                            var lot = result.mWSItemLotList.ItemLotList.OrderByDescending(x => x.AvailableLooseItem).FirstOrDefault(x => x.LotNo.ToLower() == model.LotNumber.Trim().ToLower());
                            if (lot == null)
                            {
                                return Json(new { Success = false, Message = $"Lot {model.LotNumber} not found for item {model.ItemNumber}", Exception = "" });
                            }
                            else
                            {
                                var available = 0m;
                                //make sure quantity is less than available
                                if (decimal.TryParse(lot.AvailableLooseItem, out decimal q))
                                {
                                    available = q;
                                }

                                if (model.Quantity > available)
                                {
                                    return Json(new { Success = false, Message = $"Lot {model.LotNumber} has a maximum quantity of {available} available", Exception = "" });
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            return Json(new { Success = false, Message = $"Could not validate Lot to Item. Nav Error.", Exception = ex.Message });
                        }
                    }

                    if (authorized)
                    {
                        try
                        {
                            var isScrap = _context.Transactions.Where(x => x.ProductionOrderNumber == model.JobNumber).Any(x => x.IsWaste);
                            //needs a run number 
                            var rn = _context.Transactions.Where(x => x.IsPosted == false && x.ProductionOrderNumber == model.JobNumber).Select(x => x.RunNumber).ToArray().FirstOrDefault(x => !string.IsNullOrEmpty(x)) ?? "";

                            // create the transaction
                            var tx = new Floor.Data.Models.ConsumptionItemTransaction()
                            {
                                ItemNumber = model.ItemNumber.Trim(),
                                ContainerNumber = model.ContainerNumber?.Trim(),
                                LotNumber = model.LotNumber.Trim(),
                                DateCreated = DateTime.Now,
                                DatePosted = null,
                                ItemDescription = sub != null ? sub.Description : mapped.Description,
                                ParentItemNumber = mapped.ItemNo,
                                ProductionOrderNumber = model.JobNumber.Trim(),
                                IsPosted = false,
                                IsWaste = isScrap,
                                UserId = User.UserName(),
                                Quantity = conversion.qtyInExpectedUOM,
                                MixerId = model.MixerId.Trim(),
                                LocationCode = CurrentLocation,
                                UoM = component.Unit_of_Measure_Code,
                                UserEnteredQuantity = model.Quantity,
                                RunNumber = rn
                            };

                            _context.Transactions.Add(tx);
                            var result = await _context.SaveChangesAsync();
                            if (result == 1)
                            {
                                await _hub.Clients.All.SendAsync("JobUpdateNotification", User.UserName(), model.JobNumber, model.MixerId);
                                return Json(new { Success = true, Message = "" });
                            }
                        }
                        catch (Exception ex)
                        {
                            return Json(new { Success = false, Message = "An error occurred while adding the transaction.", Exception = ex.Message });
                        }
                    }
                }

                return Json(new { Success = false, Message = $"Could not find matching component for {model.ItemNumber} on {model.JobNumber}", Exception = "" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = $"An error occured while connecting to JustFoods", Exception = ex.Message });
            }
        }

        [HttpPost()]
        public async Task<IActionResult> DeleteTx(long id)
        {
            try
            {
                var tx = _context.Transactions.FirstOrDefault(x => x.Id == id);
                if (tx != null && tx?.IsPosted != true)
                {
                    if (User.HasRole("supervisor", ViewData["CurrentLocation"] as string) || User.UserName() == tx.UserId)
                    {
                        _context.Transactions.Remove(tx);
                        await _context.SaveChangesAsync();
                        await _hub.Clients.All.SendAsync("JobUpdateNotification", User.UserName(), tx.ProductionOrderNumber, tx.MixerId);
                        return Json(new { Result = true });
                    }
                    else
                    {
                        return Json(new { Result = false, Exception = "Unauthorized" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Exception = ex.Message });
            }

            return Json(new { Result = false });
        }

        [HttpPost()]
        public async Task<IActionResult> LockTx(long id)
        {
            try
            {
                var tx = _context.Transactions.FirstOrDefault(x => x.Id == id);
                if (tx != null && tx?.IsPosted != true)
                {
                    if (User.HasRole("supervisor", ViewData["CurrentLocation"] as string) || User.UserName() == tx.UserId)
                    {
                        tx.IsLocked = true;
                        await _context.SaveChangesAsync();
                        await _hub.Clients.All.SendAsync("JobUpdateNotification", User.UserName(), tx.ProductionOrderNumber, tx.MixerId);
                        return Json(new { Result = true });
                    }
                    else
                    {
                        return Json(new { Result = false, Exception = "Unauthorized" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Exception = ex.Message });
            }

            return Json(new { Result = false });
        }

        [HttpPost()]
        public async Task<IActionResult> WasteTx(long id, bool undo = false)
        {
            var result = await WasteAll(id, undo);
            if (result.Item1)
            {
                return Json(new { Result = true });
            }
            else
            {
                return Json(new { Result = false, Exception = result.Item2 });
            }
        }

        private async Task<(bool, string)> WasteAll(long transactionId, bool undo = false)
        {
            try
            {
                var tx = _context.Transactions.AsNoTracking().FirstOrDefault(x => x.Id == transactionId);

                if (tx != null && tx?.IsPosted != true)
                {
                    if (User.HasRole("supervisor", ViewData["CurrentLocation"] as string) || (User.UserName() == tx.UserId && !undo))
                    {
                        // Waste all transactions
                        var allTransactions = await _context.Transactions.Where(x => !x.IsPosted && x.ProductionOrderNumber == tx.ProductionOrderNumber).ToListAsync();

                        allTransactions.ForEach(x => x.IsWaste = !undo);

                        await _context.SaveChangesAsync();
                        await _hub.Clients.All.SendAsync("JobUpdateNotification", User.UserName(), tx.ProductionOrderNumber, tx.MixerId);
                        return (true, string.Empty);
                    }
                    else
                    {
                        return (false, "Unauthorized");
                    }
                }
                else
                {
                    return (false, "Job already posted");
                }
            }
            catch (Exception ex)
            {

                return (false, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> ValidateContainer(string containerNo)
        {
            try
            {
                var result = await _prodMgmtClient.NicGetContainerContentAsync(new NicGetContainerContent()
                {
                    containerNo = containerNo.Trim(),
                    itemNo = "",
                    lotNo = "",
                    qauntity = 0
                });

                var existing = _context.Transactions.FirstOrDefault(x => x.ContainerNumber.ToLower() == containerNo.Trim().ToLower() && !x.IsPosted && !x.IsWaste);


                if (result.qauntity == -1)
                {
                    return Json(new { Valid = false, Message = "Container not found or contains multiple items" });
                } else if(existing != null)
                {
                    return Json(new { Valid = false, Message = $"Container already used on unposted job: {existing.ProductionOrderNumber} {existing.MixerId}." });
                }
                else
                {
                    return Json(new { Valid = true, Message = "", ItemNo = result.itemNo, Quantity = result.qauntity, LotNo = result.lotNo });
                }

            }
            catch (Exception ex)
            {
                return Json(new { Valid = false, Message = "There was an error connecting to JustFoods" });
            }

        }

        [HttpPost]
        public async Task<IActionResult> ValidateLotNumber(string itemNo)
        {
            try
            {
                var lots = await _itemLotListClient.ReadMultipleAsync(new WSItemLotList_Filter[]
                    {
                    new WSItemLotList_Filter()
                    {
                        Criteria = $"={itemNo.Trim()}",
                        Field = WSItemLotList_Fields.Item_No
                    },
                    new WSItemLotList_Filter()
                    {
                        Criteria = $"={CurrentLocation}",
                        Field = WSItemLotList_Fields.Location
                    }
                    }, "", 500);

                return Json(lots.ReadMultiple_Result1.Where(x => x.Available_Loose_Item > 0).Select(x => new { Success = true, LotNumber = x.Lot_No, Quantity = x.Available_Loose_Item }));
            }
            catch (Exception ex)
            {
                return Json(new object[] { new { Success = false, Message = "Could not retrieve lot information from Just Foods", Exception = ex.Message } });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Unlock(string job)
        {
            try
            {
                var txs = await _context.Transactions.Where(x => x.ProductionOrderNumber.ToLower() == job.ToLower() && x.IsLocked == true).ToListAsync();
                txs.ForEach(x => x.IsLocked = false);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred when unlocking the job", exception = ex.Message });
            }

        }

        [HttpPost]
        public async Task<IActionResult> PostJob(ConsumptionPostModel model)
        {

            var models = await GetComponents(model.OrderNumber, model.Line);

            if (models.Any(x => (x.IsStatusValid == null || x.IsStatusValid == false) && x.IsConsumable))
            {
                return Json(new { success = false, message = "Job not valid for posting." });
            }

            //var lockedMixers = _context.Transactions
            //    .Where(x => !x.IsPosted
            //    && !x.IsWaste
            //    && model.OrderNumber.ToLower() != x.ProductionOrderNumber.ToLower()
            //    && model.Line.ToLower() == x.MixerId.ToLower()).Select(x => x.ProductionOrderNumber).ToArray();

            //if (lockedMixers.Length > 0)
            //{
            //    return Json(new { Success = false, Message = $"Mixer {model.Line} is already in use by Job {lockedMixers[0]}", Exception = "" });
            //}

            var runNumber = model.OrderNumber + model.Line + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var transactions = models.SelectMany(x => x.Transactions).Where(x => x.IsPosted == false).ToArray();
            var rnGroup = transactions.Where(x => !String.IsNullOrEmpty(x.RunNumber))
                .GroupBy(x => x.RunNumber);
            if (rnGroup.Count() > 0)
            {
                runNumber = rnGroup.First().Key;
            }

            if (model.ForceAllToWaste ?? false && transactions.Length > 0)
            {
                var r = await WasteAll(transactions.First().Id);
                if (!r.Item1)
                {
                    return Json(new { success = false, message = "Error posting Job - wasting transactions.", exception = r.Item2 });
                }
            }

            foreach (var t in transactions)
            {
                t.RunNumber = runNumber;
                t.JobStartDate = model.JobStartDate;
            }

            var errormsg = "";
            bool fullPost = false;
            var mapped = _mapper.Map<ComponentTransactionModel[], ConsumptionLine[]>(transactions);

            try
            {
                var postResult = await _prodMgmtClient.PostBulkConsumptionAsync(new PostBulkConsumption()
                {
                    mWSConsListToPost = new ConsumptionListToPost()
                    {
                        ConsumptionLine = _mapper.Map<ComponentTransactionModel[], ConsumptionLine[]>(transactions),
                        Text = transactions.Select(x => string.Empty).ToArray()
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                errormsg = ex.Message;
            }

            try
            {


                var runResults = await _postedConsumptionClient.ReadMultipleAsync(new MWSPostedConsByRunID_Filter[]
                {
                    new MWSPostedConsByRunID_Filter()
                    {
                        Field = MWSPostedConsByRunID_Fields.Run_ID,
                        Criteria = $"{runNumber}-*"

                    }
                }, "", 1000);

                var postedTransactions = runResults.ReadMultiple_Result1.Select(x => x.Run_ID).ToList();


                // get all transactions
                var updateTransactions = await _context.Transactions
                   .Where(x => x.IsPosted == false && x.MixerId == model.Line && x.ProductionOrderNumber == model.OrderNumber && x.LocationCode == CurrentLocation)
                   .ToListAsync();

                updateTransactions.Where(x => String.IsNullOrEmpty(x.RunNumber)).ToList().ForEach(x => x.RunNumber = runNumber);

                postedTransactions.ForEach(x =>
                {
                    if (Int32.TryParse(x.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1], out int id))
                    {
                        var tx = updateTransactions.FirstOrDefault(x => x.Id == id);
                        if (tx != null)
                        {
                            tx.IsPosted = true;
                            tx.DatePosted = DateTime.Now;
                            tx.PostedByUserId = User.UserName();
                        }
                    }

                });

                fullPost = !updateTransactions.Any(x => !x.IsPosted && !x.IsWaste);

                //updateTransactions.ForEach(x =>
                //{
                //    x.IsPosted = transactions.Any(t => t.Id == x.Id);
                //    x.DatePosted = x.IsPosted ? DateTime.Now : null;
                //    x.PostedByUserId = x.IsPosted ? User.UserName() : "";
                //});

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error posting Job.", exception = ex.Message + "..." + errormsg });
            }

            await _hub.Clients.All.SendAsync("JobPostedNotification", User.UserName(), model.OrderNumber, model.Line);

            if (!string.IsNullOrEmpty(errormsg) || !fullPost)
            {
                return Json(new { success = true, message = "Job Partially Posted: " + errormsg, exception = errormsg });
            }
            else
                return Json(new { success = true, message = "Job Posted" });
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewData["IsConsumption"] = true;
            ViewData["ProductionOrderListSource"] = "Consumption";
            ViewData["MaxItemConsumptionQty"] = _config.MaxItemConsumptionQty;
            base.OnActionExecuting(context);
        }

        [HttpPost]
        [Authorize(Policy = "Administrator.Location")]
        public async Task<IActionResult> TogglePostedFlag(int txId)
        {
            var tx = _context.Transactions.Where(x=> x.Id == txId).FirstOrDefault();
            var oldVal = tx.IsPosted;
            if (tx != null)
            {
                try { 
                tx.IsPosted = !tx.IsPosted;
                await _context.SaveChangesAsync();
                }
                catch { };
            }

            return Json(new { Success = (tx.IsPosted != oldVal), CurrentVal = tx.IsPosted });
        }

        [HttpPost]
        [Authorize(Policy = "Administrator.Location")]
        public async Task<IActionResult> ToggleWastedFlag(int txId)
        {
            var tx = _context.Transactions.Where(x => x.Id == txId).FirstOrDefault();
            var oldVal = tx.IsWaste;
            if (tx != null)
            {
                try
                {
                    tx.IsWaste = !tx.IsWaste;
                    await _context.SaveChangesAsync();
                }
                catch { };
            }

            return Json(new { Success = (tx.IsWaste != oldVal), CurrentVal = tx.IsWaste });
        }

        [HttpPost]
        [Authorize(Policy = "Administrator.Location")]
        public async Task<IActionResult> ForceDeleteTx(int txId)
        {
            var tx = _context.Transactions.Where(x => x.Id == txId).FirstOrDefault();
            
            if (tx != null)
            {
                try
                {
                    _context.Transactions.Remove(tx);
                    await _context.SaveChangesAsync();
                }catch(Exception ex)
                {
                    return Json(new { Success = false, Exception = ex.Message });
                }
            }

            return Json(new { Success = true });
        }
    }
}
