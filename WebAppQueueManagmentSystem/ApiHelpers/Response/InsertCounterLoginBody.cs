using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppQueueManagmentSystem.ApiHelpers.Response
{
    public class InsertCounterLoginBody
    {
        public string UserId { get; set; }
        public string CounterId { get; set; }
        public DateTime Login { get; set; }
    }
}