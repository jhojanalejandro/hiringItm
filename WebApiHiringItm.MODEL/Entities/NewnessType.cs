﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WebApiHiringItm.MODEL.Entities
{
    public partial class NewnessType
    {
        public NewnessType()
        {
            NewnessContractor = new HashSet<NewnessContractor>();
        }

        public Guid Id { get; set; }
        public string NewnessType1 { get; set; }
        public string NewnessDescription { get; set; }
        public string Code { get; set; }

        public virtual ICollection<NewnessContractor> NewnessContractor { get; set; }
    }
}