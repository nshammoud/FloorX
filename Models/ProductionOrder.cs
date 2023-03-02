using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Web.Models
{
    public class ProductionOrder
    {
        public string JobNumber { get; set; }

        public string ItemCategory { get; set; }

        public DateTime StartDate { get; set; }

        public string ScreenType { get; set; }

        public decimal Quantity { get; set; }

        public decimal FinishedQuantity { get; set; }

        public decimal RemainingQuantity { get; set; }

        public string UnitOfMeasure { get; set; }


        public string ItemNumber { get; set; }

        public string ItemDescription { get; set; }

        public string Location { get; set; }

        public bool CartOrContainerRequired { get; set; }

        public bool ShowStandardReporting { get; set; }

        public decimal StandardReportingQty { get; set; }

        public string StandardReportingUoM { get; set; }

        public string WorkCenter { get; set; }

        public string ResourceCaption { get; set; }

        public bool RequireMixerLine { get; set; }

        public int AllowedOverOutputPercent { get; set; }

        public bool EnableItemValidation { get; set; }

        public bool AllowOverReportConsumption { get; set; }

        public bool ValidateOutput { get; set; }

        public bool ValidateEveryOutput { get; set; }

    }

    public class ProductionOrderBuissnessCentral
    {
        [JsonProperty(PropertyName = "@odata.etag")]
        public string odataetag { get; set; }

        [JsonProperty(PropertyName = "systemId")]
        public string SystemId { get; set; }

        [JsonProperty(PropertyName = "prodOrderNo")]
        public string ProdOrderNo { get; set; }

        [JsonProperty(PropertyName = "sourceNo")]
        public string SourceNo { get; set; }

        [JsonProperty(PropertyName = "itemCategoryCode")]
        public string ItemCategoryCode { get; set; }

        [JsonProperty(PropertyName = "workCenterNo")]
        public string WorkCenterNo { get; set; }

        [JsonProperty(PropertyName = "startingDate")]
        public string StartingDate { get; set; }

        [JsonProperty(PropertyName = "startingTime")]
        public string StartingTime { get; set; }

        [JsonProperty(PropertyName = "resourceCaption")]
        public string ResourceCaption { get; set; }

        [JsonProperty(PropertyName = "itemNo")]
        public string ItemNo { get; set; }

        [JsonProperty(PropertyName = "itemDescription")]
        public string ItemDescription { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; }

        [JsonProperty(PropertyName = "finishedQuantity")]
        public int FinishedQuantity { get; set; }

        [JsonProperty(PropertyName = "remainingQuantity")]
        public int RemainingQuantity { get; set; }

        [JsonProperty(PropertyName = "unitofMeasureCode")]
        public string UnitofMeasureCode { get; set; }

        [JsonProperty(PropertyName = "locationCode")]
        public string LocationCode { get; set; }

        [JsonProperty(PropertyName = "outputScreenVersionText")]
        public string OutputScreenVersionText { get; set; }

        [JsonProperty(PropertyName = "enableItemValidation")]
        public bool EnableItemValidation { get; set; }

        [JsonProperty(PropertyName = "prodOutputReqContainer")]
        public bool ProdOutputReqContainer { get; set; }

        [JsonProperty(PropertyName = "prodOutputReqStdReporting")]
        public bool ProdOutputReqStdReporting { get; set; }

        [JsonProperty(PropertyName = "prodOutputReqCart")]
        public bool ProdOutputReqCart { get; set; }

        [JsonProperty(PropertyName = "cartType")]
        public string CartType { get; set; }

        [JsonProperty(PropertyName = "wcresourceRequired")]
        public bool WcresourceRequired { get; set; }

        [JsonProperty(PropertyName = "overOutput")]
        public int OverOutput { get; set; }

        [JsonProperty(PropertyName = "allowOverReportConsumption")]
        public bool AllowOverReportConsumption { get; set; }

        [JsonProperty(PropertyName = "validateEveryOutput")]
        public bool ValidateEveryOutput { get; set; }
    }
}
