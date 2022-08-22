using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MVCDemo.Models;

namespace MVCDemo.Controllers
{
    public class DemoController : ApiController
    {
        // GET: api/Demo
        public IEnumerable GetMemberStates()
        {
            dbManager memberstate = new dbManager();
            List<MemberState> MemberStates = memberstate.GetMemberStates();
            return MemberStates;
        }

        // GET: api/Demo/5
        public MemberState GetGetMemberStatesById(int id)
        {
            dbManager dbmanager = new dbManager();
            MemberState memberState = dbmanager.GetMemberStateById(id);
            return memberState;
        }

        // PUT: api/Demo/5
        public HttpResponseMessage PutMemberStates(int id, MemberState memberState)
        {
            dbManager dbManager = new dbManager();
            dbManager.UpdateMember(memberState); // 修改該筆資料
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST: api/Demo
        public HttpResponseMessage PostMemberState(MemberState memberState)
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
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, memberState);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = memberState.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Demo/5
        public HttpResponseMessage DeleteMemberState(int id)
        {
            dbManager dbManager = new dbManager();
            MemberState memberState = dbManager.GetMemberStateById(id);
            if (memberState == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            try
            {
                dbManager.DeleteMemberStateById(id);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, e.ToString());
            }

            return Request.CreateResponse(HttpStatusCode.OK, memberState);
        }




        // POST: api/Demo
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Demo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Demo/5
        public void Delete(int id)
        {
        }
    }
}
