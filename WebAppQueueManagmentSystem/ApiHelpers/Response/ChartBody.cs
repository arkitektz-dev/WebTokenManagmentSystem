using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppQueueManagmentSystem.ApiHelpers.Response
{
    public class ChartBody
    {
        public List<string> Month { get; set; }
        public List<int> Total { get; set; }
        public List<int> Success { get; set; }
        public List<int> Pending { get; set; }
    }
}