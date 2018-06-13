using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace boonservice.api.Models
{
    /// <summary>
    /// Table afs_repair_header  
    /// </summary>
    [Table("AFS_REPAIR_HEADER")]
    public class afs_repair_header
    {
        /// <summary>
        /// รหัสกลุ่มรถ
        /// </summary>
        [Key]
        [StringLength(2)]
        public string CARGROUP_CODE { get; set; }

        /// <summary>
        /// รายละเอียดกลุ่มรถ
        /// </summary>
        [StringLength(50)]
        public string CARGROUP_DESC { get; set; }
    }

}