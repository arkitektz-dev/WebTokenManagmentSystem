using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Speech.Synthesis;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
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
        public static bool isPlayed = false;
         
        public HomeController(ITokenRepository _token, ICounterRepository _counter, IApiUtility _helper)
        {
            this.token = _token;
            this.counter = _counter;
            this.helper = _helper;
        }

        #region ActionResults
        public ActionResult Index()
        {

            return View();
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
        public ActionResult CurrentTicketNumber()
        {

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
        public ActionResult AllTicketStatus()
        {
            ViewBag.CounterStatus = token.StatusList();

            return View();
        }
        public ActionResult GetNewTicket()
        {
            return View();
        }
        #endregion

        #region ParitalViews
        public PartialViewResult ListCountTicket()
        {
            var list = token.ListCounterToken().ToList();
            return PartialView(list);
        }
        [HttpGet]
        public PartialViewResult GetTicketList(DateTime TicketDate, int TicketStatus, int CustomerType)
        {

            var list = token.CurrentList(TicketDate, TicketStatus, CustomerType);




            return PartialView(list);
        }
        #endregion

        #region JsonResult
        [HttpPost]
        public JsonResult GetNewTicket(string CustomerType)
        {
            bool printerFound = false;
            bool isPrinterAvialiable = ChecKAvailablePrinter();

            if (isPrinterAvialiable == true)
            {
                printerFound = true;
            }

            var row = token.GenerateTicket(CustomerType);

            var TokenDetail = new Token()
            {
                token = row.token,
                date = row.date,
                time = row.time,
                PrinterFound = printerFound
            };
            TokenNumber = TokenDetail.token;
            print();
            BroadcastTicketNumber(TokenDetail);

            return Json(new { TokenDetail }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTicketStatus(string TokenNumber)
        {

            var message = token.GetTokenStatus(TokenNumber);

            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AssignCounterToTicket(string TokenNumber, string UserId, int StatusId)
        {
            var message = counter.AssignTokenToCounter(TokenNumber, UserId, StatusId);

            if (message != null)
            {
                BroadcastNewAssignTicket(TokenNumber);
                BroadcastRemoveTicket(TokenNumber);
                //TicketHub.SoundPlayed();
                return Json(new { message = "Success" }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { message = "Error" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult SubmittedTicket(string TokenNumber, string Comment, int ServiceOptionId, byte StatusId)
        {
            var message = token.Submitted_Token(TokenNumber, Comment, ServiceOptionId, StatusId);

            if (message != null)
            {
                return Json(new { message = "Success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { message = "Error" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult CallAgain(string TokenNumber, int Counter)
        {
            var message = token.InsertAnncoumentInQueue(Counter, TokenNumber);

            if (message != null)
            {
                return Json(new { message = "Success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { message = "Error" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ChangeTokenStatus(string TokenNumber, byte Status)
        {
            var tokenDetail = token.ChangeTokenStatus(TokenNumber, Status);

            if (tokenDetail != null)
            {
                return Json(new { tokenDetail }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { tokenDetail = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetPendingCounter(string UserId)
        {

            var tokenDetail = counter.GetLastPendingTicket(UserId);

            if (tokenDetail != null)
            {
                return Json(new { tokenDetail }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { tokenDetail = "" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetAllTicketCount()
        {
            var message = token.GetTicketStatuses();

            if (HttpRuntime.Cache["LoggedInUsers"] != null)//check if the list has been created
            {
                string username = User.Identity.Name;

                List<string> loggedInUsers = (List<string>)HttpRuntime.Cache["LoggedInUsers"];
                message.ActiveCounter = loggedInUsers.Count().ToString();
                return Json(new { message }, JsonRequestBehavior.AllowGet);

            }
            else {
                message.ActiveCounter = "0";
            }

            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllChartsValues()
        {
            var message = token.GetAllChartValues();
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllCounterValues() 
        {
            var message = token.GetCountTicketByCounter();
             
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region HelperMethod
        [NonAction]
        private void BroadcastRemoveTicket(string tokenNumber)
        {
            TicketHub.RemoveTicket(tokenNumber);
        }
        [NonAction]
        private void BroadcastNewAssignTicket(string TokenNumber)
        {
            TicketHub.NewAssignTicket(TokenNumber);
        }
        [NonAction]
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
        [NonAction]
        public bool ChecKAvailablePrinter()
        {
            // Set management scope
            ManagementScope scope = new ManagementScope(@"\root\cimv2");
            scope.Connect();

            // Select Printers from WMI Object Collections
            ManagementObjectSearcher searcher = new
             ManagementObjectSearcher("SELECT * FROM Win32_Printer");

            string printerName = "";
            foreach (ManagementObject printer in searcher.Get())
            {
                printerName = printer["Name"].ToString();
                string SelectedPrinterName = ConfigurationManager.AppSettings["PrinterName"];
                if (printerName.Contains(SelectedPrinterName))
                {
                    Debug.WriteLine("Printer = " + printer["Name"]);
                    if (printer["WorkOffline"].ToString().ToLower().Equals("true"))
                    {

                        // printer is offline by user
                        Debug.WriteLine("Your Plug-N-Play printer is not connected.");
                    }
                    else
                    {
                        return true;
                        // printer is not offline
                        Debug.WriteLine("Your Plug-N-Play printer is connected.");
                    }
                }
            }

            return false;
        }
        [NonAction]
        public void BroadcastTicketNumber(Token TokenDetail)
        {
            TicketHub.TicketBroadCast(TokenDetail);
        }
        [NonAction]
        void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            var g = e.Graphics;
            var stringformat = new StringFormat();
            stringformat.Alignment = StringAlignment.Far;
            var solidBrush = new SolidBrush(Color.Black);
            var fontFamily = new FontFamily("Times New Roman");
            var font = new Font(fontFamily, 15, FontStyle.Regular, GraphicsUnit.Pixel);
            string currentDate = $"{DateTime.Now.Date.ToString("dd/MM/yyyy")} {DateTime.Now.ToString("hh:mm tt")}";
            var tkcLogo = Image.FromFile(@"C:\Users\pc\source\repos\WebTokeManagmentSystem\WebAppQueueManagmentSystem\assets\images\meezan-bank-vector-logo.png");
            g.DrawImage(tkcLogo, new Point(40, 35));
            RectangleF rect1 = new RectangleF(12.0F, 25.0F, 182, 25.0F);
            RectangleF rect2 = new RectangleF(100.0F, 25.0F, 182, 25.0F);
            RectangleF rect3 = new RectangleF(50.0F, 140.0F, 182, 25.0F);
            RectangleF rect4 = new RectangleF(50.0F, 180.0F, 182, 25.0F);
            g.DrawString(currentDate, font, solidBrush, rect1);
            g.DrawString($"Queue Ticket", font, solidBrush, rect2, stringformat);
            g.DrawString($"Ticket Number : {TokenNumber}", font, solidBrush, rect3, stringformat);
            g.DrawString($"Expected Time : {token.GetAverageTime()} min", font, solidBrush, rect4, stringformat);
            tkcLogo.Dispose();
        }
        #endregion
    }
}