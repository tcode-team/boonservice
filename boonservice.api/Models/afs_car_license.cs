using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace boonservice.api.Models
{
    /// <summary>
    /// Table afs_car_license  
    /// </summary>
    [Table("AFS_CAR_LICENSE")]
    public class afs_car_license
    {
        /// <summary>
        /// รหัสรถ
        /// </summary>
        [Key]
        public Int16 CAR_ID { get; set; }

        /// <summary>
        /// รหัสกลุ่มรถ
        /// </summary>
        [StringLength(2)]
        public string CARGROUP_CODE { get; set; }

        /// <summary>
        /// ทะเบียนรถ SAP
        /// </summary>
        [StringLength(40)]
        public string CAR_SAP { get; set; }

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