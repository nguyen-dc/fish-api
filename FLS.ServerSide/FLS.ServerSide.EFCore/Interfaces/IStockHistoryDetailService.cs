using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface IStockHistoryDetailService
    {
        Task<StockHistoryDetail> GetDetail(int _id);
        Task<int> Add(StockHistoryDetail _model);
        Task<bool> Modify(StockHistoryDetail _model);
    }
}
