using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebTokenManagmentSystem.Models;

namespace WebTokenManagmentSystem.Authentication.Params
{
    public class ServiceBody
    {
        [Required]
        public string ServiceName { get; set; }

        [Required]
        public List<string> ListOption { get; set; }
    }
}
