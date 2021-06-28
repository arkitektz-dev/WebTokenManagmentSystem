using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTokenManagmentSystem.Dtos.Token
{
    public class AddTicketToQueueDto
    {
        public string TokenNumber { get; set; }

        public int CounterId { get; set; }
    }
}
