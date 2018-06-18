﻿using System;
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
        /// รหัสแจ้งซ่อม
        /// </summary>
        [Key]
        [StringLength(11)]
        public string repair_code { get; set; }

        /// <summary>
        /// วันที่แจ้งซ่อม
        /// </summary>
        public DateTime? repair_date { get; set; }

        /// <summary>
        /// Sale Order Number
        /// </summary>
        [StringLength(30)]
        public string so_number { get; set; }

        /// <summary>
        /// SO ค่าขนส่ง
        /// </summary>
        public decimal transport_amount { get; set; }

        /// <summary>
        /// เบอร์ติดต่อ
        /// </summary>
        [StringLength(50)]
        public string contact_tel { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        [StringLength(255)]
        public string remark { get; set; }
        
        /// <summary>
        /// สถานะ
        /// </summary>
        [StringLength(20)]
        public string status { get; set; }

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