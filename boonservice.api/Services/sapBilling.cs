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
    /// Service about Billing
    /// </summary>
    /// <remarks></remarks>
    public class sapBilling
    {
        /// <summary>
        /// Get VBRK
        /// </summary>
        /// <remarks></remarks>
        public ICollection<VBRK> GetVBRKbySONumber(string mandt, string zuonr)
        {
            using (var context = new SAPSR3Context())
            {
                OracleParameter p1 = new OracleParameter("MANDT", mandt);
                OracleParameter p2 = new OracleParameter("ZUONR", zuonr);
                object[] parameters = new object[] { p1, p2 };

                var vbrk = context.VBRK
                    .SqlQuery("SELECT * FROM SAPSR3.VBRK WHERE MANDT=:MANDT AND ZUONR=:ZUONR", parameters)
                    .ToList();
                return vbrk;
            }
        }

    }
}