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
    
    public partial class Order
    {
        public long Id { get; set; }
        public string OrderCode { get; set; }
        public Nullable<long> CustomerId { get; set; }
        public Nullable<int> PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }
        public Nullable<int> ShippingMethodId { get; set; }
        public string ShippingMethodName { get; set; }
        public Nullable<int> StatusId { get; set; }
        public string StatusName { get; set; }
        public Nullable<double> Cost { get; set; }
        public string Note { get; set; }
        public Nullable<System.DateTime> PaIdDT { get; set; }
        public Nullable<System.DateTime> PlaceDT { get; set; }
        public Nullable<System.DateTime> DeliverDT { get; set; }
        public string Coupon { get; set; }
        public Nullable<long> EmloyeeId { get; set; }
        public string EmloyeeName { get; set; }
        public Nullable<long> ApprovedUId { get; set; }
        public string ApprovedName { get; set; }
        public Nullable<System.DateTime> LastApprovedDT { get; set; }
        public string NameCustomerBuy { get; set; }
        public string AddressCustomer { get; set; }
        public string PhoneCustomer { get; set; }
        public string EmailCustomer { get; set; }
        public string PostCode { get; set; }
        public Nullable<int> IdProvince { get; set; }
        public Nullable<int> IdDistrict { get; set; }
        public Nullable<int> IdWard { get; set; }
        public string NameProvince { get; set; }
        public string NameDistrict { get; set; }
        public string NameWard { get; set; }
        public Nullable<long> MicrositeId { get; set; }
        public string MicrositeName { get; set; }
        public Nullable<int> MicroStatus { get; set; }
        public string MicroStatusName { get; set; }
        public string MicroNote { get; set; }
        public Nullable<int> TranStatus { get; set; }
        public string TranStatusName { get; set; }
        public string TranNote { get; set; }
    }
}
