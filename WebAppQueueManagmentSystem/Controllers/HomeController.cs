﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppQueueManagmentSystem.BLL.Token;
using WebAppQueueManagmentSystem.Hubs;

namespace WebAppQueueManagmentSystem.Controllers
{
    public class HomeController : Controller
    {
        string TokenNumber;
        readonly ITokenRepository token;
        public HomeController(ITokenRepository _token)
        {
            this.token = _token;
        }

        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult CounterDashboard()
        { 
            return View();
        }

        public ActionResult CurrentTicketNumber() {

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
            var message = token.GenerateTicket(CustomerType);
            TokenNumber = message.token.ToString();
            print();
            BroadcastTicketNumber(TokenNumber);

            return Json(new { message}, JsonRequestBehavior.AllowGet);
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

        public void BroadcastTicketNumber(string TicketNumber) {
            TicketHub.TicketBroadCast(TicketNumber);
        }

        void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            var g = e.Graphics;
            var stringformat = new StringFormat();
            stringformat.Alignment = StringAlignment.Far;
            var solidBrush = new SolidBrush(Color.Black);
            var fontFamily = new FontFamily("Times New Roman");
            var font = new Font(fontFamily,  15,  FontStyle.Regular, GraphicsUnit.Pixel);
            var tkcLogo = Image.FromFile(@"C:\Users\pc\source\repos\WebTokeManagmentSystem\WebAppQueueManagmentSystem\assets\images\meezan-bank-vector-logo.png"); 
            g.DrawImage(tkcLogo, new Point(40, 35));
            RectangleF rect1 = new RectangleF(12.0F, 25.0F, 182, 25.0F);
            RectangleF rect2 = new RectangleF(100.0F, 25.0F, 182, 25.0F);
            RectangleF rect3 = new RectangleF(50.0F, 140.0F, 182, 25.0F);
            g.DrawString($"{DateTime.Now.Date.ToString("dd/MM/yyyy")}", font, solidBrush, rect1);
            g.DrawString($"Queue Ticket", font, solidBrush, rect2, stringformat);
            g.DrawString($"Ticket Number : {TokenNumber}", font, solidBrush, rect3, stringformat);
            tkcLogo.Dispose();
        }
    }
}