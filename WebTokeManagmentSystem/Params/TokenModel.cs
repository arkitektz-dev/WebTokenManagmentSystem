using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTokenManagmentSystem.Dtos.Token
{
    public class TokenModel
    {
        [Required(ErrorMessage = "Customer type is required")]
        public string CustomerType { get; set; }
    }
}
