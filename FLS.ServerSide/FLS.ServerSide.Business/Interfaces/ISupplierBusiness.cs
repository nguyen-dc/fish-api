using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Interfaces
{
    public interface ISupplierBusiness
    {
        Task<PagedList<SupplierModel>> GetList(PageFilterModel _model);
        Task<SupplierModel> GetDetail(int _id);
        Task<int> Add(SupplierModel _model);
        Task<bool> Modify(int _id, SupplierModel _model);
        Task<bool> Remove(int _id);
        Task<PagedList<SupplierBranchModel>> GetBranchs(int _branchId, PageFilterModel _model);
    }
}
