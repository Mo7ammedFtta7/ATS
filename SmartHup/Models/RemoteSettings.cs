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
    
    public partial class RemoteSettings
    {
        public decimal SystemId { get; set; }
        public int Version { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> Note { get; set; }
        public string consumerURL { get; set; }
        public string balancePath { get; set; }
        public string paymentPath { get; set; }
        public string inquiryPath { get; set; }
        public string keyPath { get; set; }
        public string registerPath { get; set; }
        public string transferPath { get; set; }
        public string reversalPath { get; set; }
        public string changeIPinPath { get; set; }
        public string changePasswordPath { get; set; }
        public string applicationId { get; set; }
        public string specialPaymentPath { get; set; }
    }
}
