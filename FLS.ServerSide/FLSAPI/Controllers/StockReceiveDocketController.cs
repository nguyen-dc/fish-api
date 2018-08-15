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
    [Route("api/stock-receive-dockets")]
    public class StockReceiveDocketController : Controller
    {
        IConfiguration config;
        IStockReceiveDocketBusiness busStockReceiveDocket;
        public StockReceiveDocketController(IConfiguration _config, IStockReceiveDocketBusiness _busStockReceiveDocket)
        {
            config = _config;
            busStockReceiveDocket = _busStockReceiveDocket;
        }
        [HttpPost("")]
        public async Task<IActionResult> Search([FromBody]PageFilterModel _model)
        {
            var result = await busStockReceiveDocket.GetList(_model);
            return Ok(new ApiResponse<PagedList<StockReceiveDocketModel>>(result));
        }
        [HttpGet("{_id}")]
        public async Task<IActionResult> Get(int _id)
        {
            var result = await busStockReceiveDocket.GetDetail(_id);
            return Ok(new ApiResponse<StockReceiveDocketModel>(result));
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]StockReceiveDocketModel _model)
        {
            var result = await busStockReceiveDocket.Add(_model);
            return Ok(new ApiResponse<int>(result));
        }
        [HttpPut("{_id}/modify")]
        public async Task<IActionResult> Modify(int _id, [FromBody]StockReceiveDocketModel _model)
        {
            var result = await busStockReceiveDocket.Modify(_id, _model);
            return Ok(new ApiResponse<bool>(result));
        }
        [HttpDelete("{_id}/remove")]
        public async Task<IActionResult> Remove(int _id)
        {
            var result = await busStockReceiveDocket.Remove(_id);
            return Ok(new ApiResponse<bool>(result));
        }
    }
}
