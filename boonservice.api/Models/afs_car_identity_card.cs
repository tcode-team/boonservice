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
    [Table("AFS_CAR_IDENTITY_CARD")]
    public class afs_car_identity_card
    {
        /// <summary>
        /// รหัส
        /// </summary>
        [Key]
        public Int16 PEOPLE_ID { get; set; }

        /// <summary>
        /// ชื่อนามสกุล
        /// </summary>
        [StringLength(120)]
        public string NAME { get; set; }

        /// <summary>
        /// เลขที่บัตรประจำตัวประชาชน
        /// </summary>
        [StringLength(20)]
        public string CARD_CODE { get; set; }

        /// <summary>
        /// เลขที่ passport
        /// </summary>
        [StringLength(20)]
        public string PASSPORT_CODE { get; set; }

        /// <summary>
        /// รหัสสัญชาติ
        /// </summary>
        [StringLength(3)]
        public string NATION { get; set; }

        /// <summary>
        /// พนักงานขับรถ
        /// </summary>
        [StringLength(1)]
        public string DRIVER_FLAG { get; set; }

        /// <summary>
        /// เด็กรถ
        /// </summary>
        [StringLength(1)]
        public string STAFF_FLAG { get; set; }
    }

}