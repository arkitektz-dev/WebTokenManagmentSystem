using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTokenManagmentSystem.Authentication.Params
{
    public class StatusChangeModel
    {
        [Required(ErrorMessage = "Token number is required")]
        public string TokenNumber { get; set; }

        [Required(ErrorMessage = "Status number is required")]
        public byte Status { get; set; }



    }

}
