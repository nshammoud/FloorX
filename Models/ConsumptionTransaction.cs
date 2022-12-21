using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Web.Models
{
    public class ConsumptionTransaction
    {
        public string JobNumber { get; set; }

        public string ContainerNumber { get; set; }

        public string LotNumber { get; set; }

        public string ItemNumber { get; set; }

        public decimal Quantity { get; set; }

        public string MixerId { get; set; }
    }
}
