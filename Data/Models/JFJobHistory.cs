using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Data.Models
{
    [Keyless]
    public class JFJobHistory
    {
        public string JobNo { get; set; }
        public string ItemNo { get; set; }
        public string LotNo { get; set; }
        public Decimal Qty { get; set; }
        public string ContainerNo { get; set; }
        public string User { get; set; }
        public string SrcSubType { get; set; }
        public DateTime CreateDateTime { get; set; }

        public string ItemCategoryCode { get; set; }

        [NotMapped]
        public string CreateDateStr
        {
            get
            {
                return CreateDateTime.ToLocalTime().ToString("MM/dd HH:mm");
            }
        }
    }
}
