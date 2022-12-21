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
}
