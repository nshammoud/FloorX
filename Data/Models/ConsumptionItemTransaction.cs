using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Data.Models
{
    public class ConsumptionItemTransaction
    {
        public long Id { get; set; }

        public string ProductionOrderNumber { get; set; }

        public string ContainerNumber { get; set; }

        public string ParentItemNumber { get; set; }

        public string ItemNumber { get; set; }

        public string ItemDescription { get; set; }

        public string LotNumber { get; set; }

        public string UserId { get; set; }

        public decimal Quantity { get; set; }

        public decimal UserEnteredQuantity { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsPosted { get; set; }

        public DateTime? DatePosted { get; set; }

        public string MixerId { get; set; }

        public string LocationCode { get; set; }

        public string UoM { get; set; }

        public string PostedByUserId { get; set; }

        public bool IsWaste { get; set; }

        public bool IsLocked { get; set; }

        public string RunNumber { get; set; }
    }
}
