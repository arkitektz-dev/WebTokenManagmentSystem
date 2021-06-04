using System;
using System.Collections.Generic;

#nullable disable

namespace WebTokenManagmentSystem.Models
{
    public partial class Token
    {
        public Token()
        {
            CounterTokenRelations = new HashSet<CounterTokenRelation>();
            TokenStatusHistories = new HashSet<TokenStatusHistory>();
            UserTokens = new HashSet<UserToken>();
        }

        public int Id { get; set; }
        public int? ServiceOptionId { get; set; }
        public int? TokenNumber { get; set; }
        public byte? Status { get; set; }
        public bool? IsCustomer { get; set; }
        public string Comment { get; set; }
        public string CustomTokenNumber { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CompleteDate { get; set; }

        public virtual ServiceOption ServiceOption { get; set; }
        public virtual ICollection<CounterTokenRelation> CounterTokenRelations { get; set; }
        public virtual ICollection<TokenStatusHistory> TokenStatusHistories { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }
    }
}
