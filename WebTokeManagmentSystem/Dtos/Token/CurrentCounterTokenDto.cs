using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTokenManagmentSystem.Dtos.Token
{
    public class CurrentCounterTokenDto
    { 
        public int TokenId { get; set; }
        public string TokenNumber { get; set; }
        public int CounterId { get; set; }
        public string Status { get; set; }
        public DateTime TicketDate { get; set; }

    }
}
