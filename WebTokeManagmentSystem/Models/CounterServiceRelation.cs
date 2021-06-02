using System;
using System.Collections.Generic;

#nullable disable

namespace WebTokenManagmentSystem.Models
{
    public partial class CounterServiceRelation
    {
        public int Id { get; set; }
        public int CounterTypeId { get; set; }
        public int? ServiceMasterId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual CounterType CounterType { get; set; }
        public virtual ServiceMaster ServiceMaster { get; set; }
    }
}
