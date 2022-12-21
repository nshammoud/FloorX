using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Web.Models
{
    public class ProductionOrderComponent
    {
        public ProductionOrderComponent()
        {
            Transactions = new List<Models.ComponentTransactionModel>();
            Substituions = new List<ItemSubstitution>();

        }

        public string ProductionOrderNo { get; set; }

        public string ItemNo { get; set; }

        public string Description { get; set; }

        public string UoM { get; set; }

        public decimal? QuantityPerUnit { get; set; }

        public bool? IsStatusValid { get; set; }

        public int NumberOfUnits { get; set; }

        public decimal DecimalNumberOfUnits { get; set; }

        public List<Models.ComponentTransactionModel> Transactions { get; set; }

        public decimal ExpectedQuantity { get; set; }

        public decimal RemainingQuantity { get; set; }

        public decimal MovedQuantity { get; set; }

        public string ItemCategory { get; set; }

        public bool IsConsumable { get; set; }

        public bool IsAuthorized { get; set; }

        public bool ReworkOnly { get; set; }

        public bool IsConsolidated { get; set; }

        public bool IsGroupedConsumption { get; set; }

        public decimal GroupedConsumptionAllowedPercentage { get; set; }

        public List<ItemSubstitution> Substituions { get; set; }



        public bool IsLocked
        {
            get
            {
                return this.Transactions?.Any(t => t.IsLocked) ?? false;
            }
        }
    }
}
