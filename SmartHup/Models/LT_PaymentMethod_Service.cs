//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartHup.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LT_PaymentMethod_Service
    {
        public long systemid { get; set; }
        public long paymentMethodId { get; set; }
        public long serviceId { get; set; }
    
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual Service Service { get; set; }
    }
}
