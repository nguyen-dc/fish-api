using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FLS.ServerSide.Business.Interfaces;
using FLS.ServerSide.SharingObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FLS.ServerSide.API.Controllers
{
    [AllowAnonymous]
    [Route("api/suppliers")]
    public class SupplierController : Controller
    {
        IConfiguration config;
        ISupplierBusiness busSupplier;
        public SupplierController(IConfiguration _config, ISupplierBusiness _busSupplier)
        {
            config = _config;
            busSupplier = _busSupplier;
        }
        [HttpPost("")]
        public async Task<IActionResult> Search([FromBody]PageFilterModel _model)
        {
            var result = await busSupplier.GetList(_model);
            return Ok(new ApiResponse<PagedList<SupplierModel>>(result));
        }
        [HttpGet("{_id}")]
        public async Task<IActionResult> Get(int _id)
        {
            var result = await busSupplier.GetDetail(_id);
            return Ok(new ApiResponse<SupplierModel>(result));
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]SupplierModel _model)
        {
            var result = await busSupplier.Add(_model);
            return Ok(new ApiResponse<int>(result));
        }
        [HttpPut("{_id}/modify")]
        public async Task<IActionResult> Modify(int _id, [FromBody]SupplierModel _model)
        {
            var result = await busSupplier.Modify(_id, _model);
            return Ok(new ApiResponse<bool>(result));
        }
        [HttpDelete("{_id}/remove")]
        public async Task<IActionResult> Remove(int _id)
        {
            var result = await busSupplier.Remove(_id);
            return Ok(new ApiResponse<bool>(result));
        }
        [HttpPost("{_id}/supplier-branchs")]
        public async Task<IActionResult> ListSupplierBranch(int _id, [FromBody]PageFilterModel _model)
        {
            var result = await busSupplier.GetBranchs(_id, _model);
            return Ok(new ApiResponse<PagedList<SupplierBranchModel>>(result));
        }
    }
}
