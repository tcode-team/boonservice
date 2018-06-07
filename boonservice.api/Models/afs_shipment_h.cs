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
    [Table("afs_shipment_h")]
    public class afs_shipment_h
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
        /// วันที่
        /// </summary>
        public DateTime? transport_date { get; set; }

        /// <summary>
        /// รหัสกลุ่มรถ
        /// </summary>
        [StringLength(2)]
        public string cargroup_code { get; set; }

        /// <summary>
        /// สถานะ
        /// </summary>
        [StringLength(20)]
        public string status { get; set; }

        /// <summary>
        /// รหัสคนขับ afs_car_identity_card
        /// </summary>
        public Int16 driver_id { get; set; }

        /// <summary>
        /// รหัสเด็กรถ 1 afs_car_identity_card
        /// </summary>
        public Int16 staff1_id { get; set; }

        /// <summary>
        /// รหัสเด็กรถ 2 afs_car_identity_card
        /// </summary>
        public Int16 staff2_id { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        [StringLength(250)]
        public string remark { get; set; }

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