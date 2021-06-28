using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTokenManagmentSystem.Dtos.DishboardWidget
{
    public class QueueCardDto
    {
        public string ActiveCounter { get; set; }
        public string TicketIssued { get; set; }
        public string IssuedResolved { get; set; }
        public string Waiting { get; set; }
    }
}
