﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WebApiHiringItm.MODEL.Entities
{
    public partial class ProjectFolder
    {
        public ProjectFolder()
        {
            Componente = new HashSet<Componente>();
            Contractor = new HashSet<Contractor>();
            ContractorPayments = new HashSet<ContractorPayments>();
            DetailProjectContractor = new HashSet<DetailProjectContractor>();
            DetalleContrato = new HashSet<DetalleContrato>();
            EconomicdataContractor = new HashSet<EconomicdataContractor>();
            FolderContractor = new HashSet<FolderContractor>();
        }

        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string CompanyName { get; set; }
        public string ProjectName { get; set; }
        public string DescriptionProject { get; set; }
        public bool Execution { get; set; }
        public bool Activate { get; set; }
        public bool EnableProject { get; set; }
        public int? ContractorsCant { get; set; }
        public decimal? ValorContrato { get; set; }
        public decimal? GastosOperativos { get; set; }
        public decimal? ValorSubTotal { get; set; }
        public string NumberProject { get; set; }

        public virtual UserT User { get; set; }
        public virtual ICollection<Componente> Componente { get; set; }
        public virtual ICollection<Contractor> Contractor { get; set; }
        public virtual ICollection<ContractorPayments> ContractorPayments { get; set; }
        public virtual ICollection<DetailProjectContractor> DetailProjectContractor { get; set; }
        public virtual ICollection<DetalleContrato> DetalleContrato { get; set; }
        public virtual ICollection<EconomicdataContractor> EconomicdataContractor { get; set; }
        public virtual ICollection<FolderContractor> FolderContractor { get; set; }
    }
}