using System;
using System.Collections.Generic;

#nullable disable

namespace WebTokenManagmentSystem.Models
{
    public partial class TokenStatusHistory
    {
        public int Id { get; set; }
        public byte? Status { get; set; }
        public int? TokenId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Token Token { get; set; }
    }
}
