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
    
    public partial class CurrentMeasurementList
    {
        public long systemId { get; set; }
        public Nullable<long> SLAId { get; set; }
        public Nullable<long> MeasurementId { get; set; }
        public Nullable<long> List { get; set; }
        public Nullable<long> TicletId { get; set; }
        public Nullable<double> list_value { get; set; }
        public Nullable<System.DateTime> run_date { get; set; }
    
        public virtual Measurment Measurment { get; set; }
        public virtual MeasurmentList MeasurmentList { get; set; }
        public virtual SLA SLA { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
