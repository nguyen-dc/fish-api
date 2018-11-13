using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface IFarmingSeasonHistoryService
    {
        Task<FarmingSeasonHistory> GetDetail(int _id);
        Task<int> Add(FarmingSeasonHistory _model);
        Task<bool> Modify(FarmingSeasonHistory _model);
    }
}
