using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
            if (!dbmanager.IsUserIdExist(memberState.UserId)) // 如果該帳號不存在才能新增
            {
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
            else
            {
                ViewBag.Message = "此帳號已存在";
                return View();
            }
        }

        //---------------------修改帳號頁-----------------------
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
            return RedirectToAction("Index"); // 返回首頁
        }

        //--------------------刪除資料功能-----------------------
        public ActionResult DeleteMember(int id)
        {
            dbManager dbManager = new dbManager();
            dbManager.DeleteMemberStateById(id);
            return RedirectToAction("Index");
        }


        //--------------------Login--------------------------
        public ActionResult Login()
        {
            return View();
        }
        // Post: Home/Login
        [HttpPost]
        public ActionResult Login(string userid, string password)
        {
            dbManager dbManager = new dbManager();
            MemberState memberState = dbManager.GetMemberByAccountandPassword(userid, password);

            // 判斷是否註冊過以指定導向頁
            if (memberState.UserId == "不存在")
            {
                ViewBag.Message = "帳密錯誤，登入失敗";
                return View();
            }

            // 使用Session變數紀錄歡迎詞
            Session["Welcome"] = memberState.nickName + "歡迎光臨";

            // 登入驗證
            FormsAuthentication.RedirectFromLoginPage(userid, true);
            return RedirectToAction("Index", "Member");

        }
    }

}