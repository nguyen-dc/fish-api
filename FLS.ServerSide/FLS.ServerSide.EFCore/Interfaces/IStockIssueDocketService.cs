using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface IStockIssueDocketService
    {
        Task<PagedList<StockIssueDocket>> GetList(PageFilterModel _model);
        Task<StockIssueDocket> GetDetail(int _id);
        Task<int> Add(StockIssueDocket _model);
        Task<bool> Modify(StockIssueDocket _model);
        Task<bool> Remove(int _id);
    }
}
