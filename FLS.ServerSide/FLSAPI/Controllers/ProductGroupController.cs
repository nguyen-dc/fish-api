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
    [Route("api/product-groups")]
    public class ProductGroupController : Controller
    {
        IConfiguration config;
        IScopeContext context;
        IProductGroupBusiness busProductGroup;
        public ProductGroupController(IConfiguration _config, IScopeContext _scopeContext, IProductGroupBusiness _busProductGroup)
        {
            config = _config;
            context = _scopeContext;
            busProductGroup = _busProductGroup;
        }
        [HttpPost("")]
        public async Task<IActionResult> Search([FromBody]PageFilterModel _model)
        {
            var result = await busProductGroup.GetList(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpGet("{_id}")]
        public async Task<IActionResult> Get(int _id)
        {
            var result = await busProductGroup.GetDetail(_id);
            return Ok(context.WrapResponse(result));
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]ProductGroupModel _model)
        {
            var result = await busProductGroup.Add(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpPut("{_id}/modify")]
        public async Task<IActionResult> Modify(int _id, [FromBody]ProductGroupModel _model)
        {
            var result = await busProductGroup.Modify(_id, _model);
            return Ok(context.WrapResponse(result));
        }
        [HttpDelete("{_id}/remove")]
        public async Task<IActionResult> Remove(int _id)
        {
            var result = await busProductGroup.Remove(_id);
            return Ok(context.WrapResponse(result));
        }
        [HttpPost("{_id}/product-subgroups")]
        public async Task<IActionResult> ListSubgroup(int _id, [FromBody]PageFilterModel _model)
        {
            var result = await busProductGroup.GetSubgroups(_id, _model);
            return Ok(context.WrapResponse(result));
        }
        [HttpPost("{_id}/products")]
        public async Task<IActionResult> ListProduct(int _id, [FromBody]PageFilterModel _model)
        {
            var result = await busProductGroup.GetProducts(_id, _model);
            return Ok(context.WrapResponse(result));
        }
    }
}
