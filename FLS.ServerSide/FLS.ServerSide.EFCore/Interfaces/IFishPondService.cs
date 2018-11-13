using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface IFishPondService
    {
        Task<PagedList<FishPondModel>> GetList(PageFilterModel _model);
        Task<FishPond> GetDetail(int _id);
        Task<FishPond> GetByWarehouseId(int _warehouseId);
        Task<int> Add(FishPond _model);
        Task<bool> Modify(FishPond _model);
        Task<bool> Remove(int _id);
        Task<List<FishPond>> GetCache();
    }
}
