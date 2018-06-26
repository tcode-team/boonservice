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
    /// Service about Repair
    /// </summary>
    /// <remarks></remarks>
    public class b3gRepair
    {
        /// <summary>
        /// Get Next Document Number
        /// </summary>
        /// <remarks></remarks>
        public string GetNextDocumentNumber()
        {
            using (var context = new SAPContext())
            {
                var maxdoc = context.Database
                    .SqlQuery<string>("SELECT SUBSTR(REPAIR_CODE,1,6) || LPAD(TO_NUMBER(SUBSTR(REPAIR_CODE,7,4)) + 1,4,'0') REPAIR_CODE " +
                                    "FROM( " +
                                    "SELECT NVL(MAX(REPAIR_CODE), TO_CHAR(SYSDATE, 'YYYYMM') || '0000') REPAIR_CODE " +
                                    "FROM AFS_REPAIR_HEADER WHERE REPAIR_CODE LIKE TO_CHAR(SYSDATE, 'YYYYMM') || '%' " +
                                    ")").FirstOrDefault();
                return maxdoc;
            }
        }

        /// <summary>
        /// Get afs_repair_header
        /// </summary>
        /// <remarks></remarks>
        public afs_repair_header GetRepairHeader(string repair_code)
        {
            using (var context = new SAPContext())
            {
                OracleParameter p1 = new OracleParameter("REPAIR_CODE", repair_code);
                object[] parameters = new object[] { p1 };

                var repair_header = context.afs_repair_header
                    .SqlQuery("SELECT * FROM AFS_REPAIR_HEADER WHERE REPAIR_CODE=:REPAIR_CODE", parameters)
                    .FirstOrDefault();
                return repair_header;
            }
        }

        /// <summary>
        /// Get afs_repair_item
        /// </summary>
        /// <remarks></remarks>
        public ICollection<afs_repair_items> GetRepairItem(string repair_code)
        {
            using (var context = new SAPContext())
            {
                OracleParameter p1 = new OracleParameter("REPAIR_CODE", repair_code);
                object[] parameters = new object[] { p1 };

                var repair_items = context.afs_repair_items
                    .SqlQuery("SELECT * FROM AFS_REPAIR_ITEMS WHERE REPAIR_CODE=:REPAIR_CODE", parameters)
                    .ToList();
                return repair_items;
            }
        }

        /// <summary>
        /// Get afs_repair_images
        /// </summary>
        /// <remarks></remarks>
        public ICollection<afs_repair_images> GetRepairImage(double repair_item_id)
        {
            using (var context = new SAPContext())
            {
                OracleParameter p1 = new OracleParameter("REPAIR_ITEM_ID", repair_item_id);
                object[] parameters = new object[] { p1 };

                var repair_images = context.afs_repair_images
                    .SqlQuery("SELECT * FROM AFS_REPAIR_IMAGES WHERE REPAIR_ITEM_ID=:REPAIR_ITEM_ID", parameters)
                    .ToList();
                return repair_images;
            }
        }

        /// <summary>
        /// Get afs_repair_appointment
        /// </summary>
        /// <remarks></remarks>
        public afs_repair_appointment GetRepairAppointment(string repair_code)
        {
            using (var context = new SAPContext())
            {
                OracleParameter p1 = new OracleParameter("REPAIR_CODE", repair_code);
                object[] parameters = new object[] { p1 };

                var repair_appointment = context.afs_repair_appointment
                    .SqlQuery("SELECT * FROM AFS_REPAIR_APPOINTMENT WHERE REPAIR_CODE=:REPAIR_CODE", parameters)
                    .FirstOrDefault();
                return repair_appointment;
            }
        }

        /// <summary>
        /// Get afs_repair_raw
        /// </summary>
        /// <remarks></remarks>
        public ICollection<afs_repair_raw> GetRepairRaw(string repair_code)
        {
            using (var context = new SAPContext())
            {
                OracleParameter p1 = new OracleParameter("REPAIR_CODE", repair_code);
                object[] parameters = new object[] { p1 };

                var raws = context.afs_repair_raw
                    .SqlQuery("SELECT * FROM AFS_REPAIR_RAW WHERE REPAIR_CODE=:REPAIR_CODE", parameters)
                    .ToList();
                return raws;
            }
        }

    }
}