using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Interfaces
{
    public interface IReportBusiness
    {
        Task<List<ReportLivestockHistoryDetail>> LivestockHistoryDetail(ReportLivestockHistoryDetailRequest _request);
        Task<List<ReportFeedConversionRate>> FeedConversionRate(ReportFeedConversionRateRequest _request);
        Task<List<ReportFarmingSeason>> FarmingSeason(ReportFarmingSeasonRequest _request);
        Task<List<ReportFarmingSeasonHistoryStock>> FarmingSeasonHistoryStock(ReportFarmingSeasonHistoryStockRequest _request);
    }
}
