﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiHiringItm.CORE.Core.Componentes.Interfaces;
using WebApiHiringItm.MODEL.Dto.Componentes;

namespace WebApiHiringItm.API.Controllers.Componente
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElementosComponenteController : ControllerBase
    {
        #region Fields
        private readonly IElementosComponenteCore _element;
        #endregion

        #region Builder
        public ElementosComponenteController(IElementosComponenteCore element)
        {
            _element = element;
        }
        #endregion

        #region Methods
        [HttpPost]
        public async Task<IActionResult> Add(List<ElementosComponenteDto> model)
        {
            try
            {
                var res = await _element.Add(model);
                return res != false ? Ok(res) : BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var res = await _element.Get(id);
                return res.Count == 0 ? BadRequest() : Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion
    }
}
