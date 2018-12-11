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
    
    public partial class Channel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Channel()
        {
            this.LT_Channel_Service = new HashSet<LT_Channel_Service>();
            this.LT_PaymentMethod_Channel = new HashSet<LT_PaymentMethod_Channel>();
            this.Terminal = new HashSet<Terminal>();
        }
    
        public long systemId { get; set; }
        public int status { get; set; }
        public System.DateTime creationDate { get; set; }
        public Nullable<System.DateTime> modificationDate { get; set; }
        public long createdBy { get; set; }
        public Nullable<long> modifiedBy { get; set; }
        public int version { get; set; }
        public string channelId { get; set; }
        public string channelName { get; set; }
        public string channelStatus { get; set; }
        public string publicKey { get; set; }
        public Nullable<long> deletedBy { get; set; }
        public Nullable<System.DateTime> deletedDate { get; set; }
        public Nullable<long> channelTypeId { get; set; }
        public Nullable<int> entityStatus_systemId { get; set; }
    
        public virtual ChannelType ChannelType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LT_Channel_Service> LT_Channel_Service { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LT_PaymentMethod_Channel> LT_PaymentMethod_Channel { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Terminal> Terminal { get; set; }
    }
}