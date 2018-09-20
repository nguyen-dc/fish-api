using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface IStockReceiveDocketDetailService
    {
        Task<PagedList<StockReceiveDocketDetail>> GetList(PageFilterModel _model);
        Task<StockReceiveDocketDetail> GetDetail(int _id);
        Task<int> Add(StockReceiveDocketDetail _model);
        Task<bool> Modify(StockReceiveDocketDetail _model);
        Task<bool> Remove(int _id);
    }
}
