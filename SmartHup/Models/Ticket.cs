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
    
    public partial class Ticket
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ticket()
        {
            this.ChatSessions = new HashSet<ChatSession>();
            this.CurrentMeasurementLists = new HashSet<CurrentMeasurementList>();
            this.Tasks = new HashSet<Task>();
            this.TicketLogs = new HashSet<TicketLog>();
        }
    
        public long systemId { get; set; }
        public string TicketTitle { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public Nullable<long> SubmitterUser { get; set; }
        public Nullable<long> CategoryID { get; set; }
        public Nullable<long> SubCategoryID { get; set; }
        public Nullable<long> StatusID { get; set; }
        public Nullable<long> Impact_ID { get; set; }
        public Nullable<long> Urgancy_ID { get; set; }
        public Nullable<long> Priority_ID { get; set; }
        public Nullable<long> AssignedUser { get; set; }
        public Nullable<long> AssignedGroup { get; set; }
        public byte[] Attachment { get; set; }
        public string Solution { get; set; }
        public string Resolution { get; set; }
        public Nullable<System.DateTime> StratDate { get; set; }
        public Nullable<System.DateTime> CloseDate { get; set; }
        public Nullable<int> version { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<long> Timer { get; set; }
        public Nullable<long> SLAId { get; set; }
        public Nullable<System.DateTime> creationDate { get; set; }
        public Nullable<System.DateTime> modificationDate { get; set; }
        public Nullable<long> createdBy { get; set; }
        public Nullable<long> modifiedBy { get; set; }
        public Nullable<int> entityStatus_systemId { get; set; }
        public Nullable<System.DateTime> deletedDate { get; set; }
        public Nullable<long> deletedBy { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Category Category1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChatSession> ChatSessions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CurrentMeasurementList> CurrentMeasurementLists { get; set; }
        public virtual EntityStatu EntityStatu { get; set; }
        public virtual Impact Impact { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual SLA SLA { get; set; }
        public virtual Status Status { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Task> Tasks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TicketLog> TicketLogs { get; set; }
        public virtual Ticket Tickets1 { get; set; }
        public virtual Ticket Ticket1 { get; set; }
        public virtual Urgency Urgency { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
