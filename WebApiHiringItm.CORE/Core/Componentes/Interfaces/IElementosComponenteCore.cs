﻿using WebApiHiringItm.MODEL.Dto.Componentes;

namespace WebApiHiringItm.CORE.Core.Componentes.Interfaces
{
    public interface IElementosComponenteCore
    {
        Task<bool> Add(List<ElementosComponenteDto> model);
        Task<List<ElementosComponenteDto>?> Get(int id);
        Task<List<ElementosComponenteDto>?> GetByContractId(int id);

    }
}
