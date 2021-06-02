using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTokenManagmentSystem.BLL;
using WebTokenManagmentSystem.Helper;
using WebTokenManagmentSystem.Models;

namespace WebTokenManagmentSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CounterController : ControllerBase
    {
        private readonly WebTokenManagmentSystemDBContext context;
        private IConfiguration config;
        private ICounterHelper counter;
 

        public CounterController(WebTokenManagmentSystemDBContext _context, IConfiguration _config, ICounterHelper _counter)
        {
            config = _config;
            context = _context;
            counter = _counter;

        }


    }
}
