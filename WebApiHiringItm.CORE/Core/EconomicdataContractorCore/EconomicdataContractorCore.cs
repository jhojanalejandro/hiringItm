﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiHiringItm.CONTEXT.Context;
using WebApiHiringItm.CORE.Core.EconomicdataContractorCore.Interface;
using WebApiHiringItm.MODEL.Dto;
using WebApiHiringItm.MODEL.Entities;

namespace WebApiHiringItm.CORE.Core.EconomicdataContractorCore
{
    public class EconomicdataContractorCore : IEconomicdataContractorCore
    {
        private readonly Hiring_V1Context _context;
        private readonly IMapper _mapper;


        public EconomicdataContractorCore(Hiring_V1Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<List<EconomicdataContractorDto>> GetAll()
        {
            var result = _context.EconomicdataContractor.Where(x => x.Id > 0).ToList();
            var map = _mapper.Map<List<EconomicdataContractorDto>>(result);
            return await Task.FromResult(map);
        }

        public async Task<List<EconomicdataContractorDto>> GetById(int[] id)
        {
            List<EconomicdataContractorDto> economicDataContractorList = new List<EconomicdataContractorDto>();
            foreach (var item in id)
            {
                var result = _context.EconomicdataContractor.Where(x => x.ContractorId == item).FirstOrDefault();
                if (result != null)
                {
                    var map = _mapper.Map<EconomicdataContractorDto>(result);
                    economicDataContractorList.Add(map);
                }

            }
            return await Task.FromResult(economicDataContractorList);
        }

        public async Task<bool> Delete(int id)
        {
            var getData = _context.EconomicdataContractor.Where(x => x.Id == id).FirstOrDefault();
            if (getData != null)
            {

                var result = _context.EconomicdataContractor.Remove(getData);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Create(List<EconomicdataContractorDto> model)
        {
            List<EconomicdataContractor> economicDataListAdd = new List<EconomicdataContractor>();
            List<EconomicdataContractor> economicDataListUpdate = new List<EconomicdataContractor>();


            var map = _mapper.Map<List<EconomicdataContractor>>(model);

            try
            {
                for (var i = 0; i < map.Count; i++)
                {
                    var getData = _context.EconomicdataContractor.FirstOrDefault(x => x.ContractorId == map[i].ContractorId && x.Id == map[i].Id);
                    if (getData != null)
                    {
                        var mapData = _mapper.Map(model[i], getData);
                        economicDataListUpdate.Add(mapData);
                        map.Remove(map[i]);
                        i--;
                    }
                    else
                    {
                        economicDataListAdd.Add(map[i]);
                    }
                }
                if (economicDataListUpdate.Count > 0)
                    _context.EconomicdataContractor.UpdateRange(economicDataListUpdate);
                if (economicDataListAdd.Count > 0)
                    _context.EconomicdataContractor.AddRange(economicDataListAdd);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);

            }
            return false;
        }

    }
}
