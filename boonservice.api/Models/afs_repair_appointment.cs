using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace boonservice.api.Models
{
    /// <summary>
    /// Table afs_repair_appointment
    /// </summary>
    [Table("AFS_REPAIR_APPOINTMENT")]
    public class afs_repair_appointment
    {
        /// <summary>
        /// ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public double REPAIR_APPOINT_ID { get; set; }

        /// <summary>
        /// รหัสแจ้งซ่อม
        /// </summary>
        [StringLength(11)]
        public string REPAIR_CODE { get; set; }

        /// <summary>
        /// วันที่นัดลูกค้า
        /// </summary>
        public DateTime? APPOINTMENT_DATE { get; set; }

        /// <summary>
        /// เวลานัดลูกค้า
        /// </summary>
        [StringLength(8)]
        public string APPOINTMENT_TIME { get; set; }

        /// <summary>
        /// วันที่นัดซ่อมเสร็จ
        /// </summary>
        public DateTime? TARGET_DATE { get; set; }

        /// <summary>
        /// ทีมช่าง
        /// </summary>
        [StringLength(40)]
        public string TECHNICIAN_TEAM{ get; set; }

        /// <summary>
        /// ราคาประเมิน
        /// </summary>
        public decimal PRICE_AMOUNT { get; set; }

        /// <summary>
        /// ราคา Extra
        /// </summary>
        public decimal PRICE_EXTRA { get; set; }

        /// <summary>
        /// หมายเหตุลูกค้า
        /// </summary>
        [StringLength(255)]
        public string REMARK_CUSTOMER { get; set; }
        
        /// <summary>
        /// รหัสผู้สร้าง
        /// </summary>
        public double CREATED_BY { get; set; }

        /// <summary>
        /// วันที่สร้าง
        /// </summary>
        public DateTime? CREATED_DATE { get; set; }

        /// <summary>
        /// รหัสผู้อัพเดท
        /// </summary>
        public double UPDATE_BY { get; set; }

        /// <summary>
        /// วันที่อัพเดท
        /// </summary>
        public DateTime? UPDATE_DATE { get; set; }
    }


}