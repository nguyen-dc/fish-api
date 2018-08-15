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
    public class ProductGroupBusiness : IProductGroupBusiness
    {
        private readonly IProductGroupService svcProductGroup;
        private readonly IMapper iMapper;
        public ProductGroupBusiness(IProductGroupService _svcProductGroup, IMapper _iMapper)
        {
            svcProductGroup = _svcProductGroup;
            iMapper = _iMapper;
        }
        public async Task<PagedList<ProductGroupModel>> GetList(PageFilterModel _model)
        {
            return iMapper.Map<PagedList<ProductGroupModel>>(await svcProductGroup.GetList(_model));
        }
        public async Task<ProductGroupModel> GetDetail(int _id)
        {
            return iMapper.Map<ProductGroupModel>(await svcProductGroup.GetDetail(_id));
        }
        public async Task<int> Add(ProductGroupModel _model)
        {
            ProductGroup entity = iMapper.Map<ProductGroup>(_model);
            return await svcProductGroup.Add(entity);
        }
        public async Task<bool> Modify(int _id, ProductGroupModel _model)
        {
            ProductGroup entity = await svcProductGroup.GetDetail(_id);
            if (entity == null) return false;
            entity = iMapper.Map(_model, entity);
            return await svcProductGroup.Modify(entity);
        }
        public async Task<bool> Remove(int _id)
        {
            return await svcProductGroup.Remove(_id);
        }
        public async Task<PagedList<ProductSubgroupModel>> GetSubgroups(int _groupId, PageFilterModel _model)
        {
            return iMapper.Map<PagedList<ProductSubgroupModel>>(await svcProductGroup.GetSubgroups(_groupId, _model));
        }
        public async Task<PagedList<ProductModel>> GetProducts(int _groupId, PageFilterModel _model)
        {
            return iMapper.Map<PagedList<ProductModel>>(await svcProductGroup.GetProducts(_groupId, _model));
        }
        public async Task<List<IdNameModel>> GetCache()
        {
            return iMapper.Map<List<IdNameModel>>(await svcProductGroup.GetCache());
        }
    }
}
