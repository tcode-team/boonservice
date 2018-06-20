using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace boonservice.api.Models
{
    /// <summary>
    /// Table afs_repair_items
    /// </summary>
    [Table("AFS_REPAIR_ITEMS")]
    public class afs_repair_items
    {
        /// <summary>
        /// ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public double REPAIR_ITEM_ID { get; set; }

        /// <summary>
        /// รหัสแจ้งซ่อม
        /// </summary>
        [StringLength(11)]
        public string REPAIR_CODE { get; set; }

        /// <summary>
        /// Sale Order Number
        /// </summary>
        [StringLength(30)]
        public string SO_NUMBER { get; set; }

        /// <summary>
        /// Sale Order item
        /// </summary>
        [StringLength(18)]
        public string SO_ITEM { get; set; }

        /// <summary>
        /// ประเภทสินค้า
        /// </summary>
        [StringLength(80)]
        public string REPAIR_ITEM_TYPE { get; set; }

        /// <summary>
        /// จำนวน
        /// </summary>
        public decimal REPAIR_QTY { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        [StringLength(255)]
        public string REPAIR_REMARK { get; set; }
        
        /// <summary>
        /// ตำแหน่งชำรุด
        /// </summary>
        [StringLength(255)]
        public string REPAIR_DESC { get; set; }

        /// <summary>
        /// การดำเนินการ
        /// </summary>
        [StringLength(80)]
        public string REPAIR_TYPE { get; set; }

        /// <summary>
        /// ประกัน
        /// </summary>
        [StringLength(1)]
        public string WARANTY { get; set; }

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