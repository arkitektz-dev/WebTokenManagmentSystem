using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppQueueManagmentSystem.ApiHelpers.Request
{
    public class LoginRequestBody
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}