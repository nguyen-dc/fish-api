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
    [Route("api/stock-receive-dockets")]
    public class StockReceiveDocketController : Controller
    {
        IConfiguration config;
        IScopeContext context;
        IStockReceiveDocketBusiness busStockReceiveDocket;
        public StockReceiveDocketController(IConfiguration _config, IScopeContext _scopeContext, IStockReceiveDocketBusiness _busStockReceiveDocket)
        {
            config = _config;
            context = _scopeContext;
            busStockReceiveDocket = _busStockReceiveDocket;
        }
        [HttpPost("")]
        public async Task<IActionResult> Search([FromBody]PageFilterModel _model)
        {
            var result = await busStockReceiveDocket.GetList(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpGet("{_id}")]
        public async Task<IActionResult> Get(int _id)
        {
            var result = await busStockReceiveDocket.GetDetail(_id);
            return Ok(context.WrapResponse(result));
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]ImportStockModel _model)
        {
            var result = await busStockReceiveDocket.Add(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpPut("{_id}/modify")]
        public async Task<IActionResult> Modify(int _id, [FromBody]StockReceiveDocketModel _model)
        {
            var result = await busStockReceiveDocket.Modify(_id, _model);
            return Ok(context.WrapResponse(result));
        }
        [HttpDelete("{_id}/remove")]
        public async Task<IActionResult> Remove(int _id)
        {
            var result = await busStockReceiveDocket.Remove(_id);
            return Ok(context.WrapResponse(result));
        }
    }
}
