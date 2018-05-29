using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace boonservice.api.Models
{

    public class ShipmentSearchModel
    {
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
    /// Table VTTK  
    /// </summary>
    [Table("VTTK")]
    public class VTTK
    {
        /// <summary>
        /// Client
        /// </summary>
        [Key, Column(Order = 0)]
        [StringLength(9)]
        public string MANDT { get; set; }

        /// <summary>
        /// Shipment Number
        /// </summary>
        [Key, Column(Order = 1)]
        [StringLength(30)]
        public string TKNUM { get; set; }
    
        /// <summary>
        /// Shipment date
        /// </summary>
        [StringLength(20)]
        public string ERDAT { get; set; }

        /// <summary>
        /// Shipment Type
        /// </summary>
        [StringLength(2)]
        public string VSART { get; set; }

        /// <summary>
        /// Route
        /// </summary>
        [StringLength(18)]
        public string ROUTE { get; set; }

        /// <summary>
        /// Container ID
        /// </summary>
        [StringLength(60)]
        public string SIGNI { get; set; }

        /// <summary>
        /// Number of forwarding agent
        /// </summary>
        [StringLength(10)]
        public string TDLNR { get; set; }

        ///// <summary>
        ///// Shipment Type List
        ///// </summary>
        //public virtual List<T173T> T173Ts { get; set; }
    }

    /// <summary>
    /// Table T173T  
    /// </summary>
    [Table("T173T")]
    public class T173T
    {
        /// <summary>
        /// Client
        /// </summary>
        [Key, Column(Order = 0)]
        [StringLength(9)]
        public string MANDT { get; set; }

        /// <summary>
        /// Language
        /// </summary>
        [Key, Column(Order = 1)]
        [StringLength(1)]
        public string SPRAS { get; set; }

        /// <summary>
        /// Shipment Type
        /// </summary>
        [Key, Column(Order = 2)]
        [StringLength(2)]
        public string VSART { get; set; }

        /// <summary>
        /// Description of the Shipping Type
        /// </summary>
        [StringLength(20)]
        public string BEZEI { get; set; }
        
    }

}