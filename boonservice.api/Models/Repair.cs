
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
        /// Sale Order Number
        /// </summary>
        public string so_number;

        /// <summary>
        /// Sale Order Date
        /// </summary>
        public DateTime? so_date;

        /// <summary>
        /// รหัสลูกค้า
        /// </summary>
        public string soldto_code;

        /// <summary>
        /// ชื่อลูกค้า
        /// </summary>
        public string soldto_name;

        /// <summary>
        /// ที่อยู่
        /// </summary>
        public string soldto_address;

        /// <summary>
        /// รหัสลูกค้าจัดส่ง
        /// </summary>
        public string shipto_code;

        /// <summary>
        /// ที่อยู่จัดส่ง
        /// </summary>
        public string shipto_address;

        /// <summary>
        /// รหัสพนักงานขาย
        /// </summary>
        public string salerep_code;

        /// <summary>
        /// พนักงานขาย
        /// </summary>
        public string salerep_name;

        /// <summary>
        /// วันที่ส่งสินค้า
        /// </summary>
        public DateTime? billing_date;

        /// <summary>
        /// SO ค่าขนส่ง
        /// </summary>
        public decimal transport_amount;

        /// <summary>
        /// เบอร์ติดต่อ
        /// </summary>
        public string contact_tel;

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string remark;
    }

    /// <summary>
    /// Repair Items
    /// </summary>
    public class RepairItems
    {
        /// <summary>
        /// Sale Order Number
        /// </summary>
        public string so_number;

        /// <summary>
        /// Sale Order item
        /// </summary>
        public string so_item;

        /// <summary>
        /// Article Number
        /// </summary>
        public string article_number;

        /// <summary>
        /// Article Name
        /// </summary>
        public string article_name;

        /// <summary>
        /// Quantity
        /// </summary>
        public decimal qty;


    }
}