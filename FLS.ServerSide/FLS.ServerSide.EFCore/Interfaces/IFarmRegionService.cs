using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface IFarmRegionService
    {
        Task<PagedList<FarmRegion>> GetList(PageFilterModel _model);
        Task<FarmRegion> GetDetail(int _id);
        Task<int> Add(FarmRegion _model, bool _isSaveChange = true);
        Task<bool> Modify(FarmRegion _model, bool _isSaveChange = true);
        Task<bool> Remove(int _id, bool _isSaveChange = true);
        Task<List<FarmRegion>> GetCache();
    }
}
