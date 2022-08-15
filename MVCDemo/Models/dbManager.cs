using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient; // mssql 連線
using System.Configuration; // 引用config

namespace MVCDemo.Models
{
    public class dbManager
    {
        // 取得資料庫資料
        public List<MemberState> GetMemberStates()
        {
            List<MemberState> memberStates = new List<MemberState>(); // 宣告List以存資料

            // 連線sql
            SqlConnection sqlConnection = new SqlConnection // 從web.config取連線字串，將連線動作指向變數
                (ConfigurationManager.ConnectionStrings["MemberDB"].ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM tmember"); // 取出table tmember所有row
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open(); //連線並開啟資料庫

            // 取值
            SqlDataReader reader = sqlCommand.ExecuteReader(); // 使用sqldatareader方法，逐筆讀sqlcommand篩出的資料，並存至變數
            if (reader.HasRows) // 如果有至少一筆資料
            {
                while (reader.Read()) // 逐筆讀取
                {
                    MemberState memberState = new MemberState // 將sql資料給至變數
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        nickName = reader.GetString(reader.GetOrdinal("nickName")),
                        UserId = reader.GetString(reader.GetOrdinal("UserId")),
                        CardLevel = reader.GetString(reader.GetOrdinal("CardLevel"))
                    };
                    memberStates.Add(memberState); // 加入list中
                }
            }
            else { Console.WriteLine("資料庫為空! "); } // 沒資料

            sqlConnection.Close(); // 關閉資料庫

            return memberStates; // 回傳memberstates list
        }

        // 新增資料
        public void NewMember(MemberState memberState)
        {
            // 連線sql
            SqlConnection sqlConnection = new SqlConnection // 從web.config取連線字串，將連線動作指向變數
                (ConfigurationManager.ConnectionStrings["MemberDB"].ConnectionString);

            SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO tmember(UserId, Password, nickName, CardLevel)
                                    VALUES (@UserId, @Password, @nickName, @CardLevel)"); // insert tmember
                                                                                          // 使用sql paramter避免injection
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@UserId", memberState.UserId));
            sqlCommand.Parameters.Add(new SqlParameter("@Password", memberState.Password));
            sqlCommand.Parameters.Add(new SqlParameter("@nickName", memberState.nickName));
            sqlCommand.Parameters.Add(new SqlParameter("@CardLevel", "1"));

            sqlConnection.Open(); //連線並開啟資料庫
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}