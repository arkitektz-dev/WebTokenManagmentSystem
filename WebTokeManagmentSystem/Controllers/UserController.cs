using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebTokenManagmentSystem.Models;
using WebTokenManagmentSystem.Authentication.enums;
using WebTokenManagmentSystem.Authentication.Params;
using WebTokenManagmentSystem.Dtos.Token;
using System.Text.RegularExpressions;
using WebTokenManagmentSystem.Helper;
using WebTokenManagmentSystem.BLL;
using WebTokenManagmentSystem.Params;
using WebTokenManagmentSystem.LINQExtension;
using WebTokenManagmentSystem.Service;
using Microsoft.Extensions.Hosting;

namespace WebTokenManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly WebTokenManagmentSystemDBContext context;
        private IConfiguration config;
        private readonly ITokenHelper tokenHelper;
        private readonly ITokenBLL tokenBLL;
        private readonly TicketSpeaker _ticketSpeaker;
        public static bool isServiceRunning = true;

        public UserController(TicketSpeaker hostedService, WebTokenManagmentSystemDBContext _context, IConfiguration _config, ITokenHelper _tokenHelper, ITokenBLL _tokenBLL)
        {
            config = _config;
            context = _context;
            tokenHelper = _tokenHelper;
            tokenBLL = _tokenBLL;
            _ticketSpeaker = hostedService;
        }


        /// <summary>
        /// Get User List
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("Get-User-List")]
        public IActionResult GetUserList(string Role)
        {
            if (String.IsNullOrEmpty(Role))
                return Ok(context.AspNetUsers.ToList());

            var list = context.AspNetUsers.ToList();

            List<AspNetUser> ListUser = new List<AspNetUser>();
            foreach (var item in list)
            {
                var isUserInRole = context
                     .AspNetUserRoles
                     .Where(x => x.UserId == item.Id && x.RoleId == Role)
                     .ToList()
                     .Count() > 0;

                if (isUserInRole == true) {
                    ListUser.Add(item);
                }

            }

            var message = list;

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
        /// Get User Role List
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("Get-Role-List")]
        public IActionResult GetRoleList()
        {
            var message = context.AspNetRoles.ToList();

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
        /// Get User Role List
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("Get-Counter-Details")]
        public IActionResult GetCounterDetails()
        {

            var list = context.Counters.ToList();

            var listCounterToken = context
                    .CounterTokenRelations.Where(x => x.StatusId == (byte)GlobalEnums.Status.Complete)
                    .ToList();

            List<CounterValueDTO> Master = new List<CounterValueDTO>();
            foreach (var item in list)
            {
                CounterValueDTO row = new CounterValueDTO();

                row.Counter = item.Number.ToString();
                row.CompletedTickets = listCounterToken.Where(x => x.CounterId == item.Id).ToList().Count().ToString();


                Master.Add(row);


            }

            var message = Master;

            if (message != null)
            {
                return Ok(message);
            }
            else
            {
                return NotFound();
            }
        }

        class CounterValueDTO
        {
            public string Counter { get; set; }
            public string CompletedTickets { get; set; }
        }





    }
}
