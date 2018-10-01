using AutoMapper;
using FLS.ServerSide.Business.Interfaces;
using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.EFCore.Services;
using FLS.ServerSide.Model.Scope;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Biz
{
    public class FarmRegionBusiness : IFarmRegionBusiness
    {
        private IScopeContext context;
        private readonly IFarmRegionService svcFarmRegion;
        private readonly IMapper iMapper;
        public FarmRegionBusiness(IFarmRegionService _svcFarmRegion, IMapper _iMapper)
        {
            svcFarmRegion = _svcFarmRegion;
            iMapper = _iMapper;
        }
        public async Task<PagedList<FarmRegionModel>> GetList(PageFilterModel _model)
        {
            return iMapper.Map<PagedList<FarmRegionModel>>(await svcFarmRegion.GetList(_model));
        }
        public async Task<FarmRegionModel> GetDetail(int _id)
        {
            return iMapper.Map<FarmRegionModel>(await svcFarmRegion.GetDetail(_id));
        }
        public async Task<int> Add(FarmRegionModel _model)
        {
            FarmRegion entity = iMapper.Map<FarmRegion>(_model);
            return await svcFarmRegion.Add(entity);
        }
        public async Task<bool> Modify(int _id, FarmRegionModel _model)
        {
            FarmRegion entity = await svcFarmRegion.GetDetail(_id);
            if (entity == null) return false;
            entity = iMapper.Map(_model, entity);
            return await svcFarmRegion.Modify(entity);
        }
        public async Task<bool> Remove(int _id)
        {
            return await svcFarmRegion.Remove(_id);
        }
        public async Task<List<IdNameModel>> GetCache()
        {
            return iMapper.Map<List<IdNameModel>>(await svcFarmRegion.GetCache());
        }
    }
}
