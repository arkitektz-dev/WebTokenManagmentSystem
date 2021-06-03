using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppQueueManagmentSystem.Models;

namespace WebAppQueueManagmentSystem.Hubs
{
    public class TicketHub : Hub
    {
        public static void TicketBroadCast(Token TokenDetail)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<TicketHub>();
            context.Clients.All.getNewTicket(TokenDetail);
        }

        
    }
}