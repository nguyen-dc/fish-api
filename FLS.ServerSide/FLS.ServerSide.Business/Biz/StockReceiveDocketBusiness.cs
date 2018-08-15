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
    public class StockReceiveDocketBusiness : IStockReceiveDocketBusiness
    {
        private readonly IStockReceiveDocketService svcStockReceiveDocket;
        private readonly IMapper iMapper;
        public StockReceiveDocketBusiness(IStockReceiveDocketService _svcStockReceiveDocket, IMapper _iMapper)
        {
            svcStockReceiveDocket = _svcStockReceiveDocket;
            iMapper = _iMapper;
        }
        public async Task<PagedList<StockReceiveDocketModel>> GetList(PageFilterModel _model)
        {
            return iMapper.Map<PagedList<StockReceiveDocketModel>>(await svcStockReceiveDocket.GetList(_model));
        }
        public async Task<StockReceiveDocketModel> GetDetail(int _id)
        {
            return iMapper.Map<StockReceiveDocketModel>(await svcStockReceiveDocket.GetDetail(_id));
        }
        public async Task<int> Add(StockReceiveDocketModel _model)
        {
            StockReceiveDocket entity = iMapper.Map<StockReceiveDocket>(_model);
            return await svcStockReceiveDocket.Add(entity);
        }
        public async Task<bool> Modify(int _id, StockReceiveDocketModel _model)
        {
            StockReceiveDocket entity = await svcStockReceiveDocket.GetDetail(_id);
            if (entity == null) return false;
            entity = iMapper.Map(_model, entity);
            return await svcStockReceiveDocket.Modify(entity);
        }
        public async Task<bool> Remove(int _id)
        {
            return await svcStockReceiveDocket.Remove(_id);
        }
    }
}
