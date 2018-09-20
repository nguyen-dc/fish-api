using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface ISupplierService
    {
        Task<PagedList<Supplier>> GetList(PageFilterModel _model);
        Task<Supplier> GetDetail(int _id);
        Task<int> Add(Supplier _model);
        Task<bool> Modify(Supplier _model);
        Task<bool> Remove(int _id);
        Task<PagedList<SupplierBranch>> GetBranchs(int _branchId, PageFilterModel _model);
    }
}
