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
        ILivestockProcessBusiness busLivestockProcess;
        public LivestockProcessController(
            IConfiguration _config, 
            IScopeContext _scopeContext,
            ILivestockProcessBusiness _busLivestockProcess
        ){
            config = _config;
            context = _scopeContext;
            busLivestockProcess = _busLivestockProcess;
        }
        [HttpPost("release")]
        public async Task<IActionResult> ReleaseLivestock([FromBody]ReleaseLivestockModel _model)
        {
            var result = await busLivestockProcess.ReleaseLivestock(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpPost("feed")]
        public async Task<IActionResult> FeedingLivestock([FromBody]FeedingLivestockModel _model)
        {
            var result = await busLivestockProcess.FeedingLivestock(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpPost("cure")]
        public async Task<IActionResult> CuringLivestock([FromBody]FeedingLivestockModel _model)
        {
            var result = await busLivestockProcess.CuringLivestock(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpPost("fcr-check")]
        public async Task<IActionResult> FCRCheck([FromBody]FCRCheckModel _model)
        {
            var result = await busLivestockProcess.FCRCheck(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpPost("deadstocks/collect")]
        public async Task<IActionResult> CollectDeadstock([FromBody]CollectDeadstockRequest _model)
        {
            var result = await busLivestockProcess.CollectDeadstock(_model);
            return Ok(context.WrapResponse(result));
        }
    }
}
