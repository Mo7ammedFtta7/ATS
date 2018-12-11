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
    
    public partial class Role
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Role()
        {
            this.ALERT = new HashSet<ALERT>();
            this.MONITORINGLOG = new HashSet<MONITORINGLOG>();
            this.RoleActions = new HashSet<RoleActions>();
            this.User = new HashSet<User>();
            this.UserRoles = new HashSet<UserRoles>();
        }
    
        public long systemId { get; set; }
        public string Name { get; set; }
        public string EName { get; set; }
        public long serviceProviderTypeId { get; set; }
        public long createdBy { get; set; }
        public Nullable<long> modifiedBy { get; set; }
        public Nullable<long> deletedBy { get; set; }
        public System.DateTime creationDate { get; set; }
        public Nullable<System.DateTime> modificationDate { get; set; }
        public Nullable<System.DateTime> deletedDate { get; set; }
        public Nullable<int> status { get; set; }
        public int version { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ALERT> ALERT { get; set; }
        public virtual EntityStatus EntityStatus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MONITORINGLOG> MONITORINGLOG { get; set; }
        public virtual ServiceProviderType ServiceProviderType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoleActions> RoleActions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
