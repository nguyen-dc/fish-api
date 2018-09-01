using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Interfaces
{
    public interface IStockIssueDocketBusiness
    {
        Task<PagedList<StockIssueDocketModel>> GetList(PageFilterModel _model);
        Task<StockIssueDocketModel> GetDetail(int _id);
        Task<int> Add(ExportStockModel _model);
        Task<bool> Modify(int _id, StockIssueDocketModel _model);
        Task<bool> Remove(int _id);
    }
}
