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
    public class WarehouseBusiness : IWarehouseBusiness
    {
        private readonly IWarehouseService svcWarehouse;
        private readonly IMapper iMapper;
        public WarehouseBusiness(IWarehouseService _svcWarehouse, IMapper _iMapper)
        {
            svcWarehouse = _svcWarehouse;
            iMapper = _iMapper;
        }
        public async Task<PagedList<WarehouseModel>> GetList(PageFilterModel _model)
        {
            return iMapper.Map<PagedList<WarehouseModel>>(await svcWarehouse.GetList(_model));
        }
        public async Task<WarehouseModel> GetDetail(int _id)
        {
            return iMapper.Map<WarehouseModel>(await svcWarehouse.GetDetail(_id));
        }
        public async Task<int> Add(WarehouseModel _model)
        {
            Warehouse entity = iMapper.Map<Warehouse>(_model);
            return await svcWarehouse.Add(entity);
        }
        public async Task<bool> Modify(int _id, WarehouseModel _model)
        {
            Warehouse entity = await svcWarehouse.GetDetail(_id);
            if (entity == null) return false;
            entity = iMapper.Map(_model, entity);
            return await svcWarehouse.Modify(entity);
        }
        public async Task<bool> Remove(int _id)
        {
            return await svcWarehouse.Remove(_id);
        }
        public async Task<List<IdNameModel>> GetCache()
        {
            return iMapper.Map<List<IdNameModel>>(await svcWarehouse.GetCache());
        }
    }
}
