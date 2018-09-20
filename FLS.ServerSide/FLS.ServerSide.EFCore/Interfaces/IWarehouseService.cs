using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface IWarehouseService
    {
        Task<PagedList<Warehouse>> GetList(PageFilterModel _model);
        Task<Warehouse> GetDetail(int _id);
        Task<int> Add(Warehouse _model);
        Task<bool> Modify(Warehouse _model);
        Task<bool> Remove(int _id);
        Task<List<Warehouse>> GetCache();
    }
}
