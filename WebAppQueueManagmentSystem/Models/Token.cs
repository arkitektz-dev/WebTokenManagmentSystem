using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppQueueManagmentSystem.Models
{
    public class Token
    {
        public string token { get; set; }

        public string date { get; set; }

        public string time { get; set; }
        public string CustomTokenNumber { get; set; }
        public int Id { get; set; }
        public int? ServiceOptionId { get; set; }
        public int? TokenNumber { get; set; }
        public byte? Status { get; set; }
        public bool? IsCustomer { get; set; }
        public string Comment { get; set; } 
    }
}