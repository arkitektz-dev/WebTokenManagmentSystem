using System;
using System.Collections.Generic;

#nullable disable

namespace WebTokenManagmentSystem.Models
{
    public partial class CounterHistory
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int? CounterId { get; set; }
        public DateTime? Login { get; set; }
        public DateTime? Logout { get; set; }
    }
}
