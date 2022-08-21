using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCDemo.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult Index()
        {
            return View();
        }

        // GET: Member/Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut(); // 登出授權
            return RedirectToAction("Login", "Home");
        }


    }
}