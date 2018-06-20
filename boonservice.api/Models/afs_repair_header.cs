using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace boonservice.api.Models
{
    /// <summary>
    /// Table afs_repair_header  
    /// </summary>
    [Table("AFS_REPAIR_HEADER")]
    public class afs_repair_header
    {
        /// <summary>
        /// รหัสแจ้งซ่อม
        /// </summary>
        [Key]
        [StringLength(11)]
        public string REPAIR_CODE { get; set; }

        /// <summary>
        /// วันที่แจ้งซ่อม
        /// </summary>
        public DateTime? REPAIR_DATE { get; set; }

        /// <summary>
        /// Sale Order Number
        /// </summary>
        [StringLength(30)]
        public string SO_NUMBER { get; set; }

        /// <summary>
        /// Sold-to Code
        /// </summary>
        [StringLength(30)]
        public string SOLDTO_CODE { get; set; }

        /// <summary>
        /// Sold-to Name
        /// </summary>
        [StringLength(80)]
        public string SOLDTO_NAME { get; set; }

        /// <summary>
        /// SO ค่าขนส่ง
        /// </summary>
        public decimal TRANSPORT_AMOUNT { get; set; }

        /// <summary>
        /// เบอร์ติดต่อ
        /// </summary>
        [StringLength(50)]
        public string CONTACT_TEL { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        [StringLength(255)]
        public string REMARK { get; set; }
        
        /// <summary>
        /// สถานะ
        /// </summary>
        [StringLength(20)]
        public string STATUS { get; set; }

        /// <summary>
        /// รหัสผู้สร้าง
        /// </summary>
        public int CREATED_BY { get; set; }

        /// <summary>
        /// วันที่สร้าง
        /// </summary>
        public DateTime? CREATED_DATE { get; set; }

        /// <summary>
        /// รหัสผู้อัพเดท
        /// </summary>
        public int UPDATE_BY { get; set; }

        /// <summary>
        /// วันที่อัพเดท
        /// </summary>
        public DateTime? UPDATE_DATE { get; set; }
    }


}