using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppQueueManagmentSystem.ApiHelpers.Response
{
    public class UserListBody
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}