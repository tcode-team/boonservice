using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace boonservice.api.Models
{
    /// <summary>
    /// Structure for Shipment Search
    /// </summary>
    public class ShipmentSearchModel
    {
        /// <summary>
        /// Fetch data setting
        /// </summary>
        public fetchdata fetchdata;

        /// <summary>
        /// forwarding agent
        /// </summary>
        public string forwarding;

        /// <summary>
        /// Shipment date from
        /// </summary>
        public string ShipmentDateFrom;

        /// <summary>
        /// Shipment date to
        /// </summary>
        public string ShipmentDateTo;

        /// <summary>
        /// Shipment Number
        /// </summary>
        public string ShipmentNo;

        /// <summary>
        /// Shipment Type
        /// </summary>
        public string ShipmentType;

        /// <summary>
        /// กลุ่มรถ
        /// </summary>
        public string CarGroup;

        /// <summary>
        /// ทะเบียนรถ
        /// </summary>
        public string CarLicense;

        /// <summary>
        /// สถานะ
        /// </summary>
        public string ShipmentStatus;
    }

    /// <summary>
    /// Structure for Shipment Plan Search
    /// </summary>
    public class ShipmentPlanSearchModel
    {
        /// <summary>
        /// Fetch data setting
        /// </summary>
        public fetchdata fetchdata;

        /// <summary>
        /// วันที่ส่ง
        /// </summary>
        public string transport_date;

        /// <summary>
        /// Shipment Number
        /// </summary>
        public string ShipmentNo;

        /// <summary>
        /// Shipment Type
        /// </summary>
        public string ShipmentType;

        /// <summary>
        /// ทะเบียนรถ
        /// </summary>
        public string CarLicense;

        /// <summary>
        /// Container ID
        /// </summary>
        public string ContainerID;

        /// <summary>
        /// SO Number
        /// </summary>
        public string SONumber;
    }

    /// <summary>
    /// Structure for Shipment Summary Search
    /// </summary>
    public class ShipmentSummarySearchModel
    {
        /// <summary>
        /// Fetch data setting
        /// </summary>
        public fetchdata fetchdata;

        /// <summary>
        /// รหัสพนักงาน
        /// </summary>
        public Int16 identity_id;

        /// <summary>
        /// วันที่ส่ง
        /// </summary>
        public string transport_month;
    }

    /// <summary>
    /// Shipment Type
    /// </summary>
    public class ShipmentTypeModel
    {
        /// <summary>
        /// Client
        /// </summary>
        [Key, Column(Order = 0)]
        [StringLength(9)]
        public string client { get; set; }

        /// <summary>
        /// Language
        /// </summary>
        [Key, Column(Order = 1)]
        [StringLength(1)]
        public string lang_code { get; set; }

        /// <summary>
        /// Shipment Type
        /// </summary>
        [Key, Column(Order = 2)]
        [StringLength(2)]
        public string shipmenttype_code { get; set; }

        /// <summary>
        /// Description of the Shipping Type
        /// </summary>
        [StringLength(20)]
        public string shipmenttype_desc { get; set; }
    }

    /// <summary>
    /// Route
    /// </summary>
    public class RouteModel
    {
        /// <summary>
        /// Client
        /// </summary>
        [Key, Column(Order = 0)]
        [StringLength(9)]
        public string client { get; set; }

        /// <summary>
        /// Language
        /// </summary>
        [Key, Column(Order = 1)]
        [StringLength(1)]
        public string lang_code { get; set; }

        /// <summary>
        /// Route
        /// </summary>
        [Key, Column(Order = 2)]
        [StringLength(2)]
        public string route_code { get; set; }

        /// <summary>
        /// Description of Route
        /// </summary>
        [StringLength(40)]
        public string route_desc { get; set; }
    }

    /// <summary>
    /// Shipment Confirm 
    /// </summary>
    public class ShipmentConfirmModeal
    {
        /// <summary>
        /// Client
        /// </summary>
        [StringLength(9)]
        public string client { get; set; }

        /// <summary>
        /// Shipment Number
        /// </summary>
        [StringLength(30)]
        public string shipment_number { get; set; }

        /// <summary>
        /// Confirm by userid
        /// </summary>
        public int confirm_by { get; set; }
    }

    /// <summary>
    /// Shipment Detail
    /// </summary>
    public class ShipmentDetailModel
    {
        /// <summary>
        /// Client
        /// </summary>
        [Key, Column(Order = 0)]
        [StringLength(9)]
        public string client { get; set; }

        /// <summary>
        /// Shipment Number
        /// </summary>
        [Key, Column(Order = 1)]
        [StringLength(30)]
        public string shipment_number { get; set; }

        /// <summary>
        /// Shipment date
        /// </summary>
        public DateTime? shipment_date { get; set; }

        /// <summary>
        /// วันที่ส่ง
        /// </summary>
        public string transport_date { get; set; }

        /// <summary>
        /// Shipment Type
        /// </summary>
        [StringLength(2)]
        public string shipmenttype_code { get; set; }

        /// <summary>
        /// Description of the Shipping Type
        /// </summary>
        [StringLength(20)]
        public string shipmenttype_desc { get; set; }

        /// <summary>
        /// Route
        /// </summary>
        [StringLength(18)]
        public string route { get; set; }

        /// <summary>
        /// Description of Route
        /// </summary>
        [StringLength(40)]
        public string route_desc { get; set; }

        /// <summary>
        /// รหัสกลุ่มรถ
        /// </summary>
        [StringLength(2)]
        public string cargroup_code { get; set; }

        /// <summary>
        /// รายละเอียดกลุ่มรถ
        /// </summary>
        [StringLength(50)]
        public string cargroup_desc { get; set; }

        /// <summary>
        /// Container ID
        /// </summary>
        [StringLength(60)]
        public string container_id { get; set; }

        /// <summary>
        /// Car License (ทะเบียนรถ)
        /// </summary>
        [StringLength(10)]
        public string car_license { get; set; }

        /// <summary>
        /// รหัสคนขับ afs_car_identity_card
        /// </summary>
        public Int16 driver_id { get; set; }

        /// <summary>
        /// รหัสเด็กรถ 1 afs_car_identity_card
        /// </summary>
        public Int16 staff1_id { get; set; }

        /// <summary>
        /// รหัสเด็กรถ 2 afs_car_identity_card
        /// </summary>
        public Int16 staff2_id { get; set; }

        /// <summary>
        /// จุดส่ง
        /// </summary>
        public Int16 point_id { get; set; }

        /// <summary>
        /// Number of forwarding agent
        /// </summary>
        [StringLength(10)]
        public string forwarding { get; set; }

        /// <summary>
        /// Transport amount
        /// </summary>
        public decimal transport_amount { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [StringLength(2)]
        public string status_code { get; set; }

        /// <summary>
        /// Status description
        /// </summary>
        [StringLength(80)]
        public string status_desc { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        [StringLength(250)]
        public string remark { get; set; }

        /// <summary>
        /// รหัสผู้ confirm
        /// </summary>
        public int confirm_by { get; set; }

        /// <summary>
        /// วันที่ confirm
        /// </summary>
        public DateTime? confirm_date { get; set; }

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

        /// <summary>
        /// Shipment รายการค่าขนส่ง 
        /// </summary>
        public List<ShipmentCarries> shipment_carries { get; set; }

        /// <summary>
        /// Shipment รายการค่าอื่น ๆ
        /// </summary>
        public List<ShipmentExpense> shipment_expense { get; set; }
    }    

    /// <summary>
    /// Shipment รายการค่าขนส่ง
    /// </summary>
    public class ShipmentCarries
    {
        /// <summary>
        /// Client
        /// </summary>
        [Key, Column(Order = 0)]
        [StringLength(9)]
        public string client { get; set; }

        /// <summary>
        /// Shipment Number
        /// </summary>
        [Key, Column(Order = 1)]
        [StringLength(30)]
        public string shipment_number { get; set; }

        /// <summary>
        /// Item No
        /// </summary>
        public int itemno { get; set; }

        /// <summary>
        /// จุดส่ง
        /// </summary>
        [StringLength(150)]
        public string point_desc { get; set; }

        /// <summary>
        /// ช่วงเวลา
        /// </summary>
        public string time_range { get; set; }

        /// <summary>
        /// Sale Order Number
        /// </summary>
        public string so_number { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// คนขับ จำนวนเงิน
        /// </summary>
        public decimal driver_amount { get; set; }

        /// <summary>
        /// เด็กรถ จำนวนเงิน
        /// </summary>
        public decimal staff_amount { get; set; }

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
    /// Shipment รายการค่าอื่น ๆ
    /// </summary>
    public class ShipmentExpense
    {
        /// <summary>
        /// Client
        /// </summary>
        [Key, Column(Order = 0)]
        [StringLength(9)]
        public string client { get; set; }

        /// <summary>
        /// Shipment Number
        /// </summary>
        [Key, Column(Order = 1)]
        [StringLength(30)]
        public string shipment_number { get; set; }

        /// <summary>
        /// Item No
        /// </summary>
        public int itemno { get; set; }

        /// <summary>
        /// รหัสค่าใช้จ่าย
        /// </summary>
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

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        [StringLength(250)]
        public string remark { get; set; }

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
    /// Shipment Plan (ตารางงานจัดส่ง)
    /// </summary>
    public class ShipmentPlanModel
    {
        /// <summary>
        /// วันที่ส่ง
        /// </summary>
        public DateTime? transport_date;

        /// <summary>
        /// Shipment Number
        /// </summary>
        public string shipment_number;

        /// <summary>
        /// ทะเบียนรถ
        /// </summary>
        public string car_license;

        /// <summary>
        /// ชื่อพนักงานขับรถ
        /// </summary>
        public string containerid;

        /// <summary>
        /// Sort ช่วงเวลา
        /// </summary>
        public int time_sort;

        /// <summary>
        /// ช่วงเวลา
        /// </summary>
        public string time_range;

        /// <summary>
        /// Sale Order Number
        /// </summary>
        public string so_number;

        /// <summary>
        /// Sale Office Code
        /// </summary>
        public string saleoffice_code;

        /// <summary>
        /// Sale Office Name
        /// </summary>
        public string saleoffice_name;

        /// <summary>
        /// จุดส่ง
        /// </summary>
        public string point_desc;

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string remark;

        /// <summary>
        /// มูลค่า SO (final amount)
        /// </summary>
        public decimal so_amount;

        /// <summary>
        /// สถานะ
        /// </summary>
        public string status;

        /// <summary>
        /// Count Article ใน SO
        /// </summary>
        public int article_count;

        /// <summary>
        /// Sum Article Qty ใน SO
        /// </summary>
        public decimal article_sum_qty;
    }

    /// <summary>
    /// รายงานเอกสารค่าเที่ยวสรุปบัญชี พนักงาน
    /// </summary>
    public class ShipmentIdentitySummaryModel
    {
        /// <summary>
        /// รหัส
        /// </summary>
        public Int16 identity_id;

        /// <summary>
        /// ชื่อพนักงาน
        /// </summary>
        public string identity_name;

        /// <summary>
        /// เดือน
        /// </summary>
        public string month_name;

        /// <summary>
        /// ปี
        /// </summary>
        public string year;

        /// <summary>
        /// Shipment header summary
        /// </summary>
        public ICollection<ShipmentSummaryModel> shipment;
    }

    /// <summary>
    /// Shipment header summary
    /// </summary>
    public class ShipmentSummaryModel
    {
        /// <summary>
        /// วันที่ส่ง
        /// </summary>
        public DateTime? transport_date;

        /// <summary>
        /// Shipment Number
        /// </summary>
        public string shipment_number;

        /// <summary>
        /// ประเภทพนักงาน
        /// </summary>
        public string identity_type;

        /// <summary>
        /// รวมค่าอื่น ๆ
        /// </summary>
        public decimal total_expense;

        /// <summary>
        /// รวมค่าจุด
        /// </summary>
        public decimal total_point;

        /// <summary>
        /// Shipment point 
        /// </summary>
        public ICollection<ShipmentPointSummaryModel> point;


        /// <summary>
        /// Shipment expense
        /// </summary>
        public ICollection<ShipmentExpenseSummaryModel> expense;
    }

    /// <summary>
    /// Shipment point summary
    /// </summary>
    public class ShipmentPointSummaryModel
    {
        /// <summary>
        /// จุดส่ง
        /// </summary>
        public string point_desc;

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string remark;

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        public decimal amount;
    }

    /// <summary>
    /// Shipment expense summary
    /// </summary>
    public class ShipmentExpenseSummaryModel
    {
        /// <summary>
        /// รหัสค่าอื่นๆ
        /// </summary>
        public int expense_id;

        /// <summary>
        /// รายละเอียดค่าอื่น ๆ 
        /// </summary>
        public string expense_desc;

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string remark;

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        public decimal amount;
    }
}