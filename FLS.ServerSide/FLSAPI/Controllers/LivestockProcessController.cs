using FLS.ServerSide.Business.Interfaces;
using FLS.ServerSide.Model.Scope;
using FLS.ServerSide.SharingObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace FLS.ServerSide.API.Controllers
{
    [AllowAnonymous]
    [Route("api/livestock-proceeds")]
    public class LivestockProcessController : Controller
    {
        IConfiguration config;
        IScopeContext context;
        IStockReceiveDocketBusiness busStockReceiveDocket;
        public LivestockProcessController(
            IConfiguration _config, 
            IScopeContext _scopeContext, 
            IStockReceiveDocketBusiness _busStockReceiveDocket
        ){
            config = _config;
            context = _scopeContext;
            busStockReceiveDocket = _busStockReceiveDocket;
        }
        [HttpPost("release")]
        public async Task<IActionResult> ReleaseLivestock([FromBody]ReleaseLivestockModel _model)
        {
            var result = await busStockReceiveDocket.ReleaseLivestock(_model);
            return Ok(context.WrapResponse(result));
        }
    }
}
