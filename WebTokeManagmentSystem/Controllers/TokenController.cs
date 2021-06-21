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

namespace WebTokeManagmentSystem.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly WebTokenManagmentSystemDBContext context;
        private IConfiguration config;
        private readonly ITokenHelper tokenHelper;
        private readonly ITokenBLL tokenBLL;

        public TokenController(WebTokenManagmentSystemDBContext _context, IConfiguration _config, ITokenHelper _tokenHelper, ITokenBLL _tokenBLL)
        {
            config = _config;
            context = _context;
            tokenHelper = _tokenHelper;
            tokenBLL = _tokenBLL;
        }


        /// <summary>
        /// Generate new token based on customer type
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Route("Generate-Customer-Token")]
        public IActionResult Generate_Customer_Token([FromBody] TokenModel model)
        {
            var message = tokenBLL.AddNewToken(model);

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
        /// List All Token based on status
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("List-Token")]
        public IActionResult List_Token(int? token_status, int? customer_Type)
        {
            var message = tokenBLL.ListToken(token_status, customer_Type);

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
        /// assign token based on user id
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Assign-Token-By-User-Id")]
        public IActionResult Assign_Token_By_User_Id([FromBody] UserTokenModel model)
        {
            var message = tokenBLL.AssignToken(model);

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
        /// changes token status based on user id
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Change-Token-Status-By-Token-Number")]
        public IActionResult Change_Token_Status_By_Token_Number([FromBody] StatusChangeModel model)
        {
            var message = tokenBLL.ChangeTokenStatus(model);

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
        /// Add New Serivce
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Add-Service")]
        public IActionResult Add_Service([FromBody] ServiceBody model)
        {
            var message = tokenBLL.AddService(model);

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
        /// Add new counter
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Add-Counter-Type")]
        public IActionResult Add_Counter_Type([FromBody] CounterTypeBody model)
        {
            var message = tokenBLL.AddCounterType(model);

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
        /// Assign Counter to Service
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Assign-Counter-Service-Relation")]
        public IActionResult Assign_Counter_Service_Relation([FromBody] CounterRelationBody model)
        {
            var message = tokenBLL.AssignCounterServiceRelation(model);

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
        /// Add Counter
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Add-Counter")]
        public IActionResult Add_Counter([FromBody] CounterBody model)
        {
            var message = tokenBLL.AddCounter(model);

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
        /// Add Counter
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Assign-Ticket-To-Counter")]
        public IActionResult Assign_Ticket_To_Counter([FromBody] CounterTokenBody model)
        {
            var message = tokenBLL.AssignTicketToCounter(model);

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
        /// Add Counter
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Counter-Submit-Ticket")]
        public IActionResult Counter_Submit_Ticket([FromBody] CompleteTicketBody model)
        {
            var message = tokenBLL.SubmittedTicket(model);

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
        /// List Assign Token to counter
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("List-Counter-Token")]
        public IActionResult List_Counter_Token()
        {
            var message = tokenBLL.ListCounterToken();

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
        /// List Assign Token to counter
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Get-Token-Status")]
        public IActionResult Get_Token_Status([FromBody] TokenStatusBody model)
        {
            var message = tokenBLL.GetTokenStatus(model);

            if (message != null)
            {
                return Ok(message);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("Get-Pending-Token-By-CounterId")]
        public IActionResult Get_Pending_Token_By_CounterId([FromBody] GetPendingTokenBody model)
        {
            var message = tokenBLL.GetPendingTokenByCounterId(model);

            if (message != null)
            {
                return Ok(message);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("Get-Status-list")]
        public IActionResult Get_Status_list()
        {
            var message = context.Statuses.Select(x => new
            {
                x.Id,
                x.Name
            });

            if (message != null)
            {
                return Ok(message);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet]
        [Route("Token-Filter")]
        public IActionResult Token_Filter(DateTime TicketDate, int TicketStatus, int CustomerType)
        {
            List<CurrentCounterTokenDto> Master = new List<CurrentCounterTokenDto>();

            var TodayTokenList = context
                .Tokens
                .Where(x => 
                x.CreatedDate.Value.Date == TicketDate.Date
                && x.Status == TicketStatus)
                .WhereIf(CustomerType == 1, x => x.IsCustomer == true)
                .WhereIf(CustomerType == 2, x => x.IsCustomer == false)
                .ToList();

            foreach (var item in TodayTokenList) {
                CurrentCounterTokenDto row = new CurrentCounterTokenDto();
                var CounterNumber = context.CounterTokenRelations.Where(x => x.TokenId == item.Id).FirstOrDefault();
                

                row.TokenId = item.Id;
                row.TokenNumber = item.CustomTokenNumber;
                if (CounterNumber != null)
                {
                    row.CounterId = (int)CounterNumber.CounterId;
                }
                row.TicketDate = (DateTime)item.CreatedDate;

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
        /// Get Average Time
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("Get-Average-Time")]
        public IActionResult Get_Average_Time(int? CounterId)
        {
            var message = tokenBLL.GetAverageTime();

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
