﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WebApiHiringItm.MODEL.Entities
{
    public partial class EmptityHealth
    {
        public Guid Id { get; set; }
        public string TypeEmptity { get; set; }
        public string Emptity { get; set; }
        public Guid? Contractor { get; set; }

        public virtual Contractor ContractorNavigation { get; set; }
    }
}