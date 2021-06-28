using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppQueueManagmentSystem.ApiHelpers.Response
{
    public class QueueCardBody
    {
        public string ActiveCounter { get; set; }
        public string TicketIssued { get; set; }
        public string IssuedResolved { get; set; }
        public string Waiting { get; set; }
    }
}