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
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        IConfiguration config;
        ICustomerBusiness busCustomer;
        public CustomerController(IConfiguration _config, ICustomerBusiness _busCustomer)
        {
            config = _config;
            busCustomer = _busCustomer;
        }
        [HttpPost("")]
        public async Task<IActionResult> Search([FromBody]PageFilterModel _model)
        {
            var result = await busCustomer.GetList(_model);
            return Ok(new ApiResponse<PagedList<CustomerModel>>(result));
        }
        [HttpGet("{_id}")]
        public async Task<IActionResult> Get(int _id)
        {
            var result = await busCustomer.GetDetail(_id);
            return Ok(new ApiResponse<CustomerModel>(result));
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]CustomerModel _model)
        {
            var result = await busCustomer.Add(_model);
            return Ok(new ApiResponse<int>(result));
        }
        [HttpPut("{_id}/modify")]
        public async Task<IActionResult> Modify(int _id, [FromBody]CustomerModel _model)
        {
            var result = await busCustomer.Modify(_id, _model);
            return Ok(new ApiResponse<bool>(result));
        }
        [HttpDelete("{_id}/remove")]
        public async Task<IActionResult> Remove(int _id)
        {
            var result = await busCustomer.Remove(_id);
            return Ok(new ApiResponse<bool>(result));
        }
    }
}
