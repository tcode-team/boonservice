using boonservice.api.Context;
using boonservice.api.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace boonservice.api.Services
{
    /// <summary>
    /// Service about User
    /// </summary>
    /// <remarks></remarks>
    public class loadUser
    {
        /// <summary>
        /// Get B3G_USER
        /// </summary>
        /// <remarks></remarks>
        public UserDetail GetUser(double userid)
        {
            using (var context = new LoadContext())
            {
                var user = context.B3G_USERS
                            .Where(b => b.USER_ID == userid)
                            .FirstOrDefault();
                if (user == null)
                {
                    return null;
                }

                var userd = context.B3G_USER_DESC
                             .Where(t => t.USER_ID == user.USER_ID && t.LANG_CODE == "TH")
                             .FirstOrDefault();

                UserDetail userdetail = new UserDetail();
                userdetail.userid = user.USER_ID;
                userdetail.username = user.USER_NAME;
                if (userd != null)
                {
                    userdetail.firstname = userd.FIRST_NAME;
                    userdetail.lastname = userd.LAST_NAME;
                }

                return userdetail;
            }
        }

    }
}