using System;
using System.Collections.Generic;

#nullable disable

namespace WebTokenManagmentSystem.Models
{
    public partial class Counter
    {
        public Counter()
        {
            CounterTokenRelations = new HashSet<CounterTokenRelation>();
        }

        public int Id { get; set; }
        public string CounterUserId { get; set; }
        public int? Csrid { get; set; }
        public int? Number { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }

        public virtual ICollection<CounterTokenRelation> CounterTokenRelations { get; set; }
    }
}
