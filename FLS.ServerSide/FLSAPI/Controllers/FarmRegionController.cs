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
    [Route("api/farm-regions")]
    public class FarmRegionController : Controller
    {
        IConfiguration config;
        IScopeContext context;
        IFarmRegionBusiness busFarmRegion;
        public FarmRegionController(
            IConfiguration _config,
            IScopeContext _scopeContext,
            IFarmRegionBusiness _busFarmRegion)
        {
            config = _config;
            context = _scopeContext;
            busFarmRegion = _busFarmRegion;
        }
        [HttpPost("")]
        public async Task<IActionResult> Search([FromBody]PageFilterModel _model)
        {
            var result = await busFarmRegion.GetList(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpGet("{_id}")]
        public async Task<IActionResult> Get(int _id)
        {
            var result = await busFarmRegion.GetDetail(_id);
            return Ok(context.WrapResponse(result));
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]FarmRegionModel _model)
        {
            var result = await busFarmRegion.Add(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpPut("{_id}/modify")]
        public async Task<IActionResult> Modify(int _id, [FromBody]FarmRegionModel _model)
        {
            var result = await busFarmRegion.Modify(_id, _model);
            return Ok(context.WrapResponse(result));
        }
        [HttpDelete("{_id}/remove")]
        public async Task<IActionResult> Remove(int _id)
        {
            var result = await busFarmRegion.Remove(_id);
            return Ok(context.WrapResponse(result));
        }
    }
}
