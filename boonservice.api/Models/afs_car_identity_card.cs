using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace boonservice.api.Models
{
    /// <summary>
    /// Table afs_car_identity_card  
    /// </summary>
    [Table("afs_car_identity_card")]
    public class afs_car_identity_card
    {
        /// <summary>
        /// รหัส
        /// </summary>
        [Key]
        public Int16 people_id { get; set; }

        /// <summary>
        /// ชื่อนามสกุล
        /// </summary>
        [StringLength(120)]
        public string name { get; set; }

        /// <summary>
        /// เลขที่บัตรประจำตัวประชาชน
        /// </summary>
        [StringLength(20)]
        public string card_code { get; set; }

        /// <summary>
        /// เลขที่ passport
        /// </summary>
        [StringLength(20)]
        public string passport_code { get; set; }

        /// <summary>
        /// รหัสสัญชาติ
        /// </summary>
        [StringLength(3)]
        public string nation { get; set; }

        /// <summary>
        /// พนักงานขับรถ
        /// </summary>
        [StringLength(1)]
        public string driver_flag { get; set; }

        /// <summary>
        /// เด็กรถ
        /// </summary>
        [StringLength(1)]
        public string staff_flag { get; set; }
    }

}