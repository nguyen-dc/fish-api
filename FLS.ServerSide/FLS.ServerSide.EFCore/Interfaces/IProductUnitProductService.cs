using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface IProductUnitProductService
    {
        Task<PagedList<ProductUnitProduct>> GetList(PageFilterModel _model);
        Task<ProductUnitProduct> GetDetail(int _id);
        Task<int> Add(ProductUnitProduct _model);
        Task<bool> Modify(ProductUnitProduct _model);
        Task<bool> Remove(int _id);
    }
}
