﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WebApiHiringItm.MODEL.Entities
{
    public partial class ElementosComponente
    {
        public int Id { get; set; }
        public string NombreElemento { get; set; }
        public string TipoElemento { get; set; }
        public int IdComponente { get; set; }
        public int CantidadContratistas { get; set; }
        public int CantidadDias { get; set; }
        public decimal ValorUnidad { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorPorDia { get; set; }
        public string Cpc { get; set; }
        public string NombreCpc { get; set; }
<<<<<<< HEAD
=======
        public bool? Modificacion { get; set; }
>>>>>>> 42cf66e55a8ab3e547588d7fd61cd983c9876af5
        public decimal? Recursos { get; set; }
        public string Consecutivo { get; set; }
        public bool? Modificacion { get; set; }

        public virtual Componente IdComponenteNavigation { get; set; }
    }
}