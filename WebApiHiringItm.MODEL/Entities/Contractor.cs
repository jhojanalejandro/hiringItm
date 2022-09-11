﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WebApiHiringItm.MODEL.Entities
{
    public partial class Contractor
    {
        public Contractor()
        {
            RecursiveContractor = new HashSet<RecursiveContractor>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Documentodeidentificación { get; set; }
        public string Lugardeexpedición { get; set; }
        public DateTime? Fechanacimiento { get; set; }
        public string Municipio { get; set; }
        public string Comuna { get; set; }
        public string Barrio { get; set; }
        public string Teléfono { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public string Sexo { get; set; }
        public string Nacionalidad { get; set; }
        public string Direccion { get; set; }
        public string Departamento { get; set; }
        public string Eps { get; set; }
        public string Pension { get; set; }
        public string Arl { get; set; }
        public string Cuentabancaria { get; set; }
        public string Tipodecuenta { get; set; }
        public string Entidadcuentabancaria { get; set; }

        public virtual ICollection<RecursiveContractor> RecursiveContractor { get; set; }
    }
}