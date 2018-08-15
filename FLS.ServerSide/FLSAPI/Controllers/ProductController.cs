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
    [Route("api/products")]
    public class ProductController : Controller
    {
        IConfiguration config;
        IProductBusiness busProduct;
        public ProductController(IConfiguration _config, IProductBusiness _busProduct)
        {
            config = _config;
            busProduct = _busProduct;
        }
        [HttpPost("")]
        public async Task<IActionResult> Search([FromBody]PageFilterModel _model)
        {
            var result = await busProduct.GetList(_model);
            return Ok(new ApiResponse<PagedList<ProductModel>>(result));
        }
        [HttpGet("{_id}")]
        public async Task<IActionResult> Get(int _id)
        {
            var result = await busProduct.GetDetail(_id);
            return Ok(new ApiResponse<ProductModel>(result));
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]ProductModel _model)
        {
            var result = await busProduct.Add(_model);
            return Ok(new ApiResponse<int>(result));
        }
        [HttpPut("{_id}/modify")]
        public async Task<IActionResult> Modify(int _id, [FromBody]ProductModel _model)
        {
            var result = await busProduct.Modify(_id, _model);
            return Ok(new ApiResponse<bool>(result));
        }
        [HttpDelete("{_id}/remove")]
        public async Task<IActionResult> Remove(int _id)
        {
            var result = await busProduct.Remove(_id);
            return Ok(new ApiResponse<bool>(result));
        }
    }
}
