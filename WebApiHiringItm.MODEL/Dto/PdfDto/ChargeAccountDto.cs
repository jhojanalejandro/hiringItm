﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiHiringItm.MODEL.Dto.PdfDto
{
    public class ChargeAccountDto
    {
        public string ContractorName { get; set; }
        public string ContractorIdentification { get; set; }
        public string ExpeditionIdentification { get; set; }
        public string Direction { get; set; }
        public string ContractNumber { get; set; }
        public string? PeriodExecutedInitialDate { get; set; }
        public string? PeriodExecutedFinalDate { get; set; }
        public string elementName { get; set; }
        public string PhoneNumber { get; set; }
        public string TypeAccount { get; set; }
        public string AccountNumber { get; set; }
        public string BankingEntity { get; set; }
        public string? TotalValue { get; set; }
        public string? ContractName { get; set; }
        public string? ChargeAccountNumber { get; set; }
    }
}
