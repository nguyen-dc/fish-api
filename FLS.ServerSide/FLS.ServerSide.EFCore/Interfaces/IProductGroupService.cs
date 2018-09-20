using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface IProductGroupService
    {
        Task<PagedList<ProductGroup>> GetList(PageFilterModel _model);
        Task<ProductGroup> GetDetail(int _id);
        Task<int> Add(ProductGroup _model);
        Task<bool> Modify(ProductGroup _model);
        Task<bool> Remove(int _id);
        Task<PagedList<ProductSubgroup>> GetSubgroups(int _groupId, PageFilterModel _model);
        Task<PagedList<Product>> GetProducts(int _groupId, PageFilterModel _model);
        Task<List<ProductGroup>> GetCache();
    }
}
