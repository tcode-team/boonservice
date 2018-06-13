
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
    /// Repair Header
    /// </summary>
    public class RepairHeader
    {
        /// <summary>
        /// Sale Order Number
        /// </summary>
        public string sonumber;

        /// <summary>
        /// Sale Order Date
        /// </summary>
        public string sodate;

        /// <summary>
        /// รหัสลูกค้า
        /// </summary>
        public string customercode;

        /// <summary>
        /// ชื่อลูกค้า
        /// </summary>
        public string customername;

        /// <summary>
        /// ที่อยู่
        /// </summary>
        public string customeraddress;

        /// <summary>
        /// ที่อยู่จัดส่ง
        /// </summary>
        public string transportaddress;

        /// <summary>
        /// พนักงานขาย
        /// </summary>
        public string salerep;

        /// <summary>
        /// วันที่ส่งสินค้า
        /// </summary>
        public string transportdate;

        /// <summary>
        /// SO ค่าขนส่ง
        /// </summary>
        public decimal transportamount;

        /// <summary>
        /// เบอร์ติดต่อ
        /// </summary>
        public string customertel;

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string remark;
    }

}