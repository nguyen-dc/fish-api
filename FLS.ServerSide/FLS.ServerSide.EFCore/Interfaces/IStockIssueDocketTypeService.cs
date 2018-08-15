using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface IStockIssueDocketTypeService
    {
        Task<PagedList<StockIssueDocketType>> GetList(PageFilterModel _model);
        Task<StockIssueDocketType> GetDetail(int _id);
        Task<int> Add(StockIssueDocketType _model, bool _isSaveChange = true);
        Task<bool> Modify(StockIssueDocketType _model, bool _isSaveChange = true);
        Task<bool> Remove(int _id, bool _isSaveChange = true);
        Task<List<StockIssueDocketType>> GetCache();
    }
}
