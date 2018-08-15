using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface IStockReceiveDocketService
    {
        Task<PagedList<StockReceiveDocket>> GetList(PageFilterModel _model);
        Task<StockReceiveDocket> GetDetail(int _id);
        Task<int> Add(StockReceiveDocket _model, bool _isSaveChange = true);
        Task<bool> Modify(StockReceiveDocket _model, bool _isSaveChange = true);
        Task<bool> Remove(int _id, bool _isSaveChange = true);
    }
}
