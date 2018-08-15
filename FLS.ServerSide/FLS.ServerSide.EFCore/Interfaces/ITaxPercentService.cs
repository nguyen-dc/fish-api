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
        Task<int> Add(TaxPercent _model, bool _isSaveChange = true);
        Task<bool> Modify(TaxPercent _model, bool _isSaveChange = true);
        Task<bool> Remove(int _id, bool _isSaveChange = true);
        Task<List<TaxPercent>> GetCache();
    }
}
