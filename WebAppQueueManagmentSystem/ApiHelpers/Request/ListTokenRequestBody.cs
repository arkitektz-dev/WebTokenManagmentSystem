﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppQueueManagmentSystem.ApiHelpers.Request
{
    public class ListTokenRequestBody
    {
       public int? token_status { get; set; } 
       public int? customer_Type { get; set; }
    }
}