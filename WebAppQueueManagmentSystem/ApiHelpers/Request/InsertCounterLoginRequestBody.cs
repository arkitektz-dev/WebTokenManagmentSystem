using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppQueueManagmentSystem.ApiHelpers.Request
{
    public class InsertCounterLoginRequestBody
    {
        public string UserId { get; set; }
        public int? CounterId { get; set; }
        public bool AuthStatus { get; set; }
    }
}