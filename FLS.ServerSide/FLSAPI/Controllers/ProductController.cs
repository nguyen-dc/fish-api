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
    [Route("api/products")]
    public class ProductController : Controller
    {
        IConfiguration config;
        IScopeContext context;
        IProductBusiness busProduct;
        IProductUnitProductBusiness busProductUnitProduct;
        public ProductController(
            IConfiguration _config, 
            IScopeContext _scopeContext, 
            IProductBusiness _busProduct,
            IProductUnitProductBusiness _busProductUnitProduct)
        {
            config = _config;
            context = _scopeContext;
            busProduct = _busProduct;
            busProductUnitProduct = _busProductUnitProduct;
        }
        [HttpPost("")]
        public async Task<IActionResult> Search([FromBody]PageFilterModel _model)
        {
            var result = await busProduct.GetStockList(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpPost("livestocks")]
        public async Task<IActionResult> SearchLivestocks([FromBody]PageFilterModel _model)
        {
            var result = await busProduct.GetLivestockList(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpGet("{_productId}")]
        public async Task<IActionResult> Get(int _productId)
        {
            var result = await busProduct.GetDetail(_productId);
            return Ok(context.WrapResponse(result));
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]ProductModel _model)
        {
            var result = await busProduct.Add(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpPut("{_productId}/modify")]
        public async Task<IActionResult> Modify(int _productId, [FromBody]ProductModel _model)
        {
            var result = await busProduct.Modify(_productId, _model);
            return Ok(context.WrapResponse(result));
        }
        [HttpDelete("{_productId}/remove")]
        public async Task<IActionResult> Remove(int _productId)
        {
            var result = await busProduct.Remove(_productId);
            return Ok(context.WrapResponse(result));
        }
        // Product Unit Product
        [HttpPost("{_productId}/units/add")]
        public async Task<IActionResult> AddUnit(int _productId, [FromBody]ProductUnitProductModel _model)
        {
            _model.ProductId = _productId;
            var result = await busProductUnitProduct.Add(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpPut("units/{_unitId}/modify")]
        public async Task<IActionResult> ModifyUnit(int _unitId, [FromBody]ProductUnitProductModel _model)
        {
            var result = await busProductUnitProduct.Modify(_unitId, _model);
            return Ok(context.WrapResponse(result));
        }
        [HttpDelete("units/{_unitId}/remove")]
        public async Task<IActionResult> RemoveUnit(int _unitId)
        {
            var result = await busProductUnitProduct.Remove(_unitId);
            return Ok(context.WrapResponse(result));
        }
    }
}
