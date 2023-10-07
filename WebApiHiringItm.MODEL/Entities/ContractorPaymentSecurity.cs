﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WebApiHiringItm.MODEL.Entities
{
    public partial class ContractorPaymentSecurity
    {
        public Guid Id { get; set; }
        public Guid ContractorPayments { get; set; }
        public string Observation { get; set; }
        public decimal PaymentPension { get; set; }
        public decimal PaymentArl { get; set; }
        public decimal PaymentEps { get; set; }
        public DateTime RegisterDate { get; set; }
        public int Consecutive { get; set; }
        public string PayrollNumber { get; set; }
        public DateTime PaymentPeriodDate { get; set; }
        public bool CorrectArlPayment { get; set; }
        public bool CorrectAfpPayment { get; set; }
        public bool CorrectEpsPayment { get; set; }
        public bool CorrectSheet { get; set; }
        public bool VerifySheet { get; set; }

        public virtual ContractorPayments ContractorPaymentsNavigation { get; set; }
    }
}