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
    public class FishPondBusiness : IFishPondBusiness
    {
        private readonly IFishPondService svcFishPond;
        private readonly IMapper iMapper;
        public FishPondBusiness(IFishPondService _svcFishPond, IMapper _iMapper)
        {
            svcFishPond = _svcFishPond;
            iMapper = _iMapper;
        }
        public async Task<PagedList<FishPondModel>> GetList(PageFilterModel _model)
        {
            return iMapper.Map<PagedList<FishPondModel>>(await svcFishPond.GetList(_model));
        }
        public async Task<FishPondModel> GetDetail(int _id)
        {
            return iMapper.Map<FishPondModel>(await svcFishPond.GetDetail(_id));
        }
        public async Task<int> Add(FishPondModel _model)
        {
            FishPond entity = iMapper.Map<FishPond>(_model);
            return await svcFishPond.Add(entity);
        }
        public async Task<bool> Modify(int _id, FishPondModel _model)
        {
            FishPond entity = await svcFishPond.GetDetail(_id);
            if (entity == null) return false;
            entity = iMapper.Map(_model, entity);
            return await svcFishPond.Modify(entity);
        }
        public async Task<bool> Remove(int _id)
        {
            return await svcFishPond.Remove(_id);
        }
        public async Task<List<IdNameModel>> GetCache()
        {
            return iMapper.Map<List<IdNameModel>>(await svcFishPond.GetCache());
        }
    }
}
