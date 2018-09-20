using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface IWarehouseTypeService
    {
        Task<PagedList<WarehouseType>> GetList(PageFilterModel _model);
        Task<WarehouseType> GetDetail(int _id);
        Task<int> Add(WarehouseType _model);
        Task<bool> Modify(WarehouseType _model);
        Task<bool> Remove(int _id);
        Task<List<WarehouseType>> GetCache();
    }
}
