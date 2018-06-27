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
    /// Service about Branch
    /// </summary>
    /// <remarks></remarks>
    public class sapBranch
    {
        /// <summary>
        /// Get ZBRANCH
        /// </summary>
        /// <remarks></remarks>
        public ZBRANCH GetZBRANCH(string mandt, string branch_id)
        {
            using (var context = new SAPContext())
            {
                OracleParameter p1 = new OracleParameter("MANDT", mandt);
                OracleParameter p2 = new OracleParameter("BRANCH_ID", branch_id);
                object[] parameters = new object[] { p1, p2 };

                var branch = context.ZBRANCH
                    .SqlQuery("SELECT * FROM SAPSR3.ZBRANCH WHERE MANDT=:MANDT AND BRANCH_ID=:BRANCH_ID", parameters)
                    .FirstOrDefault();
                return branch;
            }
        }
        
    }
}