using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface IProductSubgroupService
    {
        Task<PagedList<ProductSubgroup>> GetList(PageFilterModel _model);
        Task<ProductSubgroup> GetDetail(int _id);
        Task<int> Add(ProductSubgroup _model);
        Task<bool> Modify(ProductSubgroup _model);
        Task<bool> Remove(int _id);
        Task<PagedList<Product>> GetProducts(int _subgroupId, PageFilterModel _model);
        Task<List<ProductSubgroup>> GetCache();
    }
}
