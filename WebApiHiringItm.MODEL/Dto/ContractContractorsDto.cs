﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiHiringItm.MODEL.Dto
{
    public class ContractContractorsDto
    {
        public Guid contractId { get; set; }    
        public string[] contractors { get; set; }
    }
}
