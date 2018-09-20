using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface IExpenditureDocketTypeService
    {
        Task<PagedList<ExpenditureDocketType>> GetList(PageFilterModel _model);
        Task<ExpenditureDocketType> GetDetail(int _id);
        Task<int> Add(ExpenditureDocketType _model);
        Task<bool> Modify(ExpenditureDocketType _model);
        Task<bool> Remove(int _id);
        Task<List<ExpenditureDocketType>> GetCache();
    }
}
