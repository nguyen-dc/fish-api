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
        Task<int> Add(FarmRegion _model);
        Task<bool> Modify(FarmRegion _model);
        Task<bool> Remove(int _id);
        Task<List<FarmRegion>> GetCache();
    }
}
