using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface IProductService
    {
        Task<PagedList<Product>> GetList(PageFilterModel _model);
        Task<Product> GetDetail(int _id);
        Task<int> Add(Product _model, bool _isSaveChange = true);
        Task<bool> Modify(Product _model, bool _isSaveChange = true);
        Task<bool> Remove(int _id, bool _isSaveChange = true);
    }
}
