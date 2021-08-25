using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppQueueManagmentSystem.ApiHelpers.Utility;
using WebAppQueueManagmentSystem.BLL.Counter;
using WebAppQueueManagmentSystem.BLL.Token;
using WebAppQueueManagmentSystem.Cors;

namespace WebAppQueueManagmentSystem.Controllers
{ 
    public class CounterController : Controller
    { 
        readonly ITokenRepository token;
        readonly ICounterRepository counter;
        readonly IApiUtility helper;

        public CounterController(ITokenRepository _token, ICounterRepository _counter, IApiUtility _helper)
        {
            this.token = _token;
            this.counter = _counter;
            this.helper = _helper;
        }

        public ActionResult AddCounter()
        {
            return View();
        }

        // GET: Counter
        public ActionResult ListCounter()
        {
            var list = counter.ListCounter().ToList();

            return View(list);
        }

        public ActionResult CounterActivity()
        {
            return View();
        }

        public ActionResult TicketCounter(int counterId) 
        {
            var list = token.ViewCounterActivity(counterId);

            return View(list);
        }
    }
}