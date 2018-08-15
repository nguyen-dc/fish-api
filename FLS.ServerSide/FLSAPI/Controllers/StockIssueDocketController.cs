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
    [Route("api/stock-issue-dockets")]
    public class StockIssueDocketController : Controller
    {
        IConfiguration config;
        IStockIssueDocketBusiness busStockIssueDocket;
        public StockIssueDocketController(IConfiguration _config, IStockIssueDocketBusiness _busStockIssueDocket)
        {
            config = _config;
            busStockIssueDocket = _busStockIssueDocket;
        }
        [HttpPost("")]
        public async Task<IActionResult> Search([FromBody]PageFilterModel _model)
        {
            var result = await busStockIssueDocket.GetList(_model);
            return Ok(new ApiResponse<PagedList<StockIssueDocketModel>>(result));
        }
        [HttpGet("{_id}")]
        public async Task<IActionResult> Get(int _id)
        {
            var result = await busStockIssueDocket.GetDetail(_id);
            return Ok(new ApiResponse<StockIssueDocketModel>(result));
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]StockIssueDocketModel _model)
        {
            var result = await busStockIssueDocket.Add(_model);
            return Ok(new ApiResponse<int>(result));
        }
        [HttpPut("{_id}/modify")]
        public async Task<IActionResult> Modify(int _id, [FromBody]StockIssueDocketModel _model)
        {
            var result = await busStockIssueDocket.Modify(_id, _model);
            return Ok(new ApiResponse<bool>(result));
        }
        [HttpDelete("{_id}/remove")]
        public async Task<IActionResult> Remove(int _id)
        {
            var result = await busStockIssueDocket.Remove(_id);
            return Ok(new ApiResponse<bool>(result));
        }
    }
}
