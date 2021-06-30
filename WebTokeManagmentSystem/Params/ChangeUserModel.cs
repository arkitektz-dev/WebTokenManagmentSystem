using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTokenManagmentSystem.Params
{
    public class ChangeUserModel
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public int? CSRID { get; set; }
    }
}
