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
    
    public partial class PriorityMatrix
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PriorityMatrix()
        {
            this.PriorityRules = new HashSet<PriorityRule>();
        }
    
        public long systemId { get; set; }
        public long Impact_ID { get; set; }
        public long Urgancy_ID { get; set; }
        public long Priority_ID { get; set; }
        public System.DateTime creationDate { get; set; }
        public Nullable<System.DateTime> modificationDate { get; set; }
        public long createdBy { get; set; }
        public Nullable<long> modifiedBy { get; set; }
        public Nullable<int> version { get; set; }
        public Nullable<int> entityStatus_systemId { get; set; }
        public Nullable<long> SLAId { get; set; }
        public bool Enabled { get; set; }
        public Nullable<System.DateTime> deletedDate { get; set; }
        public Nullable<long> deletedBy { get; set; }
    
        public virtual EntityStatu EntityStatu { get; set; }
        public virtual Impact Impact { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual SLA SLA { get; set; }
        public virtual Urgency Urgency { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PriorityRule> PriorityRules { get; set; }
    }
}
