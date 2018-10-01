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
    [Route("api/product-units")]
    public class ProductUnitController : Controller
    {
        IConfiguration config;
        IScopeContext context;
        IProductUnitBusiness busProductUnit;
        public ProductUnitController(IConfiguration _config, IScopeContext _scopeContext, IProductUnitBusiness _busProductUnit)
        {
            config = _config;
            context = _scopeContext;
            busProductUnit = _busProductUnit;
        }
        [HttpPost("")]
        public async Task<IActionResult> Search([FromBody]PageFilterModel _model)
        {
            var result = await busProductUnit.GetList(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpGet("{_id}")]
        public async Task<IActionResult> Get(int _id)
        {
            var result = await busProductUnit.GetDetail(_id);
            return Ok(context.WrapResponse(result));
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]ProductUnitModel _model)
        {
            var result = await busProductUnit.Add(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpPut("{_id}/modify")]
        public async Task<IActionResult> Modify(int _id, [FromBody]ProductUnitModel _model)
        {
            var result = await busProductUnit.Modify(_id, _model);
            return Ok(context.WrapResponse(result));
        }
        [HttpDelete("{_id}/remove")]
        public async Task<IActionResult> Remove(int _id)
        {
            var result = await busProductUnit.Remove(_id);
            return Ok(context.WrapResponse(result));
        }
    }
}
