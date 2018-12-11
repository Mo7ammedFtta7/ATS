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
    
    public partial class ServiceField
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServiceField()
        {
            this.MultiValueList = new HashSet<MultiValueList>();
            this.ServiceField1 = new HashSet<ServiceField>();
        }
    
        public long systemId { get; set; }
        public int status { get; set; }
        public System.DateTime creationDate { get; set; }
        public Nullable<System.DateTime> modificationDate { get; set; }
        public long createdBy { get; set; }
        public Nullable<long> modifiedBy { get; set; }
        public int version { get; set; }
        public string serviceFieldId { get; set; }
        public string serviceFieldName { get; set; }
        public string serviceFieldStatus { get; set; }
        public string channelFieldEName { get; set; }
        public string channelFieldName { get; set; }
        public string serviceHandlerFieldName { get; set; }
        public string regularExpression { get; set; }
        public Nullable<bool> isRequired { get; set; }
        public string dateFormat { get; set; }
        public Nullable<bool> isRequestField { get; set; }
        public Nullable<bool> isSavable { get; set; }
        public Nullable<bool> isResponseField { get; set; }
        public Nullable<long> additionalFieldName { get; set; }
        public Nullable<long> serviceId { get; set; }
        public Nullable<long> serviceHanlerId { get; set; }
        public Nullable<bool> isPaymentInfo { get; set; }
        public Nullable<int> paymentInfoOrder { get; set; }
        public Nullable<System.DateTime> Note { get; set; }
        public Nullable<decimal> payeeId { get; set; }
        public Nullable<int> idx { get; set; }
        public Nullable<decimal> serviceProviderId { get; set; }
        public Nullable<bool> isParent { get; set; }
        public Nullable<long> parentId { get; set; }
        public Nullable<int> entityStatus_systemId { get; set; }
        public Nullable<long> deletedBy { get; set; }
        public Nullable<System.DateTime> deletedDate { get; set; }
        public Nullable<long> regTypeId { get; set; }
        public Nullable<bool> isFavorable { get; set; }
        public string dbColumnName { get; set; }
        public Nullable<bool> isCheckable { get; set; }
        public Nullable<bool> isPrecheckField { get; set; }
    
        public virtual EntityStatus EntityStatus { get; set; }
        public virtual EntityStatus EntityStatus1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MultiValueList> MultiValueList { get; set; }
        public virtual RangType RangType { get; set; }
        public virtual Service Service { get; set; }
        public virtual ServiceHandler ServiceHandler { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceField> ServiceField1 { get; set; }
        public virtual ServiceField ServiceField2 { get; set; }
    }
}