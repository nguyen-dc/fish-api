using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Services
{
    public interface IFeedConversionRateService
    {
        Task<FeedConversionRate> GetDetail(int _id);
        Task<int> Add(FeedConversionRate _model);
        Task<bool> Modify(FeedConversionRate _model);
    }
}
