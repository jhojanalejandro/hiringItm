﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WebApiHiringItm.MODEL.Entities
{
    public partial class Actividad
    {
        public int Id { get; set; }
        public string NombreActividad { get; set; }
        public Guid IdComponente { get; set; }

        public virtual Componente IdComponenteNavigation { get; set; }
    }
}