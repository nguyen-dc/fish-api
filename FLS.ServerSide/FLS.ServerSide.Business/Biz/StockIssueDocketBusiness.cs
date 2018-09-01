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
    public class StockIssueDocketBusiness : IStockIssueDocketBusiness
    {
        private readonly IStockIssueDocketService svcStockIssueDocket;
        private readonly IMapper iMapper;
        public StockIssueDocketBusiness(IStockIssueDocketService _svcStockIssueDocket, IMapper _iMapper)
        {
            svcStockIssueDocket = _svcStockIssueDocket;
            iMapper = _iMapper;
        }
        public async Task<PagedList<StockIssueDocketModel>> GetList(PageFilterModel _model)
        {
            return iMapper.Map<PagedList<StockIssueDocketModel>>(await svcStockIssueDocket.GetList(_model));
        }
        public async Task<StockIssueDocketModel> GetDetail(int _id)
        {
            return iMapper.Map<StockIssueDocketModel>(await svcStockIssueDocket.GetDetail(_id));
        }
        public async Task<int> Add(ExportStockModel _model)
        {
            //    StockIssueDocket entity = iMapper.Map<StockIssueDocket>(_model);
            //    return await svcStockIssueDocket.Add(entity);
            return 0;
        }
        public async Task<bool> Modify(int _id, StockIssueDocketModel _model)
        {
            StockIssueDocket entity = await svcStockIssueDocket.GetDetail(_id);
            if (entity == null) return false;
            entity = iMapper.Map(_model, entity);
            return await svcStockIssueDocket.Modify(entity);
        }
        public async Task<bool> Remove(int _id)
        {
            return await svcStockIssueDocket.Remove(_id);
        }
    }
}
