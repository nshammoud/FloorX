using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.NavServices
{
    public class NavServiceConfig
    {
        public string Host { get; set; }

        public int? CacheExpiration { get; set; }

        public Dictionary<string, string> Endpoints { get; set; }

    }
}
