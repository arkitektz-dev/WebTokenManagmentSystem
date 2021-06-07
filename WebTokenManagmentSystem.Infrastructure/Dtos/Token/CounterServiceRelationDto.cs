using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTokenManagmentSystem.Dtos.Token
{
    public class CounterServiceRelationDto
    {
        public int Id { get; set; }
        public int CounterTypeId { get; set; }
        public int? ServiceMasterId { get; set; }
    }
}
