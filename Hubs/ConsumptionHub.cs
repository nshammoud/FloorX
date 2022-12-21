using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace KQF.Floor.Web.Hubs
{
    public class ConsumptionHub : Hub
    {
        public async Task JobUpdated(string user, string jobNumber, string mixer)
        {
            await Clients.All.SendAsync("JobUpdateNotification", user, jobNumber, mixer);
        }

        public async Task JobPosted(string user, string jobNumber, string mixer)
        {
            await Clients.All.SendAsync("JobPostedNotification", user, jobNumber, mixer);
        }

        public async Task TestMsg(string message)
        {
            await Clients.All.SendAsync("TestMsg", message);
        }
    }
}
