using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KQF.Floor.NavServices.ProductionOrders;
using KQF.Floor.Web.Models;
using KQF.Floor.Web.NavServices.Components;
using KQF.Floor.Web.NavServices.ItemSubstitution;
using KQF.Floor.Web.NavServices.ProductionMgmt;

namespace KQF.Floor.NavServices.Mapping
{
    public class NavMaps : AutoMapper.Profile
    {
        public NavMaps()
        {

            CreateMap<MWSProductionOrderListV2, Web.Models.ProductionOrder>()
                .ForMember(x => x.ItemCategory, o => o.MapFrom(x => x.Item_Category_Code))
                .ForMember(x => x.ItemDescription, o => o.MapFrom(x => x.Item_Description))
                .ForMember(x => x.ItemNumber, o => o.MapFrom(x => x.Item_No))
                .ForMember(x => x.JobNumber, o => o.MapFrom(x => x.Prod_Order_No))
                .ForMember(x => x.Quantity, o => o.MapFrom(x => x.Quantity))
                .ForMember(x => x.FinishedQuantity, o => o.MapFrom(x => x.Finished_Quantity))
                .ForMember(x => x.StartDate, o => o.MapFrom(x => x.Starting_Date.AddTicks(x.Starting_Time.Ticks))) // add time
                .ForMember(x => x.UnitOfMeasure, o => o.MapFrom(x => x.Unit_of_Measure_Code))
                .ForMember(x => x.ScreenType, o => o.MapFrom(x => x.OutputScreenVersionText))
                .ForMember(x => x.RemainingQuantity, o => o.MapFrom(x => x.Remaining_Quantity))
                .ForMember(x => x.Location, o => o.MapFrom(x => x.Location_Code))
                .ForMember(x => x.StandardReportingQty, o => o.MapFrom(x => x.Std_Report_Qty))
                .ForMember(x => x.StandardReportingUoM, o => o.MapFrom(x => x.Reporting_UOM))
                .ForMember(x => x.ShowStandardReporting, o => o.MapFrom(x => x.ProdOutputReqStdReporting))
                .ForMember(x => x.CartOrContainerRequired, o => o.MapFrom(x => x.ProdOutputReqCart || x.ProdOutputReqContainer))
                .ForMember(x => x.WorkCenter, o => o.MapFrom(x => x.Work_Center_No))
                .ForMember(x => x.ResourceCaption, o => o.MapFrom(x => x.Resource_Caption))
                .ForMember(x => x.RequireMixerLine, o => o.MapFrom(x => x.WCResourceRequiredSpecified && x.WCResourceRequired))
                .ForMember(x => x.AllowedOverOutputPercent, o => o.MapFrom(x => x.OverOutput))
                .ForMember(x => x.AllowOverReportConsumption, o => o.MapFrom(x => x.AllowOverReportConsumptionSpecified ? x.AllowOverReportConsumption : true))
                .ForMember(x => x.EnableItemValidation, o => o.MapFrom(x => x.Enable_Item_ValidationSpecified ? x.Enable_Item_Validation : false))
                .ForMember(x => x.ValidateEveryOutput, o => o.MapFrom(x => x.ValidateEveryOutputSpecified ? x.ValidateEveryOutput : false))
                .ForAllOtherMembers(x => x.Ignore());


            CreateMap<MWSProdComponentList, Web.Models.ProductionOrderComponent>()
                .ForMember(x => x.ReworkOnly, o => o.MapFrom(x => false))
                .ForMember(x => x.IsConsolidated, o => o.MapFrom(x => false))
                .ForMember(x => x.Description, o => o.MapFrom(x => x.Description))
                .ForMember(x => x.ItemNo, o => o.MapFrom(x => x.Item_No))
                .ForMember(x => x.ProductionOrderNo, o => o.MapFrom(x => x.Prod_Order_No))
                .ForMember(x => x.UoM, o => o.MapFrom(x => x.Unit_of_Measure_Code))
                .ForMember(x => x.QuantityPerUnit, o => o.MapFrom(x => x.Quantity_perSpecified ? (decimal?)(x.Quantity_per == 0 ? 1 : x.Quantity_per) : null))
                .ForMember(x => x.IsStatusValid, o => o.MapFrom(x => (bool?)null))
                .ForMember(x => x.NumberOfUnits, o => o.MapFrom(x => 0))
                .ForMember(x => x.DecimalNumberOfUnits, o => o.MapFrom(x => 0m))
                .ForMember(x => x.ExpectedQuantity, o => o.MapFrom(x => x.Expected_QuantitySpecified ? x.Expected_Quantity : 0m))
                .ForMember(x => x.RemainingQuantity, o => o.MapFrom(x => x.Remaining_QtySpecified ? x.Remaining_Qty : 0m))
                .ForMember(x => x.MovedQuantity, o => o.MapFrom(x => x.Moved_QtySpecified ? x.Moved_Qty : 0m))
                .ForMember(x => x.ItemCategory, o => o.MapFrom(x => x.Item_Category_Component ?? string.Empty))
                .ForMember(x => x.IsConsumable, o => o.MapFrom(x => x.Not_Show_In_Component_ListSpecified ? !x.Not_Show_In_Component_List : true))
                .ForMember(x => x.IsGroupedConsumption, o => o.MapFrom(x => x.Grouped_ConsSpecified ? x.Grouped_Cons : false))
                .ForMember(x => x.GroupedConsumptionAllowedPercentage, o => o.MapFrom(x => x.Allowable_Cons_PercentageSpecified ? x.Allowable_Cons_Percentage : 0m))
                .ForMember(x => x.IsAuthorized, o => o.MapFrom(x => x.AllowedComponent))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<MWSSubstitudeItemList, Web.Models.ItemSubstitution>()
                .ForMember(x => x.Description, o => o.MapFrom(x => x.Description))
                .ForMember(x => x.ItemNo, o => o.MapFrom(x => x.No))
                .ForMember(x => x.SubstituteItemNo, o => o.MapFrom(x => x.Substitute_No))
                .ForMember(x => x.ReworkPercentage, o => o.MapFrom(x => x.MaxConsSubstOfRewItemSpecified ? (decimal?)x.MaxConsSubstOfRewItem : (decimal?)null))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<Floor.Data.Models.ConsumptionItemTransaction, Web.Models.ComponentTransactionModel>()
                .ForMember(x=> x.JobStartDate, o=> o.MapFrom(x=> DateTime.Now))
                .ForMember(x => x.ReworkOverAllocatedQty, o => o.MapFrom(x => 0));

            CreateMap<ComponentTransactionModel, ConsumptionLine>()
                //.ForMember(x => x.BinCode, o => o.MapFrom(x => ""))
                .ForMember(x => x.Scrap, o => o.MapFrom(x => x.IsWaste ? "true" : "false"))
                .ForMember(x => x.ContainerNumber, o => o.MapFrom(x => x.ContainerNumber ?? string.Empty))
                .ForMember(x => x.RunId, o => o.MapFrom(x => String.Format("{0}-{1}", x.RunNumber, x.Id)))
                .ForMember(x=> x.PostingDate, o=> o.MapFrom(x=> x.JobStartDate))
                //.ForMember(x => x.DateCreated, o => o.MapFrom(x => new string[] { }))
                //.ForMember(x => x.DatePosted, o => o.MapFrom(x => new string[] { }))
                //.ForMember(x => x.IsPosted, o => o.MapFrom(x => false))
                //.ForMember(x => x.ItemDescription, o => o.MapFrom(x => x.ItemDescription ?? string.Empty))
                .ForMember(x => x.ItemNumber, o => o.MapFrom(x => x.ItemNumber ?? string.Empty))
                //.ForMember(x => x., o => o.MapFrom(x => x.LocationCode))
                .ForMember(x => x.LotNumber, o => o.MapFrom(x => x.LotNumber ?? string.Empty))
                .ForMember(x => x.MixerId, o => o.MapFrom(x => x.MixerId))
                .ForMember(x => x.ParentItemNumber, o => o.MapFrom(x => x.ParentItemNumber))
                .ForMember(x => x.ProductionOrderNumber, o => o.MapFrom(x => x.ProductionOrderNumber))
                .ForMember(x => x.Quantity, o => o.MapFrom(x => x.UserEnteredQuantity))
                .ForMember(x => x.UOM, o => o.MapFrom(x => x.UoM))
                .ForMember(x => x.UserId, o => o.MapFrom(x => x.UserId))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
