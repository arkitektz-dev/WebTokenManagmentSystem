using System;
using System.Collections.Generic;

namespace WebAppQueueManagmentSystem.ApiHelpers.Response
{
    public partial class Counter
    {
        public int Id { get; set; }
        public string CounterUserId { get; set; }
        public int? Csrid { get; set; }
        public int? Number { get; set; }
        public string Description { get; set; }

    }
}
