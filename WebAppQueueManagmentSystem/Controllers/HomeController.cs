using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppQueueManagmentSystem.ApiHelpers.Utility;
using WebAppQueueManagmentSystem.BLL.Counter;
using WebAppQueueManagmentSystem.BLL.Token;
using WebAppQueueManagmentSystem.Hubs;
using WebAppQueueManagmentSystem.Models;

namespace WebAppQueueManagmentSystem.Controllers
{
    public class HomeController : Controller
    {
        string TokenNumber;
        readonly ITokenRepository token;
        readonly ICounterRepository counter;
        readonly IApiUtility helper;
        
        public HomeController(ITokenRepository _token, ICounterRepository _counter, IApiUtility _helper)
        {
            this.token = _token;
            this.counter = _counter;
            this.helper = _helper;
        }

        [Authorize]
        public ActionResult Index()
        {
            
            return View();
        }

        public PartialViewResult ListCountTicket()
        {
            var list = token.ListCounterToken().ToList();
            return PartialView(list);
        }



        public JsonResult GetTicketStatus(string TokenNumber) {

            var message = token.GetTokenStatus(TokenNumber);
          
            return Json(new { message },JsonRequestBehavior.AllowGet);
        }



        //Counter Dashboard
        public ActionResult CounterDashboard(string UserId)
        {
            var CounterDetail = counter.CounterDetail(UserId);

            //Fill Dropdown
            ViewBag.CounterStatus = CounterDetail.CounterStatus;
            ViewBag.TypeOfService = CounterDetail.CounterService;

            //Counter Number
            TempData["CounterNumber"] = CounterDetail.CounterID;

            //List Queue Ticket
            ViewBag.ListToken = token.ListToken(1, 3);

            return View();
        }

        public JsonResult AssignCounterToTicket(string TokenNumber, string UserId, int StatusId)
        {
            var message = counter.AssignTokenToCounter(TokenNumber, UserId, StatusId);

            if (message != null){
                BroadcastNewAssignTicket(TokenNumber);
                BroadcastRemoveTicket(TokenNumber);
                return Json(new { message = "Success" }, JsonRequestBehavior.AllowGet);
           
            }
            else { 
                return Json(new { message = "Error" }, JsonRequestBehavior.AllowGet);
            }

        }

        private void BroadcastRemoveTicket(string tokenNumber)
        {
            TicketHub.RemoveTicket(tokenNumber);
        }

        private void BroadcastNewAssignTicket(string TokenNumber)
        {
            TicketHub.NewAssignTicket(TokenNumber);
        }

        public JsonResult SubmittedTicket(string TokenNumber, string Comment, int ServiceOptionId, byte StatusId)
        {
            var message = token.Submitted_Token( TokenNumber,  Comment,  ServiceOptionId,  StatusId);

            if (message != null)
            {
                return Json(new { message = "Success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { message = "Error" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CurrentTicketNumber() {

            //List Queue Ticket
            ViewBag.ListToken = token.ListToken(1, 3);

            return View();
        }

        public ActionResult EveryCounterStatus()
        {


            return View();
        }


        public ActionResult UpdateFeedBack()
        {
            return View();
        }

    
        public ActionResult GenerateTicket()
        {
            print();
        
            return View();
        }


        public ActionResult GetNewTicket()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetNewTicket(string CustomerType)
        {
            var row = token.GenerateTicket(CustomerType);

            var TokenDetail = new Token()
            {
                token = row.token,
                date = row.date,
                time = row.time
            };
            TokenNumber = TokenDetail.token;
            print();
            BroadcastTicketNumber(TokenDetail);

            return Json(new { TokenDetail }, JsonRequestBehavior.AllowGet);
        }

        private void print()
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                pd.Print();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                Response.End();
            }
        }

        public void BroadcastTicketNumber(Token TokenDetail) {
            TicketHub.TicketBroadCast(TokenDetail);
        }

        void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            var g = e.Graphics;
            var stringformat = new StringFormat();
            stringformat.Alignment = StringAlignment.Far;
            var solidBrush = new SolidBrush(Color.Black);
            var fontFamily = new FontFamily("Times New Roman");
            var font = new Font(fontFamily,  15,  FontStyle.Regular, GraphicsUnit.Pixel);
            string currentDate = $"{DateTime.Now.Date.ToString("dd/MM/yyyy")} {DateTime.Now.ToString("hh:mm tt")}";
            var tkcLogo = Image.FromFile(@"C:\Users\pc\source\repos\WebTokeManagmentSystem\WebAppQueueManagmentSystem\assets\images\meezan-bank-vector-logo.png"); 
            g.DrawImage(tkcLogo, new Point(40, 35));
            RectangleF rect1 = new RectangleF(12.0F, 25.0F, 182, 25.0F);
            RectangleF rect2 = new RectangleF(100.0F, 25.0F, 182, 25.0F);
            RectangleF rect3 = new RectangleF(50.0F, 140.0F, 182, 25.0F);
            g.DrawString(currentDate, font, solidBrush, rect1);
            g.DrawString($"Queue Ticket", font, solidBrush, rect2, stringformat);
            g.DrawString($"Ticket Number : {TokenNumber}", font, solidBrush, rect3, stringformat);
            tkcLogo.Dispose();
        }
    }
}