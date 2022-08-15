using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDemo.Models;


namespace MVCDemo.Controllers
{
    public class HomeController : Controller
    {

        //-------------------------首頁------------------------------
        public ActionResult Index()
        {
            dbManager memberstate = new dbManager();
            List<MemberState> MemberStates = memberstate.GetMemberStates();
            ViewBag.MemberStates = MemberStates;
            return View();
        }

        //--------------------------創建帳號頁-------------------------
        public ActionResult CreateAccount()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateAccount(MemberState memberState)
        {
            dbManager dbmanager = new dbManager();
            try
            {
                dbmanager.NewMember(memberState);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return RedirectToAction("Index");
        }

        //---------------------修改帳號頁-----------------------
        public ActionResult EditMemberState(int id)
        {
            dbManager dbmanager = new dbManager();
            MemberState memberState = dbmanager.GetMemberStateById(id);
            return View(memberState);
        }
    }

}