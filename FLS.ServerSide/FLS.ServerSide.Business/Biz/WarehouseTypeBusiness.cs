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
    public class WarehouseTypeBusiness : IWarehouseTypeBusiness
    {
        private readonly IWarehouseTypeService svcWarehouseType;
        private readonly IMapper iMapper;
        public WarehouseTypeBusiness(IWarehouseTypeService _svcWarehouseType, IMapper _iMapper)
        {
            svcWarehouseType = _svcWarehouseType;
            iMapper = _iMapper;
        }
        public async Task<PagedList<WarehouseTypeModel>> GetList(PageFilterModel _model)
        {
            return iMapper.Map<PagedList<WarehouseTypeModel>>(await svcWarehouseType.GetList(_model));
        }
        public async Task<WarehouseTypeModel> GetDetail(int _id)
        {
            return iMapper.Map<WarehouseTypeModel>(await svcWarehouseType.GetDetail(_id));
        }
        public async Task<int> Add(WarehouseTypeModel _model)
        {
            WarehouseType entity = iMapper.Map<WarehouseType>(_model);
            return await svcWarehouseType.Add(entity);
        }
        public async Task<bool> Modify(int _id, WarehouseTypeModel _model)
        {
            WarehouseType entity = await svcWarehouseType.GetDetail(_id);
            if (entity == null) return false;
            entity = iMapper.Map(_model, entity);
            return await svcWarehouseType.Modify(entity);
        }
        public async Task<bool> Remove(int _id)
        {
            return await svcWarehouseType.Remove(_id);
        }
        public async Task<List<IdNameModel>> GetCache()
        {
            return iMapper.Map<List<IdNameModel>>(await svcWarehouseType.GetCache());
        }
    }
}
