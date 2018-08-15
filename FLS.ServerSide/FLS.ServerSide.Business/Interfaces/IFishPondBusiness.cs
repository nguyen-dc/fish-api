using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Interfaces
{
    public interface IFishPondBusiness
    {
        Task<PagedList<FishPondModel>> GetList(PageFilterModel _model);
        Task<FishPondModel> GetDetail(int _id);
        Task<int> Add(FishPondModel _model);
        Task<bool> Modify(int _id, FishPondModel _model);
        Task<bool> Remove(int _id);
        Task<List<IdNameModel>> GetCache();
    }
}
