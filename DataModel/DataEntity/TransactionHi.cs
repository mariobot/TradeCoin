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
    
    public partial class TransactionHi
    {
        public long TransactionID { get; set; }
        public long UserID { get; set; }
        public string UserName { get; set; }
        public System.DateTime CrtdDT { get; set; }
        public double MoneyNbrBefore { get; set; }
        public double MoneyNbrTran { get; set; }
        public double MoneyNbrAffter { get; set; }
        public string Note { get; set; }
        public Nullable<long> UserTransaction { get; set; }
        public string UserTransactionName { get; set; }
    
        public virtual User User { get; set; }
    }
}
