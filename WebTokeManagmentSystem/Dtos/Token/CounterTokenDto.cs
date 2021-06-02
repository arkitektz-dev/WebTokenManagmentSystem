using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTokenManagmentSystem.Dtos.Token
{
    public class CounterTokenDto
    {
        public int? CounterId { get; set; }
        public int? TokenId { get; set; }
        public int StatusId { get; set; }
    }
}
