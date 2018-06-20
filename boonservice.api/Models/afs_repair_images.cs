using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace boonservice.api.Models
{
    /// <summary>
    /// Table afs_repair_images
    /// </summary>
    [Table("AFS_REPAIR_IMAGES")]
    public class afs_repair_images
    {
        /// <summary>
        /// ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public double REPAIR_IMAGE_ID { get; set; }

        /// <summary>
        /// รหัสแจ้งซ่อม
        /// </summary>
        [StringLength(11)]
        public string REPAIR_CODE { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public double REPAIR_ITEM_ID { get; set; }

        /// <summary>
        /// Sale Order Number
        /// </summary>
        [StringLength(30)]
        public string SO_NUMBER { get; set; }

        /// <summary>
        /// Sale Order item
        /// </summary>
        [StringLength(18)]
        public string SO_ITEM { get; set; }

        /// <summary>
        /// File Name
        /// </summary>
        [StringLength(120)]
        public string FILENAME { get; set; }

        /// <summary>
        /// Image Base64
        /// </summary>
        public string BASE64 { get; set; }
    }


}