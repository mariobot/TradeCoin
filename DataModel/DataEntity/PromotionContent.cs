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
    
    public partial class PromotionContent
    {
        public int Id { get; set; }
        public Nullable<long> MainContentId { get; set; }
        public string MainContentName { get; set; }
        public Nullable<int> MainCateId { get; set; }
        public string MainCatetName { get; set; }
        public Nullable<int> ObjType { get; set; }
        public string ObjTypeName { get; set; }
        public Nullable<int> PromotionType { get; set; }
        public string PromotionTypeName { get; set; }
        public Nullable<System.DateTime> CrtdDT { get; set; }
        public Nullable<long> CrtdUID { get; set; }
        public string CrtdName { get; set; }
        public Nullable<System.DateTime> StartDT { get; set; }
        public Nullable<System.DateTime> EndDT { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<long> SubContentId { get; set; }
        public string SubContentName { get; set; }
        public Nullable<int> PromotionPercent { get; set; }
        public Nullable<int> PromotionAmount { get; set; }
    }
}
