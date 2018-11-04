using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface IProductService
    {
        Task<PagedList<Product>> GetStockList(PageFilterModel _model);
        Task<PagedList<Product>> GetLivestockList(PageFilterModel _model);
        Task<Product> GetDetail(int _id);
        Task<int> Add(Product _model);
        Task<bool> Modify(Product _model);
        Task<bool> Remove(int _id);
    }
}
