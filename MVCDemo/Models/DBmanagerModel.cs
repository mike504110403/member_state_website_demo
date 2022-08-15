using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient; // mssql 連線
using System.Configuration; // 引用config

namespace MVCDemo.Models
{
    public class DBmanagerModel
    {
        // 連線sql
        SqlConnection conn = new SqlConnection
            (ConfigurationManager.ConnectionStrings["MemberDB"].ConnectionString);
    }

    public class Member
    {
        public int Id { get; set; } 
        public string nickName { get; set; } // 暱稱
        public string UserId { get; set; } //帳號
        public string CardLevel { get; set; } // 卡別

        private string Password; // 密碼
        private string realName; // 真實姓名
        private string phoneNumber; // 電話號碼
        private string email; // 信箱
        private string Address; // 地址
    }
}