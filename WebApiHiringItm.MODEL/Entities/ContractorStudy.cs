﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WebApiHiringItm.MODEL.Entities
{
    public partial class ContractorStudy
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdContractor { get; set; }
        public string TypeStudy { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinalDate { get; set; }
        public string DescriptionStudy { get; set; }
        public string InstitutionName { get; set; }

        public virtual Contractor IdContractorNavigation { get; set; }
        public virtual UserT IdUserNavigation { get; set; }
    }
}