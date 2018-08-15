using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Interfaces
{
    public interface IFarmRegionBusiness
    {
        Task<PagedList<FarmRegionModel>> GetList(PageFilterModel _model);
        Task<FarmRegionModel> GetDetail(int _id);
        Task<int> Add(FarmRegionModel _model);
        Task<bool> Modify(int _id, FarmRegionModel _model);
        Task<bool> Remove(int _id);
        Task<List<IdNameModel>> GetCache();
    }
}
