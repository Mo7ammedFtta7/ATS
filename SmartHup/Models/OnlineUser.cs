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
    
    public partial class OnlineUser
    {
        public long systemId { get; set; }
        public long userId { get; set; }
        public long sessionId { get; set; }
        public string ipAddress { get; set; }
    
        public virtual ChatSession ChatSession { get; set; }
        public virtual User User { get; set; }
    }
}
