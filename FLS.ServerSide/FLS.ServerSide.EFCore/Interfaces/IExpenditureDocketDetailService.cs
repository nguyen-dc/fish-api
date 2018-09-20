using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface IExpenditureDocketDetailService
    {
        Task<PagedList<ExpenditureDocketDetail>> GetList(PageFilterModel _model);
        Task<ExpenditureDocketDetail> GetDetail(int _id);
        Task<int> Add(ExpenditureDocketDetail _model);
        Task<bool> Modify(ExpenditureDocketDetail _model);
        Task<bool> Remove(int _id);
    }
}
