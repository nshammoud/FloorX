using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Web.Models
{
    public class ItemSubstitution
    {
        public string ItemNo { get; set; }

        public string SubstituteItemNo { get; set; }

        public string Description { get; set; }

        public decimal? ReworkPercentage { get; set; }
    }
}
