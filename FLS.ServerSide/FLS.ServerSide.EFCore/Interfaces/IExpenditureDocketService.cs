using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface IExpenditureDocketService
    {
        Task<PagedList<ExpenditureDocket>> GetList(PageFilterModel _model);
        Task<ExpenditureDocket> GetDetail(int _id);
        Task<int> Add(ExpenditureDocket _model, bool _isSaveChange = true);
        Task<bool> Modify(ExpenditureDocket _model, bool _isSaveChange = true);
        Task<bool> Remove(int _id, bool _isSaveChange = true);
    }
}
