using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace boonservice.api.Models
{
    /// <summary>
    /// Table B3G_USERS  
    /// </summary>
    [Table("B3G_USERS")]
    public class B3G_USERS
    {
        /// <summary>
        /// id of user
        /// </summary>
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int USER_ID { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        [StringLength(30)]
        public string USER_NAME { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [StringLength(20)]
        public string PASSWORD { get; set; }

        /// <summary>
        /// user type code
        /// </summary>
        [StringLength(3)]
        public string USER_TYPE_CODE { get; set; }

        /// <summary>
        /// sex
        /// </summary>
        [StringLength(1)]
        public string SEX { get; set; }

        /// <summary>
        /// Employee code
        /// </summary>
        [StringLength(20)]
        public string EMP_CODE { get; set; }

        /// <summary>
        /// Vendor 
        /// </summary>
        [StringLength(20)]
        public string VENDOR_ID { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [StringLength(100)]
        public string EMAIL { get; set; }

        /// <summary>
        /// Branch id
        /// </summary>
        [StringLength(10)]
        public string BRANCH_ID { get; set; }

        /// <summary>
        /// sex
        /// </summary>
        [StringLength(20)]
        public string SAP_USER { get; set; }

        /// <summary>
        /// Sale
        /// </summary>
        [StringLength(20)]
        public string SALE_REP { get; set; }

        /// <summary>
        /// Position
        /// </summary>
        public Int32? POSITION_ID { get; set; }

        /// <summary>
        /// Cashier flag
        /// </summary>
        [StringLength(1)]
        public string CASHIER_FLAG { get; set; }

        /// <summary>
        /// Showroom flag
        /// </summary>
        [StringLength(1)]
        public string SHOWROOM_FLAG { get; set; }

        /// <summary>
        /// DIY Flag
        /// </summary>
        [StringLength(1)]
        public string DIY_FLAG { get; set; }

        /// <summary>
        /// Outlet Flag
        /// </summary>
        [StringLength(1)]
        public string OUTLET_FLAG { get; set; }

        /// <summary>
        /// ora user id
        /// </summary>
        [StringLength(20)]
        public string ORA_USER_ID { get; set; }

        /// <summary>
        /// salerqp id
        /// </summary>
        [StringLength(20)]
        public string REF_SALEREP_ID { get; set; }

        /// <summary>
        /// Inactive
        /// </summary>
        [StringLength(1)]
        public string INACTIVE { get; set; }

        /// <summary>
        /// Created by
        /// </summary>
        [StringLength(30)]
        public string CREATED_BY { get; set; }

        /// <summary>
        /// Date
        /// </summary>
        public DateTime? CREATED_DATE { get; set; }

        /// <summary>
        /// sex
        /// </summary>
        [StringLength(30)]
        public string UPDATED_BY { get; set; }

        /// <summary>
        /// sex
        /// </summary>
        public DateTime? UPDATED_DATE { get; set; }

        /// <summary>
        /// Sale Flag
        /// </summary>
        [StringLength(1)]
        public string SALE_FLAG { get; set; }

        /// <summary>
        /// CS Flag
        /// </summary>
        [StringLength(1)]
        public string CS_FLAG { get; set; }

        /// <summary>
        /// DC Flag
        /// </summary>
        [StringLength(1)]
        public string DC_FLAG { get; set; }

        /// <summary>
        /// Super Flag
        /// </summary>
        [StringLength(1)]
        public string SUP_FLAG { get; set; }

        /// <summary>
        /// Discount Flag
        /// </summary>
        [StringLength(1)]
        public string DISCOUNT_FLAG { get; set; }

        /// <summary>
        /// Mobile 
        /// </summary>
        [StringLength(20)]
        public string MOBILE_NO { get; set; }

        /// <summary>
        /// Admin Flag
        /// </summary>
        [StringLength(2)]
        public string ADMIN_FLAG { get; set; }

        /// <summary>
        /// Asst Sup Flag
        /// </summary>
        [StringLength(2)]
        public string ASST_SUP_FLAG { get; set; }

        /// <summary>
        /// Sup cashier flag
        /// </summary>
        [StringLength(2)]
        public string SUP_CASHIER_FLAG { get; set; }

        /// <summary>
        /// DIY Promo Flag
        /// </summary>
        [StringLength(1)]
        public string DIY_PROMO_FLAG { get; set; }

        /// <summary>
        /// Discount Type
        /// </summary>
        [StringLength(30)]
        public string DISCOUNT_TYPE { get; set; }

        /// <summary>
        /// Sale type
        /// </summary>
        [StringLength(20)]
        public string SALES_TYPE { get; set; }

        /// <summary>
        /// Downpayment Type
        /// </summary>
        [StringLength(30)]
        public string DOWNPAYMENT_TYPE { get; set; }

        /// <summary>
        /// sale group flag 
        /// </summary>
        [StringLength(1)]
        public string SALE_GROUP_FLAG { get; set; }

        /// <summary>
        /// bu code
        /// </summary>
        [StringLength(3)]
        public string BU_CODE { get; set; }

        //public virtual List<B3G_USER_DESC> B3G_USER_DESCs { get; set; }
    }

    /// <summary>
    /// Table B3G_USER_DESC  
    /// </summary>
    [Table("B3G_USER_DESC")]
    public class B3G_USER_DESC
    {
        /// <summary>
        /// id of user desc
        /// </summary>
        [Key]
        public int USER_DESC_ID { get; set; }

        /// <summary>
        /// Language
        /// </summary>
        [StringLength(5)]
        public string LANG_CODE { get; set; }

        /// <summary>
        /// id of user
        /// </summary>
        public int USER_ID { get; set; }

        /// <summary>
        /// prefix
        /// </summary>
        [StringLength(20)]
        public string PREFIX { get; set; }

        /// <summary>
        /// first name
        /// </summary>
        [StringLength(50)]
        public string FIRST_NAME { get; set; }

        /// <summary>
        /// last name
        /// </summary>
        [StringLength(50)]
        public string LAST_NAME { get; set; }
    }

    public class UserDetail
    {
        /// <summary>
        /// id of user
        /// </summary>
        public int userid { get; set; }
        /// <summary>
        /// User Name
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// First name
        /// </summary>
        public string firstname { get; set; }
        /// <summary>
        /// Last name
        /// </summary>
        public string lastname { get; set; }
    }

    public class UserAuthor
    {
        /// <summary>
        /// id of user
        /// </summary>
        [Key]
        public int USER_ID { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        [StringLength(30)]
        public string USER_NAME { get; set; }
        
        /// <summary>
        /// user type code
        /// </summary>
        [StringLength(3)]
        public string USER_TYPE_CODE { get; set; }

        /// <summary>
        /// sex
        /// </summary>
        [StringLength(1)]
        public string SEX { get; set; }

        public virtual List<B3G_USER_DESC> B3G_USER_DESCs { get; set; }
    }

}