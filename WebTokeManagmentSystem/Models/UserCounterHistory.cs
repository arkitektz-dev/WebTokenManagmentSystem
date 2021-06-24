using System;
using System.Collections.Generic;

#nullable disable

namespace WebTokenManagmentSystem.Models
{
    public partial class UserCounterHistory
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int? CounterId { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
