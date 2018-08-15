using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FLS.ServerSide.Business.Interfaces;
using FLS.ServerSide.SharingObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FLS.ServerSide.API.Controllers
{
    [AllowAnonymous]
    [Route("api/stock-issue-docket-types")]
    public class StockIssueDocketTypeController : Controller
    {
        IConfiguration config;
        IStockIssueDocketTypeBusiness busStockIssueDocketType;
        public StockIssueDocketTypeController(IConfiguration _config, IStockIssueDocketTypeBusiness _busStockIssueDocketType)
        {
            config = _config;
            busStockIssueDocketType = _busStockIssueDocketType;
        }
        [HttpPost("")]
        public async Task<IActionResult> Search([FromBody]PageFilterModel _model)
        {
            var result = await busStockIssueDocketType.GetList(_model);
            return Ok(new ApiResponse<PagedList<StockIssueDocketTypeModel>>(result));
        }
        [HttpGet("{_id}")]
        public async Task<IActionResult> Get(int _id)
        {
            var result = await busStockIssueDocketType.GetDetail(_id);
            return Ok(new ApiResponse<StockIssueDocketTypeModel>(result));
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]StockIssueDocketTypeModel _model)
        {
            var result = await busStockIssueDocketType.Add(_model);
            return Ok(new ApiResponse<int>(result));
        }
        [HttpPut("{_id}/modify")]
        public async Task<IActionResult> Modify(int _id, [FromBody]StockIssueDocketTypeModel _model)
        {
            var result = await busStockIssueDocketType.Modify(_id, _model);
            return Ok(new ApiResponse<bool>(result));
        }
        [HttpDelete("{_id}/remove")]
        public async Task<IActionResult> Remove(int _id)
        {
            var result = await busStockIssueDocketType.Remove(_id);
            return Ok(new ApiResponse<bool>(result));
        }
    }
}
