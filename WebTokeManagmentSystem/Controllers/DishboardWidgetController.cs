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
using WebTokenManagmentSystem.Dtos.DishboardWidget;
using System.Diagnostics;

namespace WebTokenManagmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishboardWidgetController : ControllerBase
    {
        private readonly WebTokenManagmentSystemDBContext context;
        private IConfiguration config;
        private readonly ITokenHelper tokenHelper;
        private readonly ITokenBLL tokenBLL;

        public DishboardWidgetController(WebTokenManagmentSystemDBContext _context,
            IConfiguration _config,
            ITokenHelper _tokenHelper,
            ITokenBLL _tokenBLL)
        {
            config = _config;
            context = _context;
            tokenHelper = _tokenHelper;
            tokenBLL = _tokenBLL;
        }

        /// <summary>
        /// list counter service
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("TopCardValues")]
        public IActionResult TopCardValues()
        {
            var ActiveCounter = context
                 .UserCounterHistories
                 .Where(x => x.CreatedDate.Value.Date == DateTime.Now.Date)
                 .ToList();

            var TicketIssued = context
                .Tokens
                .Where(x => x.CreatedDate.Value.Date.Date == DateTime.Now.Date)
                .ToList();

            var IssuedResolved = context
                .TokenStatusHistories
                .Where(x => x.Status == (byte)GlobalEnums.Status.Complete
                       && x.CreatedDate.Value.Date == DateTime.Now.Date)
                .ToList();

            var WaitingList = context
                .TokenStatusHistories
                .Where(x => x.CreatedDate.Value.Date == DateTime.Now.Date
                 && x.Status == (byte)GlobalEnums.Status.Serving && x.Status != (byte)GlobalEnums.Status.Complete)
                .ToList();

            var WaitingCount = WaitingList.Count();


            foreach (var item in WaitingList) {
                bool isTicketFound = IssuedResolved.Where(x => x.TokenId == item.TokenId).Count() > 0;
                if (isTicketFound == true) {
                    WaitingCount = WaitingCount - 1;
                }


            }


            var message = new QueueCardDto()
            {
                ActiveCounter = ActiveCounter.Count().ToString(),
                IssuedResolved = IssuedResolved.Count().ToString(),
                TicketIssued = TicketIssued.Count().ToString(),
                Waiting = WaitingCount.ToString()
            };

            return Ok(message);
        }

        /// <summary>
        /// Get Chart Details
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("GetChartDetails")]
        public IActionResult GetChartDetails()
        {

            var list = context.Tokens.ToList().Take(7).Select(x => x.CreatedDate.Value.Date).Distinct();
            //Total
            List<int> TotalMaster = new List<int>();

            foreach (var item in list) {

                var isTotalFound = context.Tokens.Where( 
                    x =>
                    x.CreatedDate.Value.Date == item.Date)
                    .ToList();

                if (isTotalFound.Count() > 0) {
                    TotalMaster.Add(isTotalFound.Count());
                }

            };


            //Success Tickets
            List<int> SuccessTickets = new List<int>();

            foreach (var item in list)
            {
                var isSuccessTicketFound = context.Tokens.Where(
                    x => x.Status == (byte)GlobalEnums.Status.Complete &&
                    x.CreatedDate.Value.Date == item.Date).ToList();

                if (isSuccessTicketFound.Count() > 0) {
                    SuccessTickets.Add(isSuccessTicketFound.Count());
                }
                else
                {
                    SuccessTickets.Add(0);
                }

            }

            List<int> PendingTickets = new List<int>();
            foreach (var item in list)
            {
                var isPendingTicktsFound = context.Tokens.Where(
                    x => x.Status == (byte)GlobalEnums.Status.Pending &&
                    x.CreatedDate.Value.Date == item.Date).ToList();

                if (isPendingTicktsFound.Count() > 0)
                {
                    PendingTickets.Add(isPendingTicktsFound.Count());
                }
                else {
                    PendingTickets.Add(0);
                }

            }

            List<string> MonthByName = new List<string>();
            foreach (var item in list)
            {
                MonthByName.Add(item.Day.ToString());
            }

            var return_message = new ChartDto()
            {
                Month = MonthByName,
                Pending = PendingTickets,
                Success = SuccessTickets,
                Total = TotalMaster

            };

            return Ok(return_message);
        }

        









    }
}
