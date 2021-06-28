using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTokenManagmentSystem.Dtos.DishboardWidget
{
    public class ChartDto
    {
        public List<string> Month { get; set; }
        public List<int> Total { get; set; }
        public List<int> Success { get; set; }
        public List<int> Pending { get; set; }
    }
}
