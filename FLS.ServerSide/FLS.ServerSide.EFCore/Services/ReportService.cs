using FLS.ServerSide.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FLS.ServerSide.SharingObject;
using FLS.ServerSide.Model.Scope;
using AutoMapper;

namespace FLS.ServerSide.EFCore.Services
{
    public class ReportService : EFCoreServiceBase, IReportService
    {
        private static FLSDbContext context;
        private static IScopeContext scopeContext;
        private readonly IMapper iMapper;
        public ReportService(
            FLSDbContext _context, 
            IScopeContext _scopeContext,
            IMapper _iMapper) : base(_context, _scopeContext)
        {
            context = _context;
            scopeContext = _scopeContext;
            iMapper = _iMapper;
        }

        public async Task<List<ReportLivestockHistoryDetail>> LivestockHistoryDetail(ReportLivestockHistoryDetailRequest _request)
        {
            var __params = new
            {
                farmingSeasonId = _request.FarmingSeasonId,
                fromDate = _request.FromDate,
                toDate = _request.ToDate
            };
            var details = await CallStored<ReportLivestockHistoryDetail>("SP_Livestock_History_Detail", __params).ToListAsync();
            return details;
        }
        public async Task<List<ReportFeedConversionRate>> FeedConversionRate(ReportFeedConversionRateRequest _request)
        {
            var __params = new
            {
                farmingSeasonId = _request.FarmingSeasonId
            };
            var details = await CallStored<ReportFeedConversionRate>("SP_Report_FeedConversionRate", __params).ToListAsync();
            return details;
        }
        public async Task<List<ReportFarmingSeason>> FarmingSeason(ReportFarmingSeasonRequest _request)
        {
            var __params = new
            {
                farmRegionId = _request.FarmRegionId,
                fishPondId = _request.FishPondId,
                fromDate = _request.FromDate,
                toDate = _request.ToDate
            };
            var details = await CallStored<ReportFarmingSeason>("SP_Report_FarmingSeason", __params).ToListAsync();
            return details;
        }
        public async Task<List<ReportFarmingSeasonHistoryStock>> FarmingSeasonHistoryStock(ReportFarmingSeasonHistoryStockRequest _request)
        {
            var __params = new
            {
                farmingSeasonId = _request.FarmingSeasonId,
                productGroupId = _request.ProductGroupId,
                productSubgroupId = _request.ProductSubgroupId,
                productId = _request.ProductId
    };
            var details = await CallStored<ReportFarmingSeasonHistoryStock>("SP_Report_FarmingSeason_HistoryStock", __params).ToListAsync();
            return details;
        }
    }
}
