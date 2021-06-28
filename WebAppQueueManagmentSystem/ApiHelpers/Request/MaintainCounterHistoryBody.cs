using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppQueueManagmentSystem.ApiHelpers.Request
{
    public class MaintainCounterHistoryBody
    {
        public string UserID { get; set; }

        public int CounterId { get; set; }
    }
}