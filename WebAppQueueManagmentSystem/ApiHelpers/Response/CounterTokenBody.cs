using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppQueueManagmentSystem.ApiHelpers.Response
{
    public class CounterTokenBody
    {
        public int? CounterId { get; set; }
        public string TokenNumber { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public string ServiceType { get; set; }
        public DateTime ServingTime { get; set; }
    }
}