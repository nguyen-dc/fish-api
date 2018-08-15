using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Interfaces
{
    public interface IFarmingSeasonBusiness
    {
        Task<PagedList<FarmingSeasonModel>> GetList(PageFilterModel _model);
        Task<FarmingSeasonModel> GetDetail(int _id);
        Task<int> Add(FarmingSeasonModel _model);
        Task<bool> Modify(int _id, FarmingSeasonModel _model);
        Task<bool> Remove(int _id);
    }
}
