using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTokenManagmentSystem.Authentication.Params
{
    public class CounterTokenBody
    {
        [Required]
        public int? CounterId { get; set; }
      
        [Required]
        public string TokenNumber { get; set; }
       
        [Required]
        public int StatusId { get; set; }

        public string ServiceType { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public DateTime ServingTime { get; set; }



    }
}
