﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WebApiHiringItm.MODEL.Entities
{
    public partial class Activity
    {
        public Activity()
        {
            DetailContractor = new HashSet<DetailContractor>();
            ElementComponent = new HashSet<ElementComponent>();
        }

        public Guid Id { get; set; }
        public string NombreActividad { get; set; }
        public Guid ComponentId { get; set; }

        public virtual Component Component { get; set; }
        public virtual ICollection<DetailContractor> DetailContractor { get; set; }
        public virtual ICollection<ElementComponent> ElementComponent { get; set; }
    }
}