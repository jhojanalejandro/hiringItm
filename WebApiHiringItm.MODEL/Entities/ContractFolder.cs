﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WebApiHiringItm.MODEL.Entities
{
    public partial class ContractFolder
    {
        public ContractFolder()
        {
            AssigmentContract = new HashSet<AssigmentContract>();
            Component = new HashSet<Component>();
            DetailContract = new HashSet<DetailContract>();
            DetailContractor = new HashSet<DetailContractor>();
            Files = new HashSet<Files>();
            Folder = new HashSet<Folder>();
            NewnessContractor = new HashSet<NewnessContractor>();
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string CompanyName { get; set; }
        public string ProjectName { get; set; }
        public string ObjectContract { get; set; }
        public bool Activate { get; set; }
        public bool EnableProject { get; set; }
        public int? ContractorsCant { get; set; }
        public decimal? ValorContrato { get; set; }
        public decimal? GastosOperativos { get; set; }
        public decimal? ValorSubTotal { get; set; }
        public string NumberProject { get; set; }
        public string Project { get; set; }
        public Guid? Rubro { get; set; }
        public Guid? StatusContractId { get; set; }
        public string FuenteRubro { get; set; }

        public virtual RubroType RubroNavigation { get; set; }
        public virtual StatusContract StatusContract { get; set; }
        public virtual UserT User { get; set; }
        public virtual ICollection<AssigmentContract> AssigmentContract { get; set; }
        public virtual ICollection<Component> Component { get; set; }
        public virtual ICollection<DetailContract> DetailContract { get; set; }
        public virtual ICollection<DetailContractor> DetailContractor { get; set; }
        public virtual ICollection<Files> Files { get; set; }
        public virtual ICollection<Folder> Folder { get; set; }
        public virtual ICollection<NewnessContractor> NewnessContractor { get; set; }
    }
}