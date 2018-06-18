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
        /// รหัสแจ้งซ่อม
        /// </summary>
        [Key]
        [StringLength(11)]
        public string repair_code { get; set; }

        /// <summary>
        /// Sale Order Number
        /// </summary>
        [Key]
        public Int16 repair_item { get; set; }

        /// <summary>
        /// Sale Order Number
        /// </summary>
        [StringLength(30)]
        public string so_number { get; set; }

        /// <summary>
        /// Sale Order item
        /// </summary>
        public decimal so_item { get; set; }

        /// <summary>
        /// จำนวน
        /// </summary>
        public int repair_qty { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        [StringLength(255)]
        public string repair_remark { get; set; }
        
        /// <summary>
        /// ตำแหน่งชำรุด
        /// </summary>
        [StringLength(255)]
        public string repair_desc { get; set; }

        /// <summary>
        /// การดำเนินการ
        /// </summary>
        [StringLength(80)]
        public string repair_type { get; set; }

        /// <summary>
        /// ประกัน
        /// </summary>
        [StringLength(1)]
        public string waranty { get; set; }

        /// <summary>
        /// รหัสผู้สร้าง
        /// </summary>
        public int created_by { get; set; }

        /// <summary>
        /// วันที่สร้าง
        /// </summary>
        public DateTime? created_date { get; set; }

        /// <summary>
        /// รหัสผู้อัพเดท
        /// </summary>
        public int update_by { get; set; }

        /// <summary>
        /// วันที่อัพเดท
        /// </summary>
        public DateTime? update_date { get; set; }
    }


}