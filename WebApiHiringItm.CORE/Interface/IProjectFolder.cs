﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiHiringItm.MODEL.Dto;

namespace WebApiHiringItm.CORE.Interface
{
    public interface IProjectFolder
    {
        Task<List<ProjectFolderDto>> GetAll();
        Task<ProjectFolderDto> GetById(int id);
        Task<bool> Delete(int id);
        Task<bool> Create(ProjectFolderDto model);

    }
}
