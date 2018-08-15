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
    public class ProductUnitBusiness : IProductUnitBusiness
    {
        private readonly IProductUnitService svcProductUnit;
        private readonly IMapper iMapper;
        public ProductUnitBusiness(IProductUnitService _svcProductUnit, IMapper _iMapper)
        {
            svcProductUnit = _svcProductUnit;
            iMapper = _iMapper;
        }
        public async Task<PagedList<ProductUnitModel>> GetList(PageFilterModel _model)
        {
            return iMapper.Map<PagedList<ProductUnitModel>>(await svcProductUnit.GetList(_model));
        }
        public async Task<ProductUnitModel> GetDetail(int _id)
        {
            return iMapper.Map<ProductUnitModel>(await svcProductUnit.GetDetail(_id));
        }
        public async Task<int> Add(ProductUnitModel _model)
        {
            ProductUnit entity = iMapper.Map<ProductUnit>(_model);
            return await svcProductUnit.Add(entity);
        }
        public async Task<bool> Modify(int _id, ProductUnitModel _model)
        {
            ProductUnit entity = await svcProductUnit.GetDetail(_id);
            if (entity == null) return false;
            entity = iMapper.Map(_model, entity);
            return await svcProductUnit.Modify(entity);
        }
        public async Task<bool> Remove(int _id)
        {
            return await svcProductUnit.Remove(_id);
        }
        public async Task<List<IdNameModel>> GetCache()
        {
            return iMapper.Map<List<IdNameModel>>(await svcProductUnit.GetCache());
        }
    }
}
