using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Interfaces
{
    public interface IStockIssueDocketTypeBusiness
    {
        Task<PagedList<StockIssueDocketTypeModel>> GetList(PageFilterModel _model);
        Task<StockIssueDocketTypeModel> GetDetail(int _id);
        Task<int> Add(StockIssueDocketTypeModel _model);
        Task<bool> Modify(int _id, StockIssueDocketTypeModel _model);
        Task<bool> Remove(int _id);
        Task<List<IdNameModel>> GetCache();
    }
}
