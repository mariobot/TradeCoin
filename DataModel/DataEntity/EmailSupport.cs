//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataModel.DataEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmailSupport
    {
        public long EmailId { get; set; }
        public string EmailName { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public Nullable<long> DestinationId { get; set; }
        public string DestinationName { get; set; }
        public Nullable<long> ParentId { get; set; }
        public string ParentName { get; set; }
        public string CrtdUserName { get; set; }
        public Nullable<long> CrtdUserId { get; set; }
        public Nullable<System.DateTime> CrtdDT { get; set; }
        public string AprvdUserName { get; set; }
        public Nullable<long> AprvdUID { get; set; }
        public Nullable<System.DateTime> AprvdDT { get; set; }
        public string StateName { get; set; }
        public Nullable<long> StateId { get; set; }
        public Nullable<long> EmailTypeId { get; set; }
        public string EmailTypeName { get; set; }
        public string StateName2 { get; set; }
        public Nullable<long> StateId2 { get; set; }
    }
}
