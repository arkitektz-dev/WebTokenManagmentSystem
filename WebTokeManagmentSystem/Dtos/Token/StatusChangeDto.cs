using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTokenManagmentSystem.Dtos.Token
{
    public class StatusChangeDto
    {
        public string TokenNumber { get; set; }

        public byte? Status { get; set; }
    }
}
