using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Web.Models
{
    public class OutputItemNumberValidation
    {

        public string ItemCategoryCode { get; set; }

        public string JobItemNumber { get; set; }

        public string ScannedItemNumber { get; set; }

        public bool? IsValid { get; set; }
    }
}
