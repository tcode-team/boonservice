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
    [Table("afs_carries_point")]
    public class afs_carries_point
    {
        /// <summary>
        /// รหัส
        /// </summary>
        [Key]
        public Int16 point_id { get; set; }

        /// <summary>
        /// รหัสกลุ่มรถ
        /// </summary>
        [StringLength(2)]
        public string cargroup_code { get; set; }

        /// <summary>
        /// ระยะทาง
        /// </summary>
        [StringLength(250)]
        public string tier_desc { get; set; }

        /// <summary>
        /// พนักงานขับรถ 1
        /// </summary>
        public decimal dpoint1_amount { get; set; }

        /// <summary>
        /// พนักงานขับรถ 2
        /// </summary>
        public decimal dpoint2_amount { get; set; }

        /// <summary>
        /// เด็กรถ 1
        /// </summary>
        public decimal spoint1_amount { get; set; }

        /// <summary>
        /// เด็กรถ 2
        /// </summary>
        public decimal spoint2_amount { get; set; }

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