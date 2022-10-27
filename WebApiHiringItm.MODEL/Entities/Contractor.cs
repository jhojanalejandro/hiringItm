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
            ContractorPayments = new HashSet<ContractorPayments>();
            FolderContractor = new HashSet<FolderContractor>();
            HiringData = new HashSet<HiringData>();
        }

        public int Id { get; set; }
        public string No { get; set; }
        public string Convenio { get; set; }
        public string Entidad { get; set; }
        public string ObjetoConvenio { get; set; }
        public DateTime? FechaInicioConvenio { get; set; }
        public DateTime? FechaFinalizacionConvenio { get; set; }
        public string Componente { get; set; }
        public string TalentoHumano { get; set; }
        public string NombreCompleto { get; set; }
        public string DocumentoDeIdentificacion { get; set; }
        public string LugarDeExpedicion { get; set; }
        public string Bachiller { get; set; }
        public string Tecnico { get; set; }
        public string Tecnologo { get; set; }
        public string Pregrado { get; set; }
        public string Especializacion { get; set; }
        public string Maestria { get; set; }
        public string Doctorado { get; set; }
        public string Sexo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Nacionalidad { get; set; }
        public string Direccion { get; set; }
        public string Departamento { get; set; }
        public string Municipio { get; set; }
        public string Comuna { get; set; }
        public string Barrio { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public string Eps { get; set; }
        public string Pension { get; set; }
        public string Arl { get; set; }
        public string CuentaBancaria { get; set; }
        public string TipoDeCuenta { get; set; }
        public string EntidadCuentaBancaria { get; set; }
        public int UserId { get; set; }
        public int ContractId { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string ClaveUsuario { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        public virtual ProjectFolder Contract { get; set; }
        public virtual UserT User { get; set; }
        public virtual ICollection<ContractorPayments> ContractorPayments { get; set; }
        public virtual ICollection<FolderContractor> FolderContractor { get; set; }
        public virtual ICollection<HiringData> HiringData { get; set; }
    }
}