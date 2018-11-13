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
    public class StockIssueDocketTypeBusiness : IStockIssueDocketTypeBusiness
    {
        private readonly IStockIssueDocketTypeService svcStockIssueDocketType;
        private readonly IMapper iMapper;
        public StockIssueDocketTypeBusiness(IStockIssueDocketTypeService _svcStockIssueDocketType, IMapper _iMapper)
        {
            svcStockIssueDocketType = _svcStockIssueDocketType;
            iMapper = _iMapper;
        }
        public async Task<PagedList<StockIssueDocketTypeModel>> GetList(PageFilterModel _model)
        {
            return iMapper.Map<PagedList<StockIssueDocketTypeModel>>(await svcStockIssueDocketType.GetList(_model));
        }
        public async Task<StockIssueDocketTypeModel> GetDetail(int _id)
        {
            return iMapper.Map<StockIssueDocketTypeModel>(await svcStockIssueDocketType.GetDetail(_id));
        }
        public async Task<int> Add(StockIssueDocketTypeModel _model)
        {
            StockIssueDocketType entity = iMapper.Map<StockIssueDocketType>(_model);
            return await svcStockIssueDocketType.Add(entity);
        }
        public async Task<bool> Modify(int _id, StockIssueDocketTypeModel _model)
        {
            StockIssueDocketType entity = await svcStockIssueDocketType.GetDetail(_id);
            if (entity == null) return false;
            entity = iMapper.Map(_model, entity);
            return await svcStockIssueDocketType.Modify(entity);
        }
        public async Task<bool> Remove(int _id)
        {
            return await svcStockIssueDocketType.Remove(_id);
        }
        public async Task<List<IdNameModel>> GetCache()
        {
            var list = await svcStockIssueDocketType.GetCache();
            list.RemoveAll(l => l.IsSystem == true && l.Id != (int)SystemIDEnum.ExportStock_TypeDefault);
            var result = iMapper.Map<List<IdNameModel>>(list);
            return result;
        }
    }
}
