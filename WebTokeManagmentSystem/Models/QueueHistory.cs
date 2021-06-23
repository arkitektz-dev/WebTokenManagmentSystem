using System;
using System.Collections.Generic;

#nullable disable

namespace WebTokenManagmentSystem.Models
{
    public partial class QueueHistory
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
