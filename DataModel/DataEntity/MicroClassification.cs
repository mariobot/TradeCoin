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
    
    public partial class MicroClassification
    {
        public long MicroClassifiId { get; set; }
        public Nullable<int> MicroParentClassifiId { get; set; }
        public string MicroParentName { get; set; }
        public string MicroClassifiNM { get; set; }
        public string MicroClassifiDesc { get; set; }
        public string MicroClassifiMetakeyword { get; set; }
        public string MicroClassifiMetaDes { get; set; }
        public int MicroClassifiSchemeId { get; set; }
        public string MicroClassifiCD { get; set; }
        public string FriendlyURL { get; set; }
        public byte IsEnabled { get; set; }
        public Nullable<System.DateTime> CrtdDT { get; set; }
        public Nullable<long> CrtdUID { get; set; }
        public string CrtdUserName { get; set; }
        public string ShortNM { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<long> MicroID { get; set; }
        public string MicroName { get; set; }
    }
}
