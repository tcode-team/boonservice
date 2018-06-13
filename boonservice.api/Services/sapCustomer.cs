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
    /// Service about customer
    /// </summary>
    /// <remarks></remarks>
    public class sapCustomer
    {
        /// <summary>
        /// Get KNA1
        /// </summary>
        /// <remarks></remarks>
        public KNA1 GetKNA1(string mandt, string kunnr)
        {
            using (var context = new SAPSR3Context())
            {
                OracleParameter p1 = new OracleParameter("MANDT", mandt);
                OracleParameter p2 = new OracleParameter("KUNNR", kunnr);
                object[] parameters = new object[] { p1, p2 };

                var kna1 = context.KNA1
                    .SqlQuery("SELECT * FROM SAPSR3.KNA1 WHERE MANDT=:MANDT AND KUNNR=:KUNNR", parameters)
                    .FirstOrDefault();
                return kna1;
            }
        }

        /// <summary>
        /// Concate Address
        /// </summary>
        /// <remarks></remarks>
        public string ConcateAddress(ADRC adrc)
        {
            string[] words = { 
                adrc.STR_SUPPL1.Trim(),
                adrc.STR_SUPPL2.Trim(),
                adrc.STR_SUPPL3.Trim(),
                adrc.LOCATION.Trim(),
                adrc.CITY2.Trim(),
                adrc.CITY1.Trim(),
                adrc.POST_CODE1.Trim()
            };
            return string.Join(" ", words.Where(s => !String.IsNullOrEmpty(s)));
        }

        /// <summary>
        /// Get ADRC
        /// </summary>
        /// <remarks></remarks>
        public ICollection<ADRC> GetADRC(string mandt, string addrnumber)
        {
            using (var context = new SAPSR3Context())
            {
                OracleParameter p1 = new OracleParameter("MANDT", mandt);
                OracleParameter p2 = new OracleParameter("ADDRNUMBER", addrnumber);
                object[] parameters = new object[] { p1, p2 };

                var adrc = context.ADRC
                    .SqlQuery("SELECT * FROM SAPSR3.ADRC WHERE CLIENT=:MANDT AND ADDRNUMBER=:ADDRNUMBER", parameters)
                    .ToList();
                return adrc;
            }
        }

    }
}