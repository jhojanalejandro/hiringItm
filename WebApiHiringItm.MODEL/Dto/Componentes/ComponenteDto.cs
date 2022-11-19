﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiHiringItm.MODEL.Entities;

namespace WebApiHiringItm.MODEL.Dto.Componentes
{
    public class ComponenteDto
    {
        public int Id { get; set; }
        public string NombreComponente { get; set; } = null!;
        public int IdContrato { get; set; }
    }
}
