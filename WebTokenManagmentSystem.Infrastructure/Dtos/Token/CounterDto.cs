using System;   
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTokenManagmentSystem.Dtos.Token
{
    public class CounterDto
    {

        public string CounterUserId { get; set; }

        public int? Csrid { get; set; }

        public int? Number { get; set; }

        public string Description { get; set; }
    }
}
