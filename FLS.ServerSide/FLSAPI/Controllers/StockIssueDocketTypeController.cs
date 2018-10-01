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
    [Route("api/stock-issue-docket-types")]
    public class StockIssueDocketTypeController : Controller
    {
        IConfiguration config;
        IScopeContext context;
        IStockIssueDocketTypeBusiness busStockIssueDocketType;
        public StockIssueDocketTypeController(IConfiguration _config, IScopeContext _scopeContext, IStockIssueDocketTypeBusiness _busStockIssueDocketType)
        {
            config = _config;
            context = _scopeContext;
            busStockIssueDocketType = _busStockIssueDocketType;
        }
        [HttpPost("")]
        public async Task<IActionResult> Search([FromBody]PageFilterModel _model)
        {
            var result = await busStockIssueDocketType.GetList(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpGet("{_id}")]
        public async Task<IActionResult> Get(int _id)
        {
            var result = await busStockIssueDocketType.GetDetail(_id);
            return Ok(context.WrapResponse(result));
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]StockIssueDocketTypeModel _model)
        {
            var result = await busStockIssueDocketType.Add(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpPut("{_id}/modify")]
        public async Task<IActionResult> Modify(int _id, [FromBody]StockIssueDocketTypeModel _model)
        {
            var result = await busStockIssueDocketType.Modify(_id, _model);
            return Ok(context.WrapResponse(result));
        }
        [HttpDelete("{_id}/remove")]
        public async Task<IActionResult> Remove(int _id)
        {
            var result = await busStockIssueDocketType.Remove(_id);
            return Ok(context.WrapResponse(result));
        }
    }
}
