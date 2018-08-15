using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Interfaces
{
    public interface IStockReceiveDocketTypeBusiness
    {
        Task<PagedList<StockReceiveDocketTypeModel>> GetList(PageFilterModel _model);
        Task<StockReceiveDocketTypeModel> GetDetail(int _id);
        Task<int> Add(StockReceiveDocketTypeModel _model);
        Task<bool> Modify(int _id, StockReceiveDocketTypeModel _model);
        Task<bool> Remove(int _id);
        Task<List<IdNameModel>> GetCache();
    }
}
