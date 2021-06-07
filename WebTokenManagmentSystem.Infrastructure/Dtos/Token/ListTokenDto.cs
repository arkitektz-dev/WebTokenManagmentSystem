using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTokenManagmentSystem.Dtos.Token
{
    public class ListTokenDto
    {
        public string Token { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public bool? isCustomer { get; set; }
    }
}
