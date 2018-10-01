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
    [Route("api/fish-ponds")]
    public class FishPondController : Controller
    {
        IConfiguration config;
        IScopeContext context;
        IFishPondBusiness busFishPond;
        public FishPondController(IConfiguration _config, IScopeContext _scopeContext, IFishPondBusiness _busFishPond)
        {
            config = _config;
            context = _scopeContext;
            busFishPond = _busFishPond;
        }
        [HttpPost("")]
        public async Task<IActionResult> Search([FromBody]PageFilterModel _model)
        {
            var result = await busFishPond.GetList(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpGet("{_id}")]
        public async Task<IActionResult> Get(int _id)
        {
            var result = await busFishPond.GetDetail(_id);
            return Ok(context.WrapResponse(result));
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]FishPondModel _model)
        {
            var result = await busFishPond.Add(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpPut("{_id}/modify")]
        public async Task<IActionResult> Modify(int _id, [FromBody]FishPondModel _model)
        {
            var result = await busFishPond.Modify(_id, _model);
            return Ok(context.WrapResponse(result));
        }
        [HttpDelete("{_id}/remove")]
        public async Task<IActionResult> Remove(int _id)
        {
            var result = await busFishPond.Remove(_id);
            return Ok(context.WrapResponse(result));
        }
    }
}
