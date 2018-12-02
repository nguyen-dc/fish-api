using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FLS.ServerSide.Business.Interfaces;
using FLS.ServerSide.Model.Scope;
using FLS.ServerSide.SharingObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FLS.ServerSide.API.Controllers
{
    [AllowAnonymous]
    [Route("api/reports")]
    public class ReportController : Controller
    {
        IConfiguration config;
        IScopeContext context;
        IReportBusiness busReport;
        public ReportController(
            IConfiguration _config, 
            IScopeContext _scopeContext, 
            IReportBusiness _busReport
        ){
            config = _config;
            context = _scopeContext;
            busReport = _busReport;
        }
        [HttpPost("livestock-history-detail")]
        public async Task<IActionResult> LivestockHistoryDetail([FromBody]ReportLivestockHistoryDetailRequest _request)
        {
            var result = await busReport.LivestockHistoryDetail(_request);
            return Ok(context.WrapResponse(result));
        }
        [HttpPost("feed-conversion-rate")]
        public async Task<IActionResult> FeedConversionRate([FromBody]ReportFeedConversionRateRequest _request)
        {
            var result = await busReport.FeedConversionRate(_request);
            return Ok(context.WrapResponse(result));
        }
        [HttpPost("farmingseason")]
        public async Task<IActionResult> FarmingSeason([FromBody]ReportFarmingSeasonRequest _request)
        {
            var result = await busReport.FarmingSeason(_request);
            return Ok(context.WrapResponse(result));
        }
        [HttpPost("farmingseason-stock-history")]
        public async Task<IActionResult> FarmingSeasonHistoryStock([FromBody]ReportFarmingSeasonHistoryStockRequest _request)
        {
            var result = await busReport.FarmingSeasonHistoryStock(_request);
            return Ok(context.WrapResponse(result));
        }
    }
}
