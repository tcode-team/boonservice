﻿using System;
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