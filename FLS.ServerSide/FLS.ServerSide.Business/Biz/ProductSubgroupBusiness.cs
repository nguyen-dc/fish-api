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
    public class ProductSubgroupBusiness : IProductSubgroupBusiness
    {
        private readonly IProductSubgroupService svcProductSubgroup;
        private readonly IMapper iMapper;
        public ProductSubgroupBusiness(IProductSubgroupService _svcProductSubgroup, IMapper _iMapper)
        {
            svcProductSubgroup = _svcProductSubgroup;
            iMapper = _iMapper;
        }
        public async Task<PagedList<ProductSubgroupModel>> GetList(PageFilterModel _model)
        {
            return iMapper.Map<PagedList<ProductSubgroupModel>>(await svcProductSubgroup.GetList(_model));
        }
        public async Task<ProductSubgroupModel> GetDetail(int _id)
        {
            return iMapper.Map<ProductSubgroupModel>(await svcProductSubgroup.GetDetail(_id));
        }
        public async Task<int> Add(ProductSubgroupModel _model)
        {
            ProductSubgroup entity = iMapper.Map<ProductSubgroup>(_model);
            return await svcProductSubgroup.Add(entity);
        }
        public async Task<bool> Modify(int _id, ProductSubgroupModel _model)
        {
            ProductSubgroup entity = await svcProductSubgroup.GetDetail(_id);
            if (entity == null) return false;
            entity = iMapper.Map(_model, entity);
            return await svcProductSubgroup.Modify(entity);
        }
        public async Task<bool> Remove(int _id)
        {
            return await svcProductSubgroup.Remove(_id);
        }
        public async Task<PagedList<ProductModel>> GetProducts(int _subgroupId, PageFilterModel _model)
        {
            return iMapper.Map<PagedList<ProductModel>>(await svcProductSubgroup.GetProducts(_subgroupId, _model));
        }
        public async Task<List<IdNameModel>> GetCache()
        {
            return iMapper.Map<List<IdNameModel>>(await svcProductSubgroup.GetCache());
        }
    }
}
