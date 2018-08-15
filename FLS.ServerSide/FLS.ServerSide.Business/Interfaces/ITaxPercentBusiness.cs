using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Interfaces
{
    public interface ITaxPercentBusiness
    {
        Task<PagedList<TaxPercentModel>> GetList(PageFilterModel _model);
        Task<TaxPercentModel> GetDetail(int _id);
        Task<int> Add(TaxPercentModel _model);
        Task<bool> Modify(int _id, TaxPercentModel _model);
        Task<bool> Remove(int _id);
        Task<List<IdNameModel>> GetCache();
    }
}
