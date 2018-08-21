using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Interfaces
{
    public interface IExpenditureDocketTypeBusiness
    {
        Task<PagedList<ExpenditureDocketTypeModel>> GetList(PageFilterModel _model);
        Task<ExpenditureDocketTypeModel> GetDetail(int _id);
        Task<int> Add(ExpenditureDocketTypeModel _model);
        Task<bool> Modify(int _id, ExpenditureDocketTypeModel _model);
        Task<bool> Remove(int _id);
        Task<List<IdNameModel>> GetCache();
    }
}
