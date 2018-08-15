using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Interfaces
{
    public interface IReceiptTypeBusiness
    {
        Task<PagedList<ExpenditureTypeModel>> GetList(PageFilterModel _model);
        Task<ExpenditureTypeModel> GetDetail(int _id);
        Task<int> Add(ExpenditureTypeModel _model);
        Task<bool> Modify(int _id, ExpenditureTypeModel _model);
        Task<bool> Remove(int _id);
        Task<List<IdNameModel>> GetCache();
    }
}
