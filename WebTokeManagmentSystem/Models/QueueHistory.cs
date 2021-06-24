﻿using System;
using System.Collections.Generic;

#nullable disable

namespace WebTokenManagmentSystem.Models
{
    public partial class QueueHistory
    {
        public int Id { get; set; }
        public int? CounterId { get; set; }
        public string Announcement { get; set; }
        public string TokenNumber { get; set; }
        public bool? IsPlayed { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
