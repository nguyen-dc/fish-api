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
    [Route("api/stock-receive-docket-types")]
    public class StockReceiveDocketTypeController : Controller
    {
        IConfiguration config;
        IScopeContext context;
        IStockReceiveDocketTypeBusiness busStockReceiveDocketType;
        public StockReceiveDocketTypeController(IConfiguration _config, IScopeContext _scopeContext, IStockReceiveDocketTypeBusiness _busStockReceiveDocketType)
        {
            config = _config;
            context = _scopeContext;
            busStockReceiveDocketType = _busStockReceiveDocketType;
        }
        [HttpPost("")]
        public async Task<IActionResult> Search([FromBody]PageFilterModel _model)
        {
            var result = await busStockReceiveDocketType.GetList(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpGet("{_id}")]
        public async Task<IActionResult> Get(int _id)
        {
            var result = await busStockReceiveDocketType.GetDetail(_id);
            return Ok(context.WrapResponse(result));
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]StockReceiveDocketTypeModel _model)
        {
            var result = await busStockReceiveDocketType.Add(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpPut("{_id}/modify")]
        public async Task<IActionResult> Modify(int _id, [FromBody]StockReceiveDocketTypeModel _model)
        {
            var result = await busStockReceiveDocketType.Modify(_id, _model);
            return Ok(context.WrapResponse(result));
        }
        [HttpDelete("{_id}/remove")]
        public async Task<IActionResult> Remove(int _id)
        {
            var result = await busStockReceiveDocketType.Remove(_id);
            return Ok(context.WrapResponse(result));
        }
    }
}
