using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTokenManagmentSystem.Authentication.enums;
using WebTokenManagmentSystem.Authentication.Params;
using WebTokenManagmentSystem.Dtos.Counter;
using WebTokenManagmentSystem.Models;

namespace WebTokenManagmentSystem.BLL
{
    public class ServiceBLL : IServiceBLL
    {
        private readonly WebTokenManagmentSystemDBContext context;
        private IConfiguration config;

        public ServiceBLL(WebTokenManagmentSystemDBContext _context, IConfiguration _config)
        {
            config = _config;
            context = _context;
        }

         





    }
}
