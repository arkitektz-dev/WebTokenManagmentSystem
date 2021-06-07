using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTokenManagmentSystem.Models;

namespace WebTokenManagmentSystem.Dtos.Counter
{
    public class CounterDetailDto
    {
        public int CounterNumber { get; set; }

        public List<Status> CounterStatus { get; set;}

        public List<ServiceOption> CounterService { get; set; }

        public int CounterID { get; set; }


    }
}
