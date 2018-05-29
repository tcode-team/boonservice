using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using boonservice.api.Context;
using boonservice.api.Models;

namespace boonservice.api.Controllers
{
    public class UserController : ApiController
    {

        private LoadContext load = new LoadContext();

        /// <summary>
        /// Get all b3g_users
        /// </summary>
        /// <remarks>
        /// Get a list of all b3g_user
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="404">Data not found</response>
        [ResponseType(typeof(IEnumerable<UserDetail>))]
        [Route("user/getall")]
        public HttpResponseMessage Get()
        {
            try
            {
                using (var context = new LoadContext())
                {
                    var userdetails = new List<UserDetail>();
                    //var users = context.B3G_USERS.ToList();
                    foreach (var r in context.B3G_USERS.ToList()) {
                        var userd = context.B3G_USER_DESC
                             .Where(t => t.USER_ID == r.USER_ID && t.LANG_CODE == "TH")
                             .FirstOrDefault();

                        UserDetail userdetail = new UserDetail();
                        userdetail.userid = r.USER_ID;
                        userdetail.username = r.USER_NAME;
                        if (userd != null) {
                            userdetail.firstname = userd.FIRST_NAME;
                            userdetail.lastname = userd.LAST_NAME;
                        }
                        userdetails.Add(userdetail);
                    }
                    return userdetails == null
                        ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "User not found")
                        : Request.CreateResponse(HttpStatusCode.OK, userdetails);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get user authorization
        /// </summary>
        /// <remarks>
        /// ดึงข้อมูล user โดย Username/Password
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [ResponseType(typeof(UserDetail))]     
        [Route("user/PostCheckUserAuthor")]
        public HttpResponseMessage PostCheckUserAuthor(string username, string pwd)
        {
            using (var context = new LoadContext())
            {
                var user = context.B3G_USERS
                            .Where(b => b.USER_NAME.ToUpper() == username.ToUpper() && b.PASSWORD == pwd)
                            .FirstOrDefault();

                var userd = context.B3G_USER_DESC
                             .Where(t => t.USER_ID == user.USER_ID && t.LANG_CODE == "TH")
                             .FirstOrDefault();

                UserDetail userdetail = new UserDetail();
                userdetail.userid = user.USER_ID;
                userdetail.username = user.USER_NAME;
                userdetail.firstname = userd.FIRST_NAME;
                userdetail.lastname = userd.LAST_NAME;

                return user == null
                    ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "User not found")
                    : Request.CreateResponse(HttpStatusCode.OK, userdetail);
            }
        }
        
    }
}