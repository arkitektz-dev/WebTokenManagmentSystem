using System;
using System.Collections.Generic;

#nullable disable

namespace WebTokenManagmentSystem.Models
{
    public partial class CounterType
    {
        public CounterType()
        {
            CounterServiceRelations = new HashSet<CounterServiceRelation>();
        }

        public int Id { get; set; }
        public string CounterName { get; set; }

        public virtual ICollection<CounterServiceRelation> CounterServiceRelations { get; set; }
    }
}
