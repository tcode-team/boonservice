using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace boonservice.api.Models
{
    /// <summary>
    /// Table afs_authority  
    /// </summary>
    [Table("AFS_AUTHORITY")]
    public class afs_authority
    {
        /// <summary>
        /// รหัสผู้ใช้
        /// </summary>
        [Key]
        public double USER_ID { get; set; }

        /// <summary>
        /// พนักงานขาย
        /// </summary>
        [StringLength(1)]
        public string SALE_FLAG { get; set; }

        /// <summary>
        /// After Sale
        /// </summary>
        [StringLength(1)]
        public string AFTERSALE_FLAG { get; set; }

        /// <summary>
        /// จัดซื้อ
        /// </summary>
        [StringLength(1)]
        public string PURCHASE_FLAG { get; set; }
    }

}