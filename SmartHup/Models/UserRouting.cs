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
    
    public partial class UserRouting
    {
        public long systemId { get; set; }
        public Nullable<long> userfrom { get; set; }
        public Nullable<long> userTo { get; set; }
    
        public virtual UserRouting UserRouting1 { get; set; }
        public virtual UserRouting UserRouting2 { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}