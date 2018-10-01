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
    [Route("api/warehouse-types")]
    public class WarehouseTypeController : Controller
    {
        IConfiguration config;
        IScopeContext context;
        IWarehouseTypeBusiness busWarehouseType;
        public WarehouseTypeController(IConfiguration _config, IScopeContext _scopeContext, IWarehouseTypeBusiness _busWarehouseType)
        {
            config = _config;
            context = _scopeContext;
            busWarehouseType = _busWarehouseType;
        }
        [HttpPost("")]
        public async Task<IActionResult> Search([FromBody]PageFilterModel _model)
        {
            var result = await busWarehouseType.GetList(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpGet("{_id}")]
        public async Task<IActionResult> Get(int _id)
        {
            var result = await busWarehouseType.GetDetail(_id);
            return Ok(context.WrapResponse(result));
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]WarehouseTypeModel _model)
        {
            var result = await busWarehouseType.Add(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpPut("{_id}/modify")]
        public async Task<IActionResult> Modify(int _id, [FromBody]WarehouseTypeModel _model)
        {
            var result = await busWarehouseType.Modify(_id, _model);
            return Ok(context.WrapResponse(result));
        }
        [HttpDelete("{_id}/remove")]
        public async Task<IActionResult> Remove(int _id)
        {
            var result = await busWarehouseType.Remove(_id);
            return Ok(context.WrapResponse(result));
        }
    }
}
