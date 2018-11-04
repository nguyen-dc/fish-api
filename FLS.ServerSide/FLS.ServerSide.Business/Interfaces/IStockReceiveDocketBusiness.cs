using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Interfaces
{
    public interface IStockReceiveDocketBusiness
    {
        Task<PagedList<StockReceiveDocketModel>> GetList(PageFilterModel _model);
        Task<ImportStockDetailModel> GetDetail(int _id);
        Task<int> Add(ImportStockModel _model);
        Task<bool> Modify(int _id, StockReceiveDocketModel _model);
        Task<bool> Remove(int _id);
        Task<int> ReleaseLivestock(ImportStockModel _model);
    }
}
