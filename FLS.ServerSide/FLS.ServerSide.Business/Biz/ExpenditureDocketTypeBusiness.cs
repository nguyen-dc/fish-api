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
    public class ExpenditureDocketTypeBusiness : IExpenditureDocketTypeBusiness
    {
        private readonly IExpenditureDocketTypeService svcExpenditureDocketType;
        private readonly IMapper iMapper;
        public ExpenditureDocketTypeBusiness(IExpenditureDocketTypeService _svcExpenditureDocketType, IMapper _iMapper)
        {
            svcExpenditureDocketType = _svcExpenditureDocketType;
            iMapper = _iMapper;
        }
        public async Task<PagedList<ExpenditureDocketTypeModel>> GetList(PageFilterModel _model)
        {
            return iMapper.Map<PagedList<ExpenditureDocketTypeModel>>(await svcExpenditureDocketType.GetList(_model));
        }
        public async Task<ExpenditureDocketTypeModel> GetDetail(int _id)
        {
            return iMapper.Map<ExpenditureDocketTypeModel>(await svcExpenditureDocketType.GetDetail(_id));
        }
        public async Task<int> Add(ExpenditureDocketTypeModel _model)
        {
            ExpenditureDocketType entity = iMapper.Map<ExpenditureDocketType>(_model);
            return await svcExpenditureDocketType.Add(entity);
        }
        public async Task<bool> Modify(int _id, ExpenditureDocketTypeModel _model)
        {
            ExpenditureDocketType entity = await svcExpenditureDocketType.GetDetail(_id);
            if (entity == null) return false;
            entity = iMapper.Map(_model, entity);
            return await svcExpenditureDocketType.Modify(entity);
        }
        public async Task<bool> Remove(int _id)
        {
            return await svcExpenditureDocketType.Remove(_id);
        }
        public async Task<List<IdNameModel>> GetCache()
        {
            return iMapper.Map<List<IdNameModel>>(await svcExpenditureDocketType.GetCache());
        }
    }
}
