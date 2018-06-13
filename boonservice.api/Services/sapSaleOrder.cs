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
    /// Service about Sale Order
    /// </summary>
    /// <remarks></remarks>
    public class sapSaleOrder
    {
        /// <summary>
        /// Get VBAK
        /// </summary>
        /// <remarks></remarks>
        public VBAK GetVBAK(string mandt, string vbeln)
        {
            using (var context = new SAPSR3Context())
            {
                OracleParameter p1 = new OracleParameter("MANDT", mandt);
                OracleParameter p2 = new OracleParameter("VBELN", vbeln);
                object[] parameters = new object[] { p1, p2 };

                var vbak = context.VBAK
                    .SqlQuery("SELECT * FROM SAPSR3.VBAK WHERE MANDT=:MANDT AND VBELN=:VBELN", parameters)
                    .FirstOrDefault();
                return vbak;
            }
        }

        /// <summary>
        /// Get VBAP
        /// </summary>
        /// <remarks></remarks>
        public ICollection<VBAP> GetVBAP(string mandt, string vbeln)
        {
            using (var context = new SAPSR3Context())
            {
                OracleParameter p1 = new OracleParameter("MANDT", mandt);
                OracleParameter p2 = new OracleParameter("VBELN", vbeln);
                object[] parameters = new object[] { p1, p2 };

                var vbap = context.VBAP
                    .SqlQuery("SELECT * FROM SAPSR3.VBAP WHERE MANDT=:MANDT AND VBELN=:VBELN", parameters)
                    .ToList();
                return vbap;
            }
        }

        /// <summary>
        /// Get VBPA    
        /// </summary>
        /// <remarks></remarks>
        public ICollection<VBPA> GetVBPA(string mandt, string vbeln, string posnr)
        {
            using (var context = new SAPSR3Context())
            {
                OracleParameter p1 = new OracleParameter("MANDT", mandt);
                OracleParameter p2 = new OracleParameter("VBELN", vbeln);
                OracleParameter p3 = new OracleParameter("POSNR", posnr);
                object[] parameters = new object[] { p1, p2, p3 };

                var vbpa = context.VBPA
                    .SqlQuery("SELECT * FROM SAPSR3.VBPA WHERE MANDT=:MANDT AND VBELN=:VBELN AND POSNR=:POSNR", parameters)
                    .ToList();
                return vbpa;
            }
        }

    }
}