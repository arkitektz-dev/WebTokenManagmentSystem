using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppQueueManagmentSystem.ApiHelpers.Response
{
    public class CounterValueBody
    {
        public string Counter { get; set; }
        public string CompletedTickets { get; set; }
    }
}