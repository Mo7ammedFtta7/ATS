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
    
    public partial class ShareTopic
    {
        public long systemId { get; set; }
        public string ShareTopicName { get; set; }
        public string ShareTopicArName { get; set; }
        public string Method { get; set; }
        public Nullable<long> Topic_ID { get; set; }
        public Nullable<long> UserGroup_ID { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public byte[] Attachment { get; set; }
        public Nullable<System.DateTime> creationDate { get; set; }
        public Nullable<System.DateTime> modificationDate { get; set; }
        public Nullable<long> createdBy { get; set; }
        public Nullable<long> modifiedBy { get; set; }
        public Nullable<int> version { get; set; }
        public Nullable<int> Status { get; set; }
    
        public virtual UserGroup UserGroup { get; set; }
    }
}