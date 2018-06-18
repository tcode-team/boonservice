using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace boonservice.api.Models
{
    /// <summary>
    /// Table afs_shipment_h  
    /// </summary>
    [Table("AFS_SHIPMENT_H")]
    public class afs_shipment_h
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
        /// วันที่
        /// </summary>
        public DateTime? TRANSPORT_DATE { get; set; }

        /// <summary>
        /// รหัสกลุ่มรถ
        /// </summary>
        [StringLength(2)]
        public string CARGROUP_CODE { get; set; }

        /// <summary>
        /// สถานะ
        /// </summary>
        [StringLength(20)]
        public string STATUS { get; set; }

        /// <summary>
        /// รหัสคนขับ afs_car_identity_card
        /// </summary>
        public Int16 DRIVER_ID { get; set; }

        /// <summary>
        /// รหัสเด็กรถ 1 afs_car_identity_card
        /// </summary>
        public Int16 STAFF1_ID { get; set; }

        /// <summary>
        /// รหัสเด็กรถ 2 afs_car_identity_card
        /// </summary>
        public Int16 STAFF2_ID { get; set; }

        /// <summary>
        /// จุดส่ง
        /// </summary>
        public Int16 POINT_ID { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        [StringLength(250)]
        public string REMARK { get; set; }

        /// <summary>
        /// รหัสผู้ confirm
        /// </summary>
        public int CONFIRM_BY { get; set; }

        /// <summary>
        /// วันที่ confirm
        /// </summary>
        public DateTime? CONFIRM_DATE { get; set; }

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