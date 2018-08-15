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
    [Route("api/tax-percents")]
    public class TaxPercentController : Controller
    {
        IConfiguration config;
        ITaxPercentBusiness busTaxPercent;
        public TaxPercentController(IConfiguration _config, ITaxPercentBusiness _busTaxPercent)
        {
            config = _config;
            busTaxPercent = _busTaxPercent;
        }
        [HttpPost("")]
        public async Task<IActionResult> Search([FromBody]PageFilterModel _model)
        {
            var result = await busTaxPercent.GetList(_model);
            return Ok(new ApiResponse<PagedList<TaxPercentModel>>(result));
        }
        [HttpGet("{_id}")]
        public async Task<IActionResult> Get(int _id)
        {
            var result = await busTaxPercent.GetDetail(_id);
            return Ok(new ApiResponse<TaxPercentModel>(result));
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]TaxPercentModel _model)
        {
            var result = await busTaxPercent.Add(_model);
            return Ok(new ApiResponse<int>(result));
        }
        [HttpPut("{_id}/modify")]
        public async Task<IActionResult> Modify(int _id, [FromBody]TaxPercentModel _model)
        {
            var result = await busTaxPercent.Modify(_id, _model);
            return Ok(new ApiResponse<bool>(result));
        }
        [HttpDelete("{_id}/remove")]
        public async Task<IActionResult> Remove(int _id)
        {
            var result = await busTaxPercent.Remove(_id);
            return Ok(new ApiResponse<bool>(result));
        }
    }
}
