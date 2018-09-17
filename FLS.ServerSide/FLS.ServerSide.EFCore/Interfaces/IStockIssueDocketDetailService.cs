using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface IStockIssueDocketDetailService
    {
        Task<PagedList<StockIssueDocketDetail>> GetList(PageFilterModel _model);
        Task<StockIssueDocketDetail> GetDetail(int _id);
        Task<int> Add(StockIssueDocketDetail _model, bool _isSaveChange = true);
        Task<bool> Modify(StockIssueDocketDetail _model, bool _isSaveChange = true);
        Task<bool> Remove(int _id, bool _isSaveChange = true);
    }
}
