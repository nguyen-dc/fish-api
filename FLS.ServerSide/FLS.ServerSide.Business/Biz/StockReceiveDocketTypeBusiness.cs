using AutoMapper;
using FLS.ServerSide.Business.Interfaces;
using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.EFCore.Services;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Biz
{
    public class StockReceiveDocketTypeBusiness : IStockReceiveDocketTypeBusiness
    {
        private readonly IStockReceiveDocketTypeService svcStockReceiveDocketType;
        private readonly IMapper iMapper;
        public StockReceiveDocketTypeBusiness(IStockReceiveDocketTypeService _svcStockReceiveDocketType, IMapper _iMapper)
        {
            svcStockReceiveDocketType = _svcStockReceiveDocketType;
            iMapper = _iMapper;
        }
        public async Task<PagedList<StockReceiveDocketTypeModel>> GetList(PageFilterModel _model)
        {
            return iMapper.Map<PagedList<StockReceiveDocketTypeModel>>(await svcStockReceiveDocketType.GetList(_model));
        }
        public async Task<StockReceiveDocketTypeModel> GetDetail(int _id)
        {
            return iMapper.Map<StockReceiveDocketTypeModel>(await svcStockReceiveDocketType.GetDetail(_id));
        }
        public async Task<int> Add(StockReceiveDocketTypeModel _model)
        {
            StockReceiveDocketType entity = iMapper.Map<StockReceiveDocketType>(_model);
            return await svcStockReceiveDocketType.Add(entity);
        }
        public async Task<bool> Modify(int _id, StockReceiveDocketTypeModel _model)
        {
            StockReceiveDocketType entity = await svcStockReceiveDocketType.GetDetail(_id);
            if (entity == null) return false;
            entity = iMapper.Map(_model, entity);
            return await svcStockReceiveDocketType.Modify(entity);
        }
        public async Task<bool> Remove(int _id)
        {
            return await svcStockReceiveDocketType.Remove(_id);
        }
        public async Task<List<IdNameModel>> GetCache()
        {
            var list = await svcStockReceiveDocketType.GetCache();
            list.RemoveAll(l => l.IsSystem == true && l.Id != (int)SystemIDEnum.ImportStock_TypeDefault);
            var result = iMapper.Map<List<IdNameModel>>(list);
            return result;
        }
    }
}
