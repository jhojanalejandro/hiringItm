﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WebApiHiringItm.MODEL.Entities
{
    public partial class Componente
    {
        public Componente()
        {
            Actividad = new HashSet<Actividad>();
            ElementosComponente = new HashSet<ElementosComponente>();
            ProfessionalRol = new HashSet<ProfessionalRol>();
        }

        public int Id { get; set; }
        public string NombreComponente { get; set; }
        public int IdContrato { get; set; }

        public virtual ProjectFolder IdContratoNavigation { get; set; }
        public virtual ICollection<Actividad> Actividad { get; set; }
        public virtual ICollection<ElementosComponente> ElementosComponente { get; set; }
        public virtual ICollection<ProfessionalRol> ProfessionalRol { get; set; }
    }
}