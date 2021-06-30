using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppQueueManagmentSystem.ApiHelpers.Request
{
    public class ChangeUserRequestBody
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public int? CSRID { get; set; }
    }
}