using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTokenManagmentSystem.Dtos.Token
{
    public class TokenDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string TokenNumber { get; set; }
    }
}
