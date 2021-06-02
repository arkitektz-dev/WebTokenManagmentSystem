using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTokeManagmentSystem.Authentication
{
    public class LoginModel
    {
        //[Required(ErrorMessage = "User Name is required")]
        //public string Username { get; set; }

        [Required(ErrorMessage = "Email name is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
