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
    
    public partial class TicketMsg
    {
        public long systemId { get; set; }
        public System.DateTime msgTime { get; set; }
        public long fromUser { get; set; }
        public long toUser { get; set; }
        public string method { get; set; }
        public string subject { get; set; }
        public string Body { get; set; }
    
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
