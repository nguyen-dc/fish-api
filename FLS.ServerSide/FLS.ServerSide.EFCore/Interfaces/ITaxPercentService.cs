using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface ITaxPercentService
    {
        Task<PagedList<TaxPercent>> GetList(PageFilterModel _model);
        Task<TaxPercent> GetDetail(int _id);
        Task<int> Add(TaxPercent _model);
        Task<bool> Modify(TaxPercent _model);
        Task<bool> Remove(int _id);
        Task<List<TaxPercent>> GetCache();
    }
}
