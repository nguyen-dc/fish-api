using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface ILivestockHistoryDetailService
    {
        Task<LivestockHistoryDetail> GetDetail(int _id);
        Task<int> Add(LivestockHistoryDetail _model);
        Task<bool> Modify(LivestockHistoryDetail _model);
    }
}
