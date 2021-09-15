﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTokenManagmentSystem.Authentication.enums
{
    public static class GlobalEnums
    {
        public enum Status
        {
            Pending = 1, Complete = 2, Invalid_Status = 3, Serving = 4, All = 5, Skip = 6, Hold = 7
        }

        public enum CustomerType
        { 
           Customer = 1, Non_Customer = 2, All_Customer = 3
        }
    }

   

}
