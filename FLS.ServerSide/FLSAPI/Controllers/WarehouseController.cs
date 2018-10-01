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
    [Route("api/warehouses")]
    public class WarehouseController : Controller
    {
        IConfiguration config;
        IScopeContext context;
        IWarehouseBusiness busWarehouse;
        public WarehouseController(IConfiguration _config, IScopeContext _scopeContext, IWarehouseBusiness _busWarehouse)
        {
            config = _config;
            context = _scopeContext;
            busWarehouse = _busWarehouse;
        }
        [HttpPost("")]
        public async Task<IActionResult> Search([FromBody]PageFilterModel _model)
        {
            var result = await busWarehouse.GetList(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpGet("{_id}")]
        public async Task<IActionResult> Get(int _id)
        {
            var result = await busWarehouse.GetDetail(_id);
            return Ok(context.WrapResponse(result));
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]WarehouseModel _model)
        {
            var result = await busWarehouse.Add(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpPut("{_id}/modify")]
        public async Task<IActionResult> Modify(int _id, [FromBody]WarehouseModel _model)
        {
            var result = await busWarehouse.Modify(_id, _model);
            return Ok(context.WrapResponse(result));
        }
        [HttpDelete("{_id}/remove")]
        public async Task<IActionResult> Remove(int _id)
        {
            var result = await busWarehouse.Remove(_id);
            return Ok(context.WrapResponse(result));
        }
    }
}
