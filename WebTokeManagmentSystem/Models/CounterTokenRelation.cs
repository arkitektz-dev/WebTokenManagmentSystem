using System;
using System.Collections.Generic;

#nullable disable

namespace WebTokenManagmentSystem.Models
{
    public partial class CounterTokenRelation
    {
        public int Id { get; set; }
        public int? CounterId { get; set; }
        public int? TokenId { get; set; }
        public int? StatusId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Counter Counter { get; set; }
        public virtual Status Status { get; set; }
        public virtual Token Token { get; set; }
    }
}
