using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Web.Models
{
    public class OutputPostModel
    {
        public string JobNumber { get; set; }

        public string MixerLine { get; set; }

        public string ContainerNo { get; set; }

        public decimal Quantity { get; set; }

        public decimal? BolosOrRods { get; set; }

        public bool UseCartId { get; set; }

        public bool MixerLineRequired { get; set; }
    }
}
