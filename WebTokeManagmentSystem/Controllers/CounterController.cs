using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTokenManagmentSystem.Authentication.Params;
using WebTokenManagmentSystem.BLL;
using WebTokenManagmentSystem.Helper;
using WebTokenManagmentSystem.Models;

namespace WebTokenManagmentSystem.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CounterController : ControllerBase
    {
        private readonly WebTokenManagmentSystemDBContext context;
        private IConfiguration config;
        private ICounterHelper counter;
        private ICounterBLL counterBLL;

        public CounterController(WebTokenManagmentSystemDBContext _context, IConfiguration _config, ICounterHelper _counter, ICounterBLL _counterBLL)
        {
            config = _config;
            context = _context;
            counter = _counter;
            counterBLL = _counterBLL;

        }


        /// <summary>
        /// Get Compelete Detail of counter
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Route("Get_Counter_Detail_By_UserId")]
        public IActionResult Get_Counter_Detail_By_UserId([FromBody] CounterDetailBody model)
        {
            var message = counterBLL.GetCounterDetailByUserId(model);

            if (message != null)
            {
                return Ok(message);
            }
            else
            {
                return NotFound();
            }

        }

        /// <summary>
        /// Every Counter Status
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("List-Counter")]
        public IActionResult List_Counter()
        {
            var message = counterBLL.ListCounter();

            if (message != null)
            {
                return Ok(message);
            }
            else
            {
                return NotFound();
            }

        }

        /// <summary>
        /// View Counter Activity List
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("View-Counter-Activity")]
        public IActionResult View_Counter_Activity(int? CounterId)
        {
            var message = counterBLL.ViewCounterActivity(CounterId);

            if (message != null)
            {
                return Ok(message);
            }
            else
            {
                return NotFound();
            }

        }


      


    }
}
