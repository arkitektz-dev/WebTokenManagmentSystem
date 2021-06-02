using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppQueueManagmentSystem.Hubs
{
    public class TicketHub : Hub
    {
        public static void TicketBroadCast(string TokenNumber)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<TicketHub>();
            context.Clients.All.getNewTicket(TokenNumber);
        }

        
    }
}