using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTokenManagmentSystem.Authentication.Params
{
    public class UserTokenModel
    {
        public int? TokenId { get; set; }

        [Required(ErrorMessage = "User Id is required")]
        public string UserId { get; set; }
        
        [Required(ErrorMessage = "Token number is required")]
        public string TokenNumber { get; set; }

    }
}
