using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace boonservice.api.Models
{
    /// <summary>
    /// Table afs_repair_item_type  
    /// </summary>
    [Table("AFS_REPAIR_ITEM_TYPE")]
    public class afs_repair_item_type
    {
        /// <summary>
        /// ประเภทสินค้า
        /// </summary>
        [StringLength(80)]
        public string ITEM_TYPE { get; set; }
    }

}