﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WebApiHiringItm.MODEL.Entities
{
    public partial class EconomicdataContractor
    {
        public int Id { get; set; }
        public Guid? ContractorId { get; set; }
        public decimal? TotalValue { get; set; }
        public decimal? UnitValue { get; set; }
        public decimal? TotalPaidMonth { get; set; }
        public bool? CashPayment { get; set; }
        public decimal? Missing { get; set; }
        public decimal? Debt { get; set; }
        public decimal? Freed { get; set; }
        public DateTime? RegisterDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public Guid? ContractId { get; set; }

        public virtual ProjectFolder Contract { get; set; }
        public virtual Contractor Contractor { get; set; }
    }
}