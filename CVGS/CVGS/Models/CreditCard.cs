//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CVGS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CreditCard
    {
        public int creditCardID { get; set; }
        public int userID { get; set; }
        public string owner { get; set; }
        public string cardNumber { get; set; }
        public int code { get; set; }
        public System.DateTime expiredDate { get; set; }
        public int isDefault { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public Nullable<System.DateTime> updatedDate { get; set; }
    
        public virtual User User { get; set; }
    }
}
