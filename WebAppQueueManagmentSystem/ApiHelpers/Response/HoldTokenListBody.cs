using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppQueueManagmentSystem.ApiHelpers.Response
{
    public class HoldTokenListBody
    {
        public string Token { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public bool? isCustomer { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CompleteDate { get; set; }
    }
}