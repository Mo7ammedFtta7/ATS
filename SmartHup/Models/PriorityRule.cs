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
    
    public partial class PriorityRule
    {
        public long systemId { get; set; }
        public string PriorityRuleName { get; set; }
        public Nullable<long> Section_ID { get; set; }
        public Nullable<long> UserGroup_ID { get; set; }
        public Nullable<long> Priority_ID { get; set; }
        public long SLA_ID { get; set; }
        public Nullable<long> UserId { get; set; }
        public System.DateTime creationDate { get; set; }
        public Nullable<System.DateTime> modificationDate { get; set; }
        public long createdBy { get; set; }
        public Nullable<long> modifiedBy { get; set; }
        public int version { get; set; }
        public Nullable<int> entityStatus_systemId { get; set; }
        public bool UsingPriorityMatrix { get; set; }
        public Nullable<long> PriorityMatrix { get; set; }
        public Nullable<System.DateTime> deletedDate { get; set; }
        public Nullable<long> deletedBy { get; set; }
    
        public virtual EntityStatu EntityStatu { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual PriorityMatrix PriorityMatrix1 { get; set; }
        public virtual Section Section { get; set; }
        public virtual SLA SLA { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
