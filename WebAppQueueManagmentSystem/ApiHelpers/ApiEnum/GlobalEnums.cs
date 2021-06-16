using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppQueueManagmentSystem.ApiHelpers.ApiEnum
{
    public static class GlobalEnums
    {
        public enum Status
        {
            Pending = 1, Complete = 2, Invalid_Status = 3, Serving = 4, All = 5
        }

        public enum CustomerType
        { 
           Customer = 1, Non_Customer = 2, All_Customer = 3
        }
    }

   

}
