using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace boonservice.api.Models
{
    /// <summary>
    /// Table afs_shipment_carries  
    /// </summary>
    [Table("afs_shipment_carries")]
    public class afs_shipment_carries
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
        /// item no
        /// </summary>
        [Key]
        public int item_no { get; set; }

        /// <summary>
        /// รหัส afs_carries_point
        /// </summary>
        public Int16 point_id { get; set; }

        /// <summary>
        /// ช่วงเวลา
        /// </summary>
        [StringLength(50)]
        public string time_range { get; set; }

        /// <summary>
        /// Sale Order Number
        /// </summary>
        [StringLength(20)]
        public string saleorder_number { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        [StringLength(250)]
        public string remark { get; set; }

        /// <summary>
        /// จำนวนเงิน คนขับรถ
        /// </summary>
        public decimal driver_amount { get; set; }

        /// <summary>
        /// จำนวนเงิน เด็กรถ
        /// </summary>
        public decimal staff_amount { get; set; }

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