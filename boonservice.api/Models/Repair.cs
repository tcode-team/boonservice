
using System;
using System.Collections.Generic;

namespace boonservice.api.Models
{
    /// <summary>
    /// Structure for Repair Search
    /// </summary>
    public class RepairSearchModel
    {
        /// <summary>
        /// Fetch data setting
        /// </summary>
        public fetchdata fetchdata;

        /// <summary>
        /// Sale Order number
        /// </summary>
        public string sonumber;

        /// <summary>
        /// Sold-to name
        /// </summary>
        public string soldto_name;

        /// <summary>
        /// วันที่แจ้งซ่อม จาก
        /// </summary>
        public string repair_date_from;

        /// <summary>
        /// วันที่แจ้งซ่อม ถึง
        /// </summary>
        public string repair_date_to;

        /// <summary>
        /// สถานะ
        /// </summary>
        public string status;

        /// <summary>
        /// เลขที่แจ้งซ่อม
        /// </summary>
        public string repair_code;
    }

    /// <summary>
    /// Repair Form
    /// </summary>
    public class RepairForm
    {
        /// <summary>
        /// Repair Header
        /// </summary>
        public RepairHeader header;

        /// <summary>
        /// Repair Items
        /// </summary>
        public ICollection<RepairItems> items;
    }

    /// <summary>
    /// Repair Header
    /// </summary>
    public class RepairHeader
    {
        /// <summary>
        /// เลขที่แจ้งซ่อม
        /// </summary>
        public string repair_code { get; set; }

        /// <summary>
        /// วันที่แจ้งซ่อม
        /// </summary>
        public DateTime? repair_date { get; set; }

        /// <summary>
        /// Sale Order Number
        /// </summary>
        public string so_number { get; set; }

        /// <summary>
        /// Sale Order Date
        /// </summary>
        public DateTime? so_date { get; set; }

        /// <summary>
        /// Sale Org. [VKORG]
        /// </summary>
        public string saleorg { get; set; }

        /// <summary>
        /// Distribution Channel [VTWEG]
        /// </summary>
        public string distchannel { get; set; }

        /// <summary>
        /// Division [SPART]
        /// </summary>
        public string division { get; set; }

        /// <summary>
        /// Sale Group [VKGRP]
        /// </summary>
        public string salegroup { get; set; }

        /// <summary>
        /// Sale Office [VKBUR]
        /// </summary>
        public string saleoffice { get; set; }

        /// <summary>
        /// รหัสลูกค้า
        /// </summary>
        public string soldto_code { get; set; }

        /// <summary>
        /// ชื่อลูกค้า
        /// </summary>
        public string soldto_name { get; set; }

        /// <summary>
        /// ที่อยู่
        /// </summary>
        public string soldto_address { get; set; }

        /// <summary>
        /// รหัสลูกค้าจัดส่ง
        /// </summary>
        public string shipto_code { get; set; }

        /// <summary>
        /// ที่อยู่จัดส่ง
        /// </summary>
        public string shipto_address { get; set; }

        /// <summary>
        /// รหัสพนักงานขาย
        /// </summary>
        public string salerep_code { get; set; }

        /// <summary>
        /// พนักงานขาย
        /// </summary>
        public string salerep_name { get; set; }

        /// <summary>
        /// วันที่ส่งสินค้า
        /// </summary>
        public DateTime? billing_date { get; set; }

        /// <summary>
        /// SO ค่าขนส่ง
        /// </summary>
        public decimal transport_amount { get; set; }

        /// <summary>
        /// เบอร์ติดต่อ
        /// </summary>
        public string contact_tel { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// สถานะ
        /// </summary>
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

    /// <summary>
    /// Repair Items
    /// </summary>
    public class RepairItems
    {
        /// <summary>
        /// Select
        /// </summary>
        public bool select { get;  set; }

        /// <summary>
        /// ID
        /// </summary>
        public double repair_item_id { get; set; }

        /// <summary>
        /// เลขที่แจ้งซ่อม
        /// </summary>
        public string repair_code { get; set; }

        /// <summary>
        /// Sale Order Number
        /// </summary>
        public string so_number { get; set; }

        /// <summary>
        /// Sale Order item
        /// </summary>
        public string so_item { get; set; }

        /// <summary>
        /// Article Number
        /// </summary>
        public string article_number { get; set; }

        /// <summary>
        /// Article Name
        /// </summary>
        public string article_name { get; set; }

        /// <summary>
        /// ประเภทสินค้า
        /// </summary>
        public string repair_item_type { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        public decimal qty { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string repair_remark { get; set; }

        /// <summary>
        /// ตำแหน่งชำรุด
        /// </summary>
        public string repair_desc { get; set; }

        /// <summary>
        /// การดำเนินการ
        /// </summary>
        public string repair_type { get; set; }

        /// <summary>
        /// ประกัน
        /// </summary>
        public string waranty { get; set; }

        /// <summary>
        /// Repair Images
        /// </summary>
        public ICollection<RepairImages> images;
    }

    /// <summary>
    /// Repair Images
    /// </summary>
    public class RepairImages
    {
        /// <summary>
        /// ID
        /// </summary>
        public double repair_image_id { get; set; }

        /// <summary>
        /// เลขที่แจ้งซ่อม
        /// </summary>
        public string repair_code { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public double repair_item_id { get; set; }

        /// <summary>
        /// Sale Order Number
        /// </summary>
        public string so_number { get; set; }

        /// <summary>
        /// Sale Order item
        /// </summary>
        public string so_item { get; set; }

        /// <summary>
        /// File Name
        /// </summary>
        public string filename { get; set; }

        /// <summary>
        /// Image Base64s
        /// </summary>
        public String base64 { get; set; }
        
    }
}