using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Interfaces
{
    public interface IProductBusiness
    {
        Task<PagedList<ProductModel>> GetStockList(PageFilterModel _model);
        Task<PagedList<ProductModel>> GetLivestockList(PageFilterModel _model);
        Task<ProductModel> GetDetail(int _id);
        Task<int> Add(ProductModel _model);
        Task<bool> Modify(int _id, ProductModel _model);
        Task<bool> Remove(int _id);
    }
}
