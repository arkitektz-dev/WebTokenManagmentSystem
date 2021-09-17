using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTokenManagmentSystem.Params
{
    public class InsertCounterLoginBody
    {
        public string UserId { get; set; }
        public int? CounterId { get; set; } 
        public bool AuthStatus { get; set; }
    }
}
