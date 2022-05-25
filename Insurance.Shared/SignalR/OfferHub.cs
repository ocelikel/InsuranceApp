using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Shared.SignalR
{
    public class OfferHub : Hub
    {
        public Task SendUpdate(Guid correlationId, string message)
        {
            return Clients.All.SendAsync("SEND_OFFER", message);
        }
    }
}
