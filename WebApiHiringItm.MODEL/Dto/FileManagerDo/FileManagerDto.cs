﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiHiringItm.MODEL.Dto.FileDto;
using WebApiHiringItm.MODEL.Entities;

namespace WebApiHiringItm.MODEL.Dto.FileManagerDo
{
    public class FileManagerDto
    {
        public string[] Path { get; set; }
        public List<FolderContractDto>? FolderContract { get; set; }
        public List<FolderContractorDto>? Folders { get; set; }

    }

    public class FolderContractorDto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Identificacion { get; set; }
        public string Type { get; set; }
    }

    public class FolderContractDto
    {
        public Guid? Id { get; set; }
        public string FolderName { get; set; }
        public string NumberProject { get; set; }
        public string TypeFolder { get; set; }
    }
}
