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
    [Route("api/connect")]
    public class ConnectController : Controller
    {
        IScopeContext context;
        public ConnectController(IScopeContext _scopeContext)
        {
            context = _scopeContext;
        }
        [HttpGet]
        public async Task<IActionResult> Connect()
        {
            return Ok(context.WrapResponse("Đã kết nối"));
        }
    }
}
