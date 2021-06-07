using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTokenManagmentSystem.Authentication.Params
{
    public class CounterRelationBody
    {
        [Required]
        public int CounterTypeID { get; set; }

        [Required]
        public int ServiceMasterID { get; set; }

    }
}
