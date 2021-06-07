using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTokenManagmentSystem.Authentication.Params
{
    public class CounterBody
    {
        public string Name { get; set; }
        public int? CounterServiceRelationID { get; set; }
        public string Description { get; set; }

        public string UserID { get; set; }
    }
}
