using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Web.Models
{
    public class ComponentTransactionModel : Floor.Data.Models.ConsumptionItemTransaction
    {
        public decimal ReworkOverAllocatedQty { get; set; }

        public DateTime JobStartDate { get; set; }
    }
}
