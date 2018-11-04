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
    public class ProductBusiness : IProductBusiness
    {
        private readonly IProductService svcProduct;
        private readonly IMapper iMapper;
        public ProductBusiness(IProductService _svcProduct, IMapper _iMapper)
        {
            svcProduct = _svcProduct;
            iMapper = _iMapper;
        }
        public async Task<PagedList<ProductModel>> GetStockList(PageFilterModel _model)
        {
            return iMapper.Map<PagedList<ProductModel>>(await svcProduct.GetStockList(_model));
        }
        public async Task<PagedList<ProductModel>> GetLivestockList(PageFilterModel _model)
        {
            return iMapper.Map<PagedList<ProductModel>>(await svcProduct.GetLivestockList(_model));
        }
        public async Task<ProductModel> GetDetail(int _id)
        {
            return iMapper.Map<ProductModel>(await svcProduct.GetDetail(_id));
        }
        public async Task<int> Add(ProductModel _model)
        {
            Product entity = iMapper.Map<Product>(_model);
            return await svcProduct.Add(entity);
        }
        public async Task<bool> Modify(int _id, ProductModel _model)
        {
            Product entity = await svcProduct.GetDetail(_id);
            if (entity == null) return false;
            entity = iMapper.Map(_model, entity);
            return await svcProduct.Modify(entity);
        }
        public async Task<bool> Remove(int _id)
        {
            return await svcProduct.Remove(_id);
        }
    }
}
