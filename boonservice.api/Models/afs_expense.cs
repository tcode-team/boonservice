using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace boonservice.api.Models
{
    /// <summary>
    /// Table afs_expense  
    /// </summary>
    [Table("afs_expense")]
    public class afs_expense
    {
        /// <summary>
        /// รหัสค่าใช้จ่าย
        /// </summary>
        [Key]
        public int expense_id { get; set; }

        /// <summary>
        /// รายละเอียดค่าใช้จ่าย
        /// </summary>
        [StringLength(80)]
        public string expense_desc { get; set; }

        /// <summary>
        /// มูลค่า
        /// </summary>
        public decimal expense_amount { get; set; }
    }

}