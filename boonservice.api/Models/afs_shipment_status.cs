using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace boonservice.api.Models
{
    /// <summary>
    /// Table afs_shipment_status  
    /// </summary>
    [Table("AFS_SHIPMENT_STATUS")]
    public class afs_shipment_status
    {
        /// <summary>
        /// รหัสสถานะ
        /// </summary>
        [Key]
        [StringLength(2)]
        public string STATUS_CODE { get; set; }

        /// <summary>
        /// รายละเอียดสถานะ
        /// </summary>
        [StringLength(50)]
        public string STATUS_DESC { get; set; }
    }

}