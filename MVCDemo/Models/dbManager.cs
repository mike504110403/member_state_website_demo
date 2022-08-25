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

        // 用Id 找資料
        public MemberState GetMemberStateById(int id)
        {
            MemberState memberState = new MemberState();

            // 連線sql
            SqlConnection sqlConnection = new SqlConnection // 從web.config取連線字串，將連線動作指向變數
                (ConfigurationManager.ConnectionStrings["MemberDB"].ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM tmember WHERE id = @id"); // 取出tmember id 符合的 row
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@id", id));
            sqlConnection.Open(); //連線並開啟資料庫

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    memberState = new MemberState
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        nickName = reader.GetString(reader.GetOrdinal("nickName")),
                        UserId = reader.GetString(reader.GetOrdinal("UserId")),
                        CardLevel = reader.GetString(reader.GetOrdinal("CardLevel"))
                    };
                }
            }
            else { memberState.UserId = "未找到該筆資料"; }
            sqlConnection.Close();
            return memberState;
        }

        // 用UserId找資料
        public MemberState GetMemberStateByUserId(string UserId)
        {
            MemberState memberState = new MemberState();

            // 連線sql
            SqlConnection sqlConnection = new SqlConnection // 從web.config取連線字串，將連線動作指向變數
                (ConfigurationManager.ConnectionStrings["MemberDB"].ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM tmember WHERE UserId = @UserId"); // 取出tmember UserId 符合的 row
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@UserId", UserId));
            sqlConnection.Open(); //連線並開啟資料庫

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    memberState = new MemberState
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        nickName = reader.GetString(reader.GetOrdinal("nickName")),
                        UserId = reader.GetString(reader.GetOrdinal("UserId")),
                        CardLevel = reader.GetString(reader.GetOrdinal("CardLevel"))
                    };
                }
            }
            else { memberState.UserId = "未找到該筆資料"; }
            sqlConnection.Close();
            return memberState;
        }

        // 找帳號驗證登入
        public MemberState GetMemberByAccountandPassword(string userid, string password)
        {
            MemberState memberState = new MemberState();
            // 連線sql
            SqlConnection sqlConnection = new SqlConnection // 從web.config取連線字串，將連線動作指向變數
                (ConfigurationManager.ConnectionStrings["MemberDB"].ConnectionString);
            SqlCommand sqlCommand = new SqlCommand    // 取出tmember userid,password 符合的 row
                ("SELECT * FROM tmember WHERE userid = @userid AND password = @password");
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@userid", userid));
            sqlCommand.Parameters.Add(new SqlParameter("@password", password));
            sqlConnection.Open(); //連線並開啟資料庫

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    memberState = new MemberState
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        nickName = reader.GetString(reader.GetOrdinal("nickName")),
                        UserId = reader.GetString(reader.GetOrdinal("UserId")),
                        CardLevel = reader.GetString(reader.GetOrdinal("CardLevel"))
                    };
                }
            }
            else { memberState.UserId = "不存在"; }
            sqlConnection.Close();
            return memberState;
        }

        // 驗證帳號是否存在
        public bool IsUserIdExist(string userid)
        {
            MemberState memberState = new MemberState();

            // 連線sql
            SqlConnection sqlConnection = new SqlConnection // 從web.config取連線字串，將連線動作指向變數
                (ConfigurationManager.ConnectionStrings["MemberDB"].ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM tmember WHERE userid = @userid"); // 取出tmember id 符合的 row
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@userid", userid));
            sqlConnection.Open(); //連線並開啟資料庫

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                return true;
            }
            else { return false; }
            sqlConnection.Close();

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
            sqlCommand.Parameters.Add(new SqlParameter("@CardLevel", "1")); // 一開始建立資料卡別給1

            sqlConnection.Open(); //連線並開啟資料庫
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        // 修改資料
        public void UpdateMember(MemberState memberState)
        {
            // 連線sql
            SqlConnection sqlConnection = new SqlConnection // 從web.config取連線字串，將連線動作指向變數
                (ConfigurationManager.ConnectionStrings["MemberDB"].ConnectionString);

            SqlCommand sqlCommand = new SqlCommand(@"UPDATE tmember SET UserId = @UserId,
                                    nickName = @nickName WHERE Id = @Id"); // update tmember // 使用sql paramter避免injection
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@UserId", memberState.UserId));
            sqlCommand.Parameters.Add(new SqlParameter("@nickName", memberState.nickName));
            sqlCommand.Parameters.Add(new SqlParameter("@Id", memberState.Id));

            sqlConnection.Open(); //連線並開啟資料庫
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        // 刪除資料
        public void DeleteMemberStateById(int id)
        {
            // 連線sql
            SqlConnection sqlConnection = new SqlConnection // 從web.config取連線字串，將連線動作指向變數
                (ConfigurationManager.ConnectionStrings["MemberDB"].ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("DELETE FROM tmember WHERE id = @id"); // 取出tmember id 符合的 row
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@id", id));
            sqlConnection.Open(); //連線並開啟資料庫
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}