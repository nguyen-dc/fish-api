﻿using System;
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
    [Route("api/farming-seasons")]
    public class FarmingSeasonController : Controller
    {
        IConfiguration config;
        IScopeContext context;
        IFarmingSeasonBusiness busFarmingSeason;
        public FarmingSeasonController(IConfiguration _config, IScopeContext _scopeContext, IFarmingSeasonBusiness _busFarmingSeason)
        {
            config = _config;
            context = _scopeContext;
            busFarmingSeason = _busFarmingSeason;
        }
        [HttpPost("")]
        public async Task<IActionResult> Search([FromBody]PageFilterModel _model)
        {
            var result = await busFarmingSeason.GetList(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpGet("{_id}")]
        public async Task<IActionResult> Get(int _id)
        {
            var result = await busFarmingSeason.GetDetail(_id);
            return Ok(context.WrapResponse(result));
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]FarmingSeasonModel _model)
        {
            var result = await busFarmingSeason.Add(_model);
            return Ok(context.WrapResponse(result));
        }
        [HttpPut("{_id}/modify")]
        public async Task<IActionResult> Modify(int _id, [FromBody]FarmingSeasonModel _model)
        {
            var result = await busFarmingSeason.Modify(_id, _model);
            return Ok(context.WrapResponse(result));
        }
        [HttpDelete("{_id}/remove")]
        public async Task<IActionResult> Remove(int _id)
        {
            var result = await busFarmingSeason.Remove(_id);
            return Ok(context.WrapResponse(result));
        }
    }
}
