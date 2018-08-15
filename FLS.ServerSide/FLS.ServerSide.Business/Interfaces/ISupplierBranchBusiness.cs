using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Interfaces
{
    public interface ISupplierBranchBusiness
    {
        Task<PagedList<SupplierBranchModel>> GetList(PageFilterModel _model);
        Task<SupplierBranchModel> GetDetail(int _id);
        Task<int> Add(SupplierBranchModel _model);
        Task<bool> Modify(int _id, SupplierBranchModel _model);
        Task<bool> Remove(int _id);
    }
}
