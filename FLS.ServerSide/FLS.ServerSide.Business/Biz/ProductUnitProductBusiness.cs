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
    public class ProductUnitProductBusiness : IProductUnitProductBusiness
    {
        private readonly IProductUnitProductService svcProductUnitProduct;
        private readonly IMapper iMapper;
        public ProductUnitProductBusiness(IProductUnitProductService _svcProductUnitProduct, IMapper _iMapper)
        {
            svcProductUnitProduct = _svcProductUnitProduct;
            iMapper = _iMapper;
        }
        public async Task<PagedList<ProductUnitProductModel>> GetList(PageFilterModel _model)
        {
            return iMapper.Map<PagedList<ProductUnitProductModel>>(await svcProductUnitProduct.GetList(_model));
        }
        public async Task<ProductUnitProductModel> GetDetail(int _id)
        {
            return iMapper.Map<ProductUnitProductModel>(await svcProductUnitProduct.GetDetail(_id));
        }
        public async Task<int> Add(ProductUnitProductModel _model)
        {
            ProductUnitProduct entity = iMapper.Map<ProductUnitProduct>(_model);
            return await svcProductUnitProduct.Add(entity);
        }
        public async Task<bool> Modify(int _id, ProductUnitProductModel _model)
        {
            ProductUnitProduct entity = await svcProductUnitProduct.GetDetail(_id);
            if (entity == null) return false;
            entity = iMapper.Map(_model, entity);
            return await svcProductUnitProduct.Modify(entity);
        }
        public async Task<bool> Remove(int _id)
        {
            return await svcProductUnitProduct.Remove(_id);
        }
    }
}
