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
            DetalleContrato = new HashSet<DetalleContrato>();
            Planning = new HashSet<Planning>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string CompanyName { get; set; }
        public string ProjectName { get; set; }
        public string DescriptionProject { get; set; }
        public bool? Execution { get; set; }
        public bool? Activate { get; set; }
        public int? ContractorsCant { get; set; }

        public virtual UserT User { get; set; }
        public virtual ICollection<Componente> Componente { get; set; }
        public virtual ICollection<Contractor> Contractor { get; set; }
        public virtual ICollection<DetalleContrato> DetalleContrato { get; set; }
        public virtual ICollection<Planning> Planning { get; set; }
    }
}