﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTokenManagmentSystem.Authentication.Params
{
    public class CounterTypeBody
    {
        [Required]
        public List<string> Types { get; set; }
    }
}
