using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace boonservice.api.Models
{
    /// <summary>
    /// Table afs_shipment_expense  
    /// </summary>
    [Table("afs_shipment_expense")]
    public class afs_shipment_expense
    {
        /// <summary>
        /// client
        /// </summary>
        [Key]
        [StringLength(9)]
        public string client { get; set; }

        /// <summary>
        /// shipment number
        /// </summary>
        [Key]
        [StringLength(30)]
        public string shipment_number { get; set; }

        /// <summary>
        /// รหัสค่าใช้จ่าย
        /// </summary>
        [Key]
        public int expense_id { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        [StringLength(250)]
        public string remark { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        public decimal expense_amount { get; set; }

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