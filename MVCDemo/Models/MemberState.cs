using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace MVCDemo.Models
{
    public class MemberState
    {
        public int Id { get; set; } 
        public string nickName { get; set; } // 暱稱
        public string UserId { get; set; } //帳號
        public string CardLevel { get; set; } // 卡別
        public string Password { get; set; } // 密碼
        private string realName; // 真實姓名
        private string phoneNumber; // 電話號碼
        private string email; // 信箱
        private string Address; // 地址
    }
}