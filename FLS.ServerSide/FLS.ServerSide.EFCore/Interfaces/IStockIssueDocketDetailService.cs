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
        Task<int> Add(StockIssueDocketDetail _model);
        Task<bool> Modify(StockIssueDocketDetail _model);
        Task<bool> Remove(int _id);
    }
}
