﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WebApiHiringItm.MODEL.Entities
{
    public partial class Planning
    {
        public Planning()
        {
            Component = new HashSet<Component>();
        }

        public int Id { get; set; }
        public string Consecutivo { get; set; }
        public int? ProjectFolderId { get; set; }
        public string Profesional { get; set; }
        public string Laboral { get; set; }
        public decimal? ValorTotal { get; set; }
        public string Objeto { get; set; }
        public int? DayCant { get; set; }
        public int? ContractorCant { get; set; }

        public virtual ProjectFolder ProjectFolder { get; set; }
        public virtual ICollection<Component> Component { get; set; }
    }
}