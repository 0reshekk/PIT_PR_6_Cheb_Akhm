//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PIT_PR_6_Cheb_Akhm
{
    using System;
    using System.Collections.Generic;
    
    public partial class Deposits
    {
        public int DepositID { get; set; }
        public Nullable<int> UserID { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
    
        public virtual Users Users { get; set; }
        public virtual Users Users1 { get; set; }
    }
}
