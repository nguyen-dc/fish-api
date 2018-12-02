using AutoMapper;
using FLS.ServerSide.Business.Interfaces;
using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.EFCore.Services;
using FLS.ServerSide.SharingObject;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FLS.ServerSide.Business.Biz
{
    public class ReportBusiness : IReportBusiness
    {
        private readonly IReportService svcReport;
        private readonly IMapper iMapper;
        public ReportBusiness(IReportService _svcReport, IMapper _iMapper)
        {
            svcReport = _svcReport;
            iMapper = _iMapper;
        }

        public async Task<List<ReportFarmingSeason>> FarmingSeason(ReportFarmingSeasonRequest _request)
        {
            return await svcReport.FarmingSeason(_request);
        }

        public async Task<List<ReportFarmingSeasonHistoryStock>> FarmingSeasonHistoryStock(ReportFarmingSeasonHistoryStockRequest _request)
        {
            return await svcReport.FarmingSeasonHistoryStock(_request);
        }

        public async Task<List<ReportFeedConversionRate>> FeedConversionRate(ReportFeedConversionRateRequest _request)
        {
            return await svcReport.FeedConversionRate(_request);
        }

        public async Task<List<ReportLivestockHistoryDetail>> LivestockHistoryDetail(ReportLivestockHistoryDetailRequest _request)
        {
            return await svcReport.LivestockHistoryDetail(_request);
        }
    }
}
