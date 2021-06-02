﻿using System;
using System.Collections.Generic;

#nullable disable

namespace WebTokenManagmentSystem.Models
{
    public partial class ServiceOption
    {
        public int Id { get; set; }
        public int? ServiceMasterId { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual ServiceMaster ServiceMaster { get; set; }
    }
}