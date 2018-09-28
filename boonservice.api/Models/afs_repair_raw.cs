using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace boonservice.api.Models
{
    /// <summary>
    /// Table afs_repair_raw
    /// </summary>
    [Table("AFS_REPAIR_RAW")]
    public class afs_repair_raw
    {
        /// <summary>
        /// ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public double REPAIR_RAW_ID { get; set; }

        /// <summary>
        /// รหัสแจ้งซ่อม
        /// </summary>
        [StringLength(11)]
        public string REPAIR_CODE { get; set; }

        /// <summary>
        /// รายการอะไหล่
        /// </summary>
        [StringLength(120)]
        public string RAW_NAME { get; set; }

        /// <summary>
        /// จำนวน
        /// </summary>
        public int RAW_QTY { get; set; }

        /// <summary>
        /// สถานะ
        /// </summary>
        [StringLength(40)]
        public string STATUS { get; set; }

        /// <summary>
        /// วันที่คาดว่าอะไหล่จะเข้า
        /// </summary>
        public DateTime? RAW_DATE { get; set; }
        
        /// <summary>
        /// หมายเหตุ
        /// </summary>
        [StringLength(255)]
        public string REMARK { get; set; }

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