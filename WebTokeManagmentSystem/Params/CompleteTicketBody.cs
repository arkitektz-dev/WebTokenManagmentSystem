using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTokenManagmentSystem.Authentication.Params
{
    public class CompleteTicketBody
    {
        [Required]
        public string TokenNumber { get; set; }

        [Required]
        public byte? StatusId { get; set; }

        [Required]
        public int? ServiceOptionId { get; set; }

        [Required]
        public string Comment { get; set; }


    }
}
