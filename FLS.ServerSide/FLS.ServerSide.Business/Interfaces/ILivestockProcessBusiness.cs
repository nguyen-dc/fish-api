using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Interfaces
{
    public interface ILivestockProcessBusiness
    {
        Task<int> ReleaseLivestock(ReleaseLivestockModel _model);
        Task<int> FeedingLivestock(FeedingLivestockModel _model);
        Task<int> CuringLivestock(FeedingLivestockModel _model);
    }
}
