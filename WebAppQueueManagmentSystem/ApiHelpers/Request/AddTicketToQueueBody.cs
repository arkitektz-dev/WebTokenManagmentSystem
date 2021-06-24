using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppQueueManagmentSystem.ApiHelpers.Request
{
    public class AddTicketToQueueBody
    {
        [Required]
        public string TokenNumber { get; set; }
        
        [Required]
        public int CounterId { get; set; }
   
    }
}
