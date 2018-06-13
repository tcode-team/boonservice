using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace boonservice.api.Models
{
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

        /// <summary>
        /// Suppl. 1 (ทะเบียนรถ)
        /// </summary>
        [StringLength(10)]
        public string ADD01 { get; set; }

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

    /// <summary>
    /// Table TVROT  
    /// </summary>
    [Table("TVROT")]
    public class TVROT
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
        /// Route
        /// </summary>
        [Key, Column(Order = 2)]
        [StringLength(6)]
        public string ROUTE { get; set; }

        /// <summary>
        /// Description of Route
        /// </summary>
        [StringLength(40)]
        public string BEZEI { get; set; }
    }

    /// <summary>
    /// Table VBAK  
    /// </summary>
    [Table("VBAK")]
    public class VBAK
    {
        /// <summary>
        /// Client
        /// </summary>
        [Key, Column(Order = 0)]
        [StringLength(9)]
        public string MANDT { get; set; }

        /// <summary>
        /// Sales Document
        /// </summary>
        [Key, Column(Order = 1)]
        [StringLength(30)]
        public string VBELN { get; set; }

        /// <summary>
        /// Date on Which Record Was Created
        /// </summary>
        [StringLength(24)]
        public string ERDAT { get; set; }

        /// <summary>
        /// Sold-to party
        /// </summary>
        [StringLength(30)]
        public string KUNNR { get; set; }

        /// <summary>
        /// SD document category
        /// </summary>
        [StringLength(3)]
        public string VBTYP { get; set; }
    }

    /// <summary>
    /// Table VBAP  
    /// </summary>
    [Table("VBAP")]
    public class VBAP
    {
        /// <summary>
        /// Client
        /// </summary>
        //[Key]
        [Column(Order = 0)]
        [StringLength(9)]
        public string MANDT { get; set; }

        /// <summary>
        /// Sales Document
        /// </summary>
        //[Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string VBELN { get; set; }

        /// <summary>
        /// Sales Document Item
        /// </summary>
        //[Key]
        [Column(Order = 2)]
        [StringLength(18)]
        public string POSNR { get; set; }

        /// <summary>
        /// Article Number
        /// </summary>
        [StringLength(54)]
        public string MATNR { get; set; }

        /// <summary>
        /// Short text for sales order item
        /// </summary>
        [StringLength(120)]
        public string ARKTX { get; set; }

        /// <summary>
        /// Cumulative Order Quantity in Sales Units
        /// </summary>
        public decimal KWMENG { get; set; }

        /// <summary>
        /// Base Unit of Measure
        /// </summary>
        [StringLength(9)]
        public string MEINS { get; set; }
    }

    /// <summary>
    /// Table VBPA  
    /// </summary>
    [Table("VBPA")]
    public class VBPA
    {
        /// <summary>
        /// Client
        /// </summary>
        [Key, Column(Order = 0)]
        [StringLength(9)]
        public string MANDT { get; set; }

        /// <summary>
        /// Sales Document
        /// </summary>
        [Key, Column(Order = 1)]
        [StringLength(30)]
        public string VBELN { get; set; }

        /// <summary>
        /// Sales Document Item
        /// </summary>
        [Key, Column(Order = 2)]
        [StringLength(18)]
        public string POSNR { get; set; }

        /// <summary>
        /// Partner Function
        /// </summary>
        [Key, Column(Order = 3)]
        [StringLength(6)]
        public string PARVW { get; set; }

        /// <summary>
        /// Customer Number 1
        /// </summary>
        [StringLength(30)]
        public string KUNNR { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        [StringLength(30)]
        public string ADRNR { get; set; }
    }

    /// <summary>
    /// Table KNA1  
    /// </summary>
    [Table("KNA1")]
    public class KNA1
    {
        /// <summary>
        /// Client
        /// </summary>
        //[Key]
        [Column(Order = 0)]
        [StringLength(9)]
        public string MANDT { get; set; }

        /// <summary>
        /// Customer Number 1
        /// </summary>
        //[Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string KUNNR { get; set; }

        /// <summary>
        /// Country Key
        /// </summary>
        [StringLength(9)]
        public string LAND1 { get; set; }

        /// <summary>
        /// Name 1
        /// </summary>
        [StringLength(105)]
        public string NAME1 { get; set; }

        /// <summary>
        /// Name 2
        /// </summary>
        [StringLength(105)]
        public string NAME2 { get; set; }

        /// <summary>
        /// Sort field
        /// </summary>
        [StringLength(30)]
        public string SORTL { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        [StringLength(30)]
        public string ADRNR { get; set; }
    }

    /// <summary>
    /// Table ADRC  
    /// </summary>
    [Table("ADRC")]
    public class ADRC
    {
        /// <summary>
        /// Client
        /// </summary>
        [Key, Column(Order = 0)]
        [StringLength(9)]
        public string CLIENT { get; set; }

        /// <summary>
        /// Address number
        /// </summary>
        [Key, Column(Order = 1)]
        [StringLength(30)]
        public string ADDRNUMBER { get; set; }

        /// <summary>
        /// Valid-from date - in current Release only 00010101 possible
        /// </summary>
        [Key, Column(Order = 2)]
        [StringLength(24)]
        public string DATE_FROM { get; set; }

        /// <summary>
        /// International address version ID
        /// </summary>
        [Key, Column(Order = 3)]
        [StringLength(3)]
        public string NATION { get; set; }

        /// <summary>
        /// Form-of-Address Key
        /// </summary>
        [StringLength(12)]
        public string TITLE { get; set; }

        /// <summary>
        /// Name 1
        /// </summary>
        [StringLength(120)]
        public string NAME1 { get; set; }

        /// <summary>
        /// Name 2
        /// </summary>
        [StringLength(120)]
        public string NAME2 { get; set; }

        /// <summary>
        /// Name 3
        /// </summary>
        [StringLength(120)]
        public string NAME3 { get; set; }

        /// <summary>
        /// Name 4
        /// </summary>
        [StringLength(120)]
        public string NAME4 { get; set; }

        /// <summary>
        /// City
        /// </summary>
        [StringLength(120)]
        public string CITY1 { get; set; }

        /// <summary>
        /// District
        /// </summary>
        [StringLength(120)]
        public string CITY2 { get; set; }

        /// <summary>
        /// City postal code
        /// </summary>
        [StringLength(30)]
        public string POST_CODE1 { get; set; }

        /// <summary>
        /// PO Box postal code
        /// </summary>
        [StringLength(30)]
        public string POST_CODE2 { get; set; }

        /// <summary>
        /// Company postal code (for large customers)
        /// </summary>
        [StringLength(30)]
        public string POST_CODE3 { get; set; }

        /// <summary>
        /// Transportation zone to or from which the goods are delivered
        /// </summary>
        [StringLength(30)]
        public string TRANSPZONE { get; set; }

        /// <summary>
        /// Street
        /// </summary>
        [StringLength(180)]
        public string STREET { get; set; }

        /// <summary>
        /// Street Number for City/Street File
        /// </summary>
        [StringLength(36)]
        public string STREETCODE { get; set; }

        /// <summary>
        /// House Number
        /// </summary>
        [StringLength(30)]
        public string HOUSE_NUM1 { get; set; }

        /// <summary>
        /// House number supplement
        /// </summary>
        [StringLength(30)]
        public string HOUSE_NUM2 { get; set; }

        /// <summary>
        /// (Not supported) House Number Range
        /// </summary>
        [StringLength(30)]
        public string HOUSE_NUM3 { get; set; }

        /// <summary>
        /// Street 2
        /// </summary>
        [StringLength(120)]
        public string STR_SUPPL1 { get; set; }

        /// <summary>
        /// Street 3
        /// </summary>
        [StringLength(120)]
        public string STR_SUPPL2 { get; set; }

        /// <summary>
        /// Street 4
        /// </summary>
        [StringLength(120)]
        public string STR_SUPPL3 { get; set; }

        /// <summary>
        /// Street 5
        /// </summary>
        [StringLength(120)]
        public string LOCATION { get; set; }

        /// <summary>
        /// Building (Number or Code)
        /// </summary>
        [StringLength(60)]
        public string BUILDING { get; set; }

        /// <summary>
        /// Floor in building
        /// </summary>
        [StringLength(30)]
        public string FLOOR { get; set; }

        /// <summary>
        /// Room or Appartment Number
        /// </summary>
        [StringLength(30)]
        public string ROOMNUMBER { get; set; }

        /// <summary>
        /// Country Key
        /// </summary>
        [StringLength(9)]
        public string COUNTRY { get; set; }

        /// <summary>
        /// Language Key
        /// </summary>
        [StringLength(3)]
        public string LANGU { get; set; }

        /// <summary>
        /// Region (State, Province, County)
        /// </summary>
        [StringLength(9)]
        public string REGION { get; set; }

        /// <summary>
        /// Search Term 1
        /// </summary>
        [StringLength(60)]
        public string SORT1 { get; set; }

        /// <summary>
        /// Search Term 2
        /// </summary>
        [StringLength(60)]
        public string SORT2 { get; set; }

        /// <summary>
        /// First telephone no.: dialling code+number
        /// </summary>
        [StringLength(90)]
        public string TEL_NUMBER { get; set; }

        /// <summary>
        /// First Telephone No.: Extension
        /// </summary>
        [StringLength(30)]
        public string TEL_EXTENS { get; set; }

        /// <summary>
        /// First fax no.: dialling code+number
        /// </summary>
        [StringLength(90)]
        public string FAX_NUMBER { get; set; }

        /// <summary>
        /// First fax no.: extension
        /// </summary>
        [StringLength(30)]
        public string FAX_EXTENS { get; set; }
    }

    /// <summary>
    /// Table VBRK  
    /// </summary>
    [Table("VBRK")]
    public class VBRK
    {
        /// <summary>
        /// Client
        /// </summary>
        [Key, Column(Order = 0)]
        [StringLength(9)]
        public string MANDT { get; set; }

        /// <summary>
        /// Billing Document
        /// </summary>
        [Key, Column(Order = 1)]
        [StringLength(30)]
        public string VBELN { get; set; }

        /// <summary>
        /// Billing Type
        /// </summary>
        [StringLength(12)]
        public string FKART { get; set; }

        /// <summary>
        /// Billing category
        /// </summary>
        [StringLength(3)]
        public string FKTYP { get; set; }

        /// <summary>
        /// SD document category
        /// </summary>
        [StringLength(3)]
        public string VBTYP { get; set; }

        /// <summary>
        /// Sales Organization
        /// </summary>
        [StringLength(12)]
        public string VKORG { get; set; }

        /// <summary>
        /// Billing date for billing index and printout
        /// </summary>
        [StringLength(24)]
        public string FKDAT { get; set; }

        /// <summary>
        /// Assignment number
        /// </summary>
        [StringLength(54)]
        public string ZUONR { get; set; }
    }
}