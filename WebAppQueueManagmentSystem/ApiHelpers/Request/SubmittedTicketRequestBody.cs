using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppQueueManagmentSystem.ApiHelpers.Request
{
    public class SubmittedTicketRequestBody
    {
        public string TokenNumber { get; set; }

        public byte? StatusId { get; set; }

        public int? ServiceOptionId { get; set; }

        public string Comment { get; set; }

    }
}