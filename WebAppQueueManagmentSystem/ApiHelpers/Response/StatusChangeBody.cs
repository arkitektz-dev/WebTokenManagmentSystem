using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppQueueManagmentSystem.ApiHelpers.Response
{
    public class StatusChangeBody
    {
        public string TokenNumber { get; set; }

        public byte Status { get; set; }
    }
}