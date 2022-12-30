﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiHiringItm.MODEL.Dto;
using WebApiHiringItm.MODEL.Dto.Contratista;

namespace WebApiHiringItm.CORE.Core.HiringDataCore.Interface
{
    public interface IHiringDataCore
    {
        Task<List<HiringDataDto>> GetAll();
        Task<HiringDataDto> GetById(int id);
        Task<bool> Delete(int id);
        Task<bool> Create(List<HiringDataDto> model);
        Task<MinutaDto> GetByIdMinuta(int[] id);

    }
}
