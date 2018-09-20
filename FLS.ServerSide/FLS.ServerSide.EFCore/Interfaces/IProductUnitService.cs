using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface IProductUnitService
    {
        Task<PagedList<ProductUnit>> GetList(PageFilterModel _model);
        Task<ProductUnit> GetDetail(int _id);
        Task<int> Add(ProductUnit _model);
        Task<bool> Modify(ProductUnit _model);
        Task<bool> Remove(int _id);
        Task<List<ProductUnit>> GetCache();
    }
}
