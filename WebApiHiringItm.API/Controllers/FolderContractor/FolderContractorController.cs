﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiHiringItm.CORE.Core.FoldersContractorCore.Interface;
using WebApiHiringItm.MODEL.Dto.Contratista;

namespace WebApiHiringItm.API.Controllers.FolderContractor
{
    [ApiController]
    //[Authorize]
    [Route("[controller]/[action]")]
    public class FolderContractorController : ControllerBase
    {
        private readonly IFolderContractorCore _folder;

        public FolderContractorController(IFolderContractorCore folder)
        {
            _folder = folder;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(Guid contractorId, Guid contractId)
        {
            try
            {
                //Obtenemos todos los registros.
                var Data = await _folder.GetAllById(contractorId, contractId);

                //Retornamos datos.
                return Data != null ? Ok(Data) : NoContent();
            }
            catch (Exception ex)
            {

                throw new Exception("Error", ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                //Obtenemos todos los registros.
                var Data = await _folder.GetById(id);

                //Retornamos datos.
                return Data != null ? Ok(Data) : NoContent();
            }
            catch (Exception ex)
            {

                throw new Exception("Error", ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(FolderContractorDto model)
        {
            try
            {
                var Data = await _folder.Create(model);

                return Data == true ? Ok(Data) : NoContent();
            }
            catch (Exception ex)
            {

                throw new Exception("Error", ex);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Update(FolderContractorDto model)
        {
            try
            {
                var Data = await _folder.Create(model);
                return Data != null ? Ok(Data) : NoContent();
            }
            catch (Exception ex)
            {

                throw new Exception("Error", ex);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                //Obtenemos todos los registros.
                var Data = await _folder.Delete(id);

                //Retornamos datos.
                return Data != false ? Ok(Data) : NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }
    }
}
