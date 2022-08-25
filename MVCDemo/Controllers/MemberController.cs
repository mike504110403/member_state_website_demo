using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVCDemo.Models;

namespace MVCDemo.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        //----------------------會員首頁------------------------
        // GET: Member
        public ActionResult Index()
        {
            return View();
        }


        //---------------------會員登出-------------------------
        // GET: Member/Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut(); // 登出授權
            return RedirectToAction("Login", "Home");
        }

        //---------------------會員狀態頁------------------------
        // GET: Member/MemberState
        public ActionResult MemberState()
        {
            string UserId = User.Identity.Name; // 取目前授權帳號
            dbManager dbManager = new dbManager(); // db連線物件
            MemberState memberState = dbManager.GetMemberStateByUserId(UserId); // 取用會員狀態方法
            ViewBag.memberState = memberState; // 將meberState記在viewbag
            return View(ViewBag.memberState); // 跳轉至會員狀態頁
        }

        //---------------------修改帳號-----------------------
        public ActionResult EditMemberState(int id)
        {
            dbManager dbmanager = new dbManager();
            MemberState memberState = dbmanager.GetMemberStateById(id); // 找出該筆資料
            return View(memberState); // 傳給view顯示該筆資料
        }
        [HttpPost]
        public ActionResult EditMemberState(MemberState memberState)
        {
            dbManager dbManager = new dbManager();
            dbManager.UpdateMember(memberState); // 修改該筆資料
            return RedirectToAction("MemberState"); // 返回首頁
        }


    }
}