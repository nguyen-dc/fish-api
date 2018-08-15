using AutoMapper;
using FLS.ServerSide.Business.Interfaces;
using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.EFCore.Services;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Biz
{
    public class FarmingSeasonBusiness : IFarmingSeasonBusiness
    {
        private readonly IFarmingSeasonService svcFarmingSeason;
        private readonly IMapper iMapper;
        public FarmingSeasonBusiness(IFarmingSeasonService _svcFarmingSeason, IMapper _iMapper)
        {
            svcFarmingSeason = _svcFarmingSeason;
            iMapper = _iMapper;
        }
        public async Task<PagedList<FarmingSeasonModel>> GetList(PageFilterModel _model)
        {
            return iMapper.Map<PagedList<FarmingSeasonModel>>(await svcFarmingSeason.GetList(_model));
        }
        public async Task<FarmingSeasonModel> GetDetail(int _id)
        {
            return iMapper.Map<FarmingSeasonModel>(await svcFarmingSeason.GetDetail(_id));
        }
        public async Task<int> Add(FarmingSeasonModel _model)
        {
            FarmingSeason entity = iMapper.Map<FarmingSeason>(_model);
            return await svcFarmingSeason.Add(entity);
        }
        public async Task<bool> Modify(int _id, FarmingSeasonModel _model)
        {
            FarmingSeason entity = await svcFarmingSeason.GetDetail(_id);
            if (entity == null) return false;
            entity = iMapper.Map(_model, entity);
            return await svcFarmingSeason.Modify(entity);
        }
        public async Task<bool> Remove(int _id)
        {
            return await svcFarmingSeason.Remove(_id);
        }
    }
}
