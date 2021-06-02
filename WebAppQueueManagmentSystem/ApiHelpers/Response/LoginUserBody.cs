using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppQueueManagmentSystem.ApiHelpers.Response
{
    public class LoginUserBody
    {
        public string token { get; set; }
        public string expiration { get; set; }
    }
}