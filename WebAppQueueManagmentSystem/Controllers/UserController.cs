using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppQueueManagmentSystem.ApiHelpers.Utility;
using WebAppQueueManagmentSystem.BLL.Counter;
using WebAppQueueManagmentSystem.BLL.Token;
using WebAppQueueManagmentSystem.BLL.User;

namespace WebAppQueueManagmentSystem.Controllers
{
    public class UserController : Controller
    {
        readonly ITokenRepository token;
        readonly ICounterRepository counter;
        readonly IApiUtility helper;
        readonly IUserRepository user;

        public UserController(ITokenRepository _token,
            ICounterRepository _counter, 
            IApiUtility _helper,
            IUserRepository _user)
        {
            this.token = _token;
            this.counter = _counter;
            this.helper = _helper;
            this.user = _user;
        }

        // GET: User
        public ActionResult ListUser()
        {
            ViewBag.RoleList = user.GetUserRole();

            return View();
        }
        

        public PartialViewResult ListUserTable(string Role)
        {
            var list = user.GetUserList(Role).ToList();

            return PartialView(list);
        }


       
    }
}