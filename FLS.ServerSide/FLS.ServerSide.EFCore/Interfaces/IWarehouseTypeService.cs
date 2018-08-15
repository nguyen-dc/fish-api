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
        Task<int> Add(WarehouseType _model, bool _isSaveChange = true);
        Task<bool> Modify(WarehouseType _model, bool _isSaveChange = true);
        Task<bool> Remove(int _id, bool _isSaveChange = true);
        Task<List<WarehouseType>> GetCache();
    }
}
