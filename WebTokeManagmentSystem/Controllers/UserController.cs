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
using WebTokenManagmentSystem.Dtos.User;
using WebTokeManagmentSystem.Authentication;

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
            var UserList = context.AspNetUsers.ToList();
            var UserRoleList = context.AspNetUserRoles.ToList();
            var RoleList = context.AspNetRoles.ToList();

            List<UserListDto> Master = new List<UserListDto>();
            foreach (var item in UserList)
            {
                UserListDto row = new UserListDto();

                var isUserRoleFound = UserRoleList.Where(x => x.UserId == item.Id).ToList();

                if (isUserRoleFound.Count() > 0 == false)
                    continue;

                row.Id = item.Id;
                row.Role = isUserRoleFound.Select(x => x.Role.Name).FirstOrDefault();
                row.Email = item.Email;
 
                Master.Add(row);
            }



            var message = Master.ToList();

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
            var list = context.AspNetRoles.ToList();

            List<UserRoleDto> Master = new List<UserRoleDto>();

            foreach (var item in list)
            {
                UserRoleDto row = new UserRoleDto();
                row.Id = item.Id;
                row.UserName = item.Name;
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

        /// <summary>
        /// Get Cashier Type
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("Get-Counter-Type")]
        public IActionResult GetCounterType()
        {

            var list = context.ServiceMasters.ToList();

            List<CashierTypeDto> MasterList = new List<CashierTypeDto>();
            foreach(var item in list)
            {
                CashierTypeDto row = new CashierTypeDto();
                row.ID = item.Id;
                row.Name = item.ServiceName;

                MasterList.Add(row);
                
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
        /// Generate new token based on customer type
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Route("Change-User-Role")]
        public IActionResult Change_User_Role([FromBody] ChangeUserModel model)
        {
            var Roles = context.AspNetRoles.Where(x => x.Id == model.RoleId).FirstOrDefault();

            if (Roles != null)
            {
                if (Roles.Name == UserRoles.Cashier)
                {
                    //Counter

                    AspNetUserRole row = new AspNetUserRole();
                    row.UserId = model.UserId;
                    row.RoleId = model.RoleId;

                    context.AspNetUserRoles.Add(row);
                    context.SaveChanges();


                    Counter counterRow = new Counter();
                    counterRow.CounterUserId = model.UserId;
                    counterRow.Csrid = context.CounterServiceRelations.Where(x => x.ServiceMasterId == model.CSRID).FirstOrDefault().Id;
                    counterRow.Number = context.Counters.Select(x => x.Number).Max() + 1;

                    context.Counters.Add(counterRow);
                    context.SaveChanges();


                    return Ok(model);
                }

                if (Roles.Name == UserRoles.Admin)
                {
                    //Admin

                    AspNetUserRole row = new AspNetUserRole();
                    row.UserId = model.UserId;
                    row.RoleId = model.RoleId;

                    context.AspNetUserRoles.Add(row);
                    context.SaveChanges();

                    return Ok(model);
                }


            }
            else {
                return NotFound();
            }

            return NotFound();


        }

      


    }
}
