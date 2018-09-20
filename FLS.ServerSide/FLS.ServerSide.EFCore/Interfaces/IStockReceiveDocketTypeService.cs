using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface IStockReceiveDocketTypeService
    {
        Task<PagedList<StockReceiveDocketType>> GetList(PageFilterModel _model);
        Task<StockReceiveDocketType> GetDetail(int _id);
        Task<int> Add(StockReceiveDocketType _model);
        Task<bool> Modify(StockReceiveDocketType _model);
        Task<bool> Remove(int _id);
        Task<List<StockReceiveDocketType>> GetCache();
    }
}
