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
    [Route("api/expenditure-docket-types")]
    public class ExpenditureDocketTypeController : Controller
    {
        IConfiguration config;
        IExpenditureDocketTypeBusiness busExpenditureDocketType;
        public ExpenditureDocketTypeController(IConfiguration _config, IExpenditureDocketTypeBusiness _busExpenditureDocketType)
        {
            config = _config;
            busExpenditureDocketType = _busExpenditureDocketType;
        }
        [HttpPost("")]
        public async Task<IActionResult> Search([FromBody]PageFilterModel _model)
        {
            var result = await busExpenditureDocketType.GetList(_model);
            return Ok(new ApiResponse<PagedList<ExpenditureDocketTypeModel>>(result));
        }
        [HttpGet("{_id}")]
        public async Task<IActionResult> Get(int _id)
        {
            var result = await busExpenditureDocketType.GetDetail(_id);
            return Ok(new ApiResponse<ExpenditureDocketTypeModel>(result));
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]ExpenditureDocketTypeModel _model)
        {
            var result = await busExpenditureDocketType.Add(_model);
            return Ok(new ApiResponse<int>(result));
        }
        [HttpPut("{_id}/modify")]
        public async Task<IActionResult> Modify(int _id, [FromBody]ExpenditureDocketTypeModel _model)
        {
            var result = await busExpenditureDocketType.Modify(_id, _model);
            return Ok(new ApiResponse<bool>(result));
        }
        [HttpDelete("{_id}/remove")]
        public async Task<IActionResult> Remove(int _id)
        {
            var result = await busExpenditureDocketType.Remove(_id);
            return Ok(new ApiResponse<bool>(result));
        }
    }
}
