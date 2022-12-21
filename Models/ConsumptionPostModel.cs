using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Web.Models
{
    public class ConsumptionPostModel
    {
        public string Line { get; set; }
        public string OrderNumber { get; set; }

        public bool? ForceAllToWaste { get; set; }

        public DateTime JobStartDate { get; set; }
    }
}
