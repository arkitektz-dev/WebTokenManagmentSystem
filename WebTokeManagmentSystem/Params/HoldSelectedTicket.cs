using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTokenManagmentSystem.Params
{
    public class HoldSelectedTicket
    {
        [Required]
        public string TokenNumber { get; set; }

    }
}
