using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace boonservice.api.Models
{
    /// <summary>
    /// Table afs_carries_point
    /// </summary>
    [Table("AFS_CARRIES_POINT")]
    public class afs_carries_point
    {
        /// <summary>
        /// รหัส
        /// </summary>
        [Key]
        public Int16 POINT_ID { get; set; }

        /// <summary>
        /// รหัสกลุ่มรถ
        /// </summary>
        [StringLength(2)]
        public string CARGROUP_CODE { get; set; }

        /// <summary>
        /// ระยะทาง
        /// </summary>
        [StringLength(250)]
        public string TIER_DESC { get; set; }

        /// <summary>
        /// พนักงานขับรถ 1
        /// </summary>
        public decimal DPOINT1_AMOUNT { get; set; }

        /// <summary>
        /// พนักงานขับรถ 2
        /// </summary>
        public decimal DPOINT2_AMOUNT { get; set; }

        /// <summary>
        /// เด็กรถ 1
        /// </summary>
        public decimal SPOINT1_AMOUNT { get; set; }

        /// <summary>
        /// เด็กรถ 2
        /// </summary>
        public decimal SPOINT2_AMOUNT { get; set; }

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