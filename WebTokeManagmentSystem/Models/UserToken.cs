using System;
using System.Collections.Generic;

#nullable disable

namespace WebTokenManagmentSystem.Models
{
    public partial class UserToken
    {
        public int Id { get; set; }
        public int? TokenId { get; set; }
        public string UserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? TokenNumber { get; set; }

        public virtual Token Token { get; set; }
        public virtual AspNetUser User { get; set; }
    }
}
