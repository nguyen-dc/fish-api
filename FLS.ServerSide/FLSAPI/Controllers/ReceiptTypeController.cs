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
    [Route("api/receipt-types")]
    public class ReceiptTypeController : Controller
    {
        IConfiguration config;
        IReceiptTypeBusiness busReceiptType;
        public ReceiptTypeController(IConfiguration _config, IReceiptTypeBusiness _busReceiptType)
        {
            config = _config;
            busReceiptType = _busReceiptType;
        }
        [HttpPost("")]
        public async Task<IActionResult> Search([FromBody]PageFilterModel _model)
        {
            var result = await busReceiptType.GetList(_model);
            return Ok(new ApiResponse<PagedList<ExpenditureTypeModel>>(result));
        }
        [HttpGet("{_id}")]
        public async Task<IActionResult> Get(int _id)
        {
            var result = await busReceiptType.GetDetail(_id);
            return Ok(new ApiResponse<ExpenditureTypeModel>(result));
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]ExpenditureTypeModel _model)
        {
            var result = await busReceiptType.Add(_model);
            return Ok(new ApiResponse<int>(result));
        }
        [HttpPut("{_id}/modify")]
        public async Task<IActionResult> Modify(int _id, [FromBody]ExpenditureTypeModel _model)
        {
            var result = await busReceiptType.Modify(_id, _model);
            return Ok(new ApiResponse<bool>(result));
        }
        [HttpDelete("{_id}/remove")]
        public async Task<IActionResult> Remove(int _id)
        {
            var result = await busReceiptType.Remove(_id);
            return Ok(new ApiResponse<bool>(result));
        }
    }
}
