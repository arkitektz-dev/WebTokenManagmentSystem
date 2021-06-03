using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppQueueManagmentSystem.ApiHelpers.Response
{
    public class CounterDetailBody
    {
        public int CounterNumber { get; set; }

        public List<Status> CounterStatus { get; set; }

        public List<ServiceOption> CounterService { get; set; }

        public int CounterID { get; set; }

    }

    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class ServiceOption
    {
        public int Id { get; set; }
        public int? ServiceMasterId { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}