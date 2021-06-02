using System;
using System.Collections.Generic;

#nullable disable

namespace WebTokenManagmentSystem.Models
{
    public partial class Status
    {
        public Status()
        {
            CounterTokenRelations = new HashSet<CounterTokenRelation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<CounterTokenRelation> CounterTokenRelations { get; set; }
    }
}
