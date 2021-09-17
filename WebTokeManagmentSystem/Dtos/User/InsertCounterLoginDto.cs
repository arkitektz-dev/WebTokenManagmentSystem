using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTokenManagmentSystem.Dtos.User
{
    public class InsertCounterLoginDto
    {
        public string UserId { get; set; }
        public string CounterId { get; set; }
        public DateTime Login { get; set; }
    }
}
