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
    
    public partial class Package
    {
        public long PackageId { get; set; }
        public string PackageName { get; set; }
        public Nullable<double> OldPrice { get; set; }
        public Nullable<double> NewPrice { get; set; }
        public Nullable<int> NumDay { get; set; }
        public string CrtdUserName { get; set; }
        public Nullable<long> CrtdUserId { get; set; }
        public Nullable<System.DateTime> CrtdDT { get; set; }
        public string StateName { get; set; }
        public Nullable<int> StateId { get; set; }
        public Nullable<double> ForeverPrice { get; set; }
        public Nullable<double> NewPrice3M { get; set; }
        public Nullable<double> NewPrice1Y { get; set; }
    }
}
