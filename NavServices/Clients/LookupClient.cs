using KQF.Floor.NavServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KQF.Floor.Web.NavServices.Lookup
{
    public class LookupClient : NavServices.Clients.NavClientBase<MWSLookupMgmt_Port, LookupClient>, MWSLookupMgmt_Port
    {
        public LookupClient(Microsoft.Extensions.Options.IOptions<NavServiceConfig> config, System.Security.Claims.ClaimsPrincipal user, ILogger<LookupClient> logger) :
         base(config, "MWSLookupMgmt_Port", user, logger)
        {

        }

        public System.Threading.Tasks.Task<ContainerLookup_Result> ContainerLookupAsync(ContainerLookup1 request)
        {
            return base.Channel.ContainerLookupAsync(request);
        }

        public System.Threading.Tasks.Task<BinLookup_Result> BinLookupAsync(BinLookup1 request)
        {
            return base.Channel.BinLookupAsync(request);
        }

        public System.Threading.Tasks.Task<ItemLookup_Result> ItemLookupAsync(ItemLookup1 request)
        {
            return base.Channel.ItemLookupAsync(request);

        }
    }
}
