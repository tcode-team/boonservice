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
    [Table("AFS_SHIPMENT_EXPENSE")]
    public class afs_shipment_expense
    {
        /// <summary>
        /// client
        /// </summary>
        [Key]
        [StringLength(9)]
        public string CLIENT { get; set; }

        /// <summary>
        /// shipment number
        /// </summary>
        [Key]
        [StringLength(30)]
        public string SHIPMENT_NUMBER { get; set; }

        /// <summary>
        /// รหัสค่าใช้จ่าย
        /// </summary>
        [Key]
        public int EXPENSE_ID { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        [StringLength(250)]
        public string REMARK { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        public decimal EXPENSE_AMOUNT { get; set; }

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