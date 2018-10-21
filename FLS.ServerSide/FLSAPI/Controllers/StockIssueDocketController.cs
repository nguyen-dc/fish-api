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
    [Route("api/stock-issue-dockets")]
    public class StockIssueDocketController : Controller
    {
        IConfiguration config;
        IScopeContext context;
        IStockIssueDocketBusiness busStockIssueDocket;
        public StockIssueDocketController(IConfiguration _config, IScopeContext _scopeContext, IStockIssueDocketBusiness _busStockIssueDocket)
        {
            config = _config;
            context = _scopeContext;
            busStockIssueDocket = _busStockIssueDocket;
        }
        [HttpPost("")]
        public async Task<IActionResult> Search([FromBody]PageFilterModel _model)
        {
            var result = await busStockIssueDocket.GetList(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpGet("{_id}")]
        public async Task<IActionResult> Get(int _id)
        {
            var result = await busStockIssueDocket.GetDetail(_id);
            return Ok(context.WrapResponse(result));
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]ExportStockModel _model)
        {
            var result = await busStockIssueDocket.Add(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpPut("{_id}/modify")]
        public async Task<IActionResult> Modify(int _id, [FromBody]StockIssueDocketModel _model)
        {
            var result = await busStockIssueDocket.Modify(_id, _model);
            return Ok(context.WrapResponse(result));
        }
        [HttpDelete("{_id}/remove")]
        public async Task<IActionResult> Remove(int _id)
        {
            var result = await busStockIssueDocket.Remove(_id);
            return Ok(context.WrapResponse(result));
        }
    }
}
