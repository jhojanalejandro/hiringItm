﻿using AutoMapper;
using System.Data.Entity;
using WebApiHiringItm.CONTEXT.Context;
using WebApiHiringItm.CORE.Core.HiringDataCore.Interface;
using WebApiHiringItm.MODEL.Dto;
using WebApiHiringItm.MODEL.Dto.Componentes;
using WebApiHiringItm.MODEL.Dto.Contratista;
using WebApiHiringItm.MODEL.Entities;

namespace WebApiHiringItm.CORE.Core.HiringDataCore
{
    public class HiringDataCore : IHiringDataCore
    {
        #region FIELDS
        private readonly Hiring_V1Context _context;
        private readonly IMapper _mapper;
        IQueryable<DetailProjectContractor> hiringResult;
        #endregion

        public HiringDataCore(Hiring_V1Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region PUBLIC METHODS
        public async Task<List<HiringDataDto>> GetAll()
        {
            var result = _context.HiringData.Where(x => x.Id != null).ToList();
            var map = _mapper.Map<List<HiringDataDto>>(result);
            return await Task.FromResult(map);
        }

        public async Task<HiringDataDto> GetById(Guid contractorId, Guid contractId)
        {
           var hiringResult = _context.DetailProjectContractor
                .Where(x => x.ContractorId == contractorId && x.ContractId == x.ContractId)
                .Include(x => x.HiringData)
                .Where(w => w.HiringData != null);
            if (hiringResult != null)
            {
               var hd = hiringResult.Select(hd => new HiringDataDto()
                {
                    Id = hd.HiringData.Id,
                    FechaRealDeInicio = hd.HiringData.FechaRealDeInicio,
                    FechaFinalizacionConvenio = hd.HiringData.FechaFinalizacionConvenio,
                    Contrato = hd.HiringData.Contrato,
                    Compromiso = hd.HiringData.Compromiso,
                    FechaExaPreocupacional = hd.HiringData.FechaExaPreocupacional,
                    SupervisorItm = hd.HiringData.SupervisorItm,
                    CargoSupervisorItm = hd.HiringData.CargoSupervisorItm,
                    FechaDeComite = hd.HiringData.FechaDeComite,
                    RequierePoliza = hd.HiringData.RequierePoliza,
                    NoPoliza = hd.HiringData.NoPoliza,
                    VigenciaInicial = hd.HiringData.VigenciaInicial,
                    VigenciaFinal = hd.HiringData.VigenciaFinal,
                    FechaExpedicionPoliza = hd.HiringData.FechaExpedicionPoliza,
                    ValorAsegurado = hd.HiringData.ValorAsegurado,
                    Nivel = hd.HiringData.Nivel,
                    Rubro = hd.HiringData.Rubro,
                    NombreRubro = hd.HiringData.NombreRubro,
                    FuenteRubro = hd.HiringData.FuenteRubro,
                    Cdp = hd.HiringData.Cdp,
                    NumeroActa = hd.HiringData.NumeroActa
                })
                .AsNoTracking()
                .FirstOrDefault();
                return hd;

            }
            return null;
        }

        public async Task<MinutaDto> GetByIdMinuta(Guid[] id)
        {
            var hiring = _context.DetailProjectContractor
                .Include(hd => hd.HiringData).FirstOrDefault(x => x.ContractorId == id[0]);
            var elemento = _context.ElementosComponente.FirstOrDefault(x => x.Id == id[1]);
            MinutaDto minutaDto = new MinutaDto();
            var hiringMap = _mapper.Map<HiringDataDto>(hiring);
            var elementoMap = _mapper.Map<ElementosComponenteDto>(elemento);

            minutaDto.HiringDataDto = hiringMap;
            minutaDto.elementosComponenteDto = elementoMap;
            var map = _mapper.Map<MinutaDto>(minutaDto);
            return await Task.FromResult(map);
        }

        public async Task<bool> Updates(string model)
        {
            try
            {
                if (model != null)

                {
                    var map = _mapper.Map<HiringDataDto>(model);
                    await _context.BulkInsertAsync(_context.HiringData, options => options.InsertKeepIdentity = true);
                    var res = _context.BulkSaveChangesAsync(bulk => bulk.BatchSize = 100);
                    if (res.IsCompleted)
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                new Exception("Error", e);
            }
            return false;
        }

        public async Task<bool> Delete(Guid id)
        {
            var getData = _context.HiringData.Where(x => x.Id == id).FirstOrDefault();
            if (getData != null)
            {

                var result = _context.HiringData.Remove(getData);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Create(List<HiringDataDto> model)
        {
            try
            {
                List<HiringData> hiringDataListUpdate = new List<HiringData>();
                List<HiringData> hiringDataListAdd = new List<HiringData>();
                List<DetailProjectContractor> detailDataListAdd = new List<DetailProjectContractor>();
                var getData = _context.DetailProjectContractor
                    .Where(x => x.ContractId == model[0].ContractId && x.ContractorId.Equals(model[0].ContractorId))
                    .Include(dt => dt.HiringData)
                    .FirstOrDefault();
                var hd = _context.HiringData.ToList();
                var map = _mapper.Map<List<HiringData>>(model);
                for (var i = 0; i < map.Count; i++)
                {
                    var hdata = hd.FirstOrDefault(x => x.Id == getData.HiringDataId);

                    if (hdata != null)
                    {
                        model[i].Id = hdata.Id;
                        var mapData = _mapper.Map(model[i], hdata);
                        hiringDataListUpdate.Add(mapData);
                        map.Remove(map[i]);
                        i--;
                    }
                    else
                    {
                        if (getData != null)
                        {
                            DetailProjectContractor detailProjectContractor = new DetailProjectContractor();
                            map[i].Id = Guid.NewGuid();
                            detailProjectContractor.HiringDataId = map[i].Id;
                            detailProjectContractor.ContractorId = map[i].ContractorId;
                            detailProjectContractor.ContractId = model[i].ContractId;
                            detailProjectContractor.ElementId = getData.ElementId;
                            detailProjectContractor.ComponenteId = getData.ComponenteId;
                            detailProjectContractor.Id = getData.Id;
                            detailDataListAdd.Add(detailProjectContractor);
                            hiringDataListAdd.Add(map[i]);
                        }
                        else
                        {
                            return false;
                        }

                    }
                }

                if (hiringDataListUpdate.Count > 0)
                    _context.HiringData.UpdateRange(hiringDataListUpdate);
                if (hiringDataListAdd.Count > 0)
                    _context.HiringData.AddRange(hiringDataListAdd);
                await _context.SaveChangesAsync();

                if (detailDataListAdd.Count > 0)
                    return await updateDetails(detailDataListAdd);

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("Error", ex);
            }
            return false;
        }
        #endregion
        #region PRIVATE METHODS
        private async Task<bool> updateDetails(List<DetailProjectContractor> detailProjectContractors)
        {
            try
            {
                _context.DetailProjectContractor.UpdateRange(detailProjectContractors);
                var result = await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);

            }

            return false;
        }
        #endregion
    }
}
