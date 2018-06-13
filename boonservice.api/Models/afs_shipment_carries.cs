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
    [Table("AFS_SHIPMENT_CARRIES")]
    public class afs_shipment_carries
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
        /// item no
        /// </summary>
        [Key]
        public int ITEM_NO { get; set; }

        /// <summary>
        /// ช่วงเวลา
        /// </summary>
        [StringLength(50)]
        public string TIME_RANGE { get; set; }

        /// <summary>
        /// Sale Order Number
        /// </summary>
        [StringLength(20)]
        public string SALEORDER_NUMBER { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        [StringLength(250)]
        public string REMARK { get; set; }

        /// <summary>
        /// จำนวนเงิน คนขับรถ
        /// </summary>
        public decimal DRIVER_AMOUNT { get; set; }

        /// <summary>
        /// จำนวนเงิน เด็กรถ
        /// </summary>
        public decimal STAFF_AMOUNT { get; set; }

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