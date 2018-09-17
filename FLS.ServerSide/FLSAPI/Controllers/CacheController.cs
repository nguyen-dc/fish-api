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
    [Route("api/cache")]
    public class CacheController : Controller
    {
        readonly IConfiguration config;
        readonly IFarmRegionBusiness busFarmRegion;
        readonly IFishPondBusiness busFishPond;
        readonly IProductGroupBusiness busProductGroup;
        readonly IProductSubgroupBusiness busProductSubgroup;
        readonly IProductUnitBusiness busProductUnit;
        readonly IExpenditureDocketTypeBusiness busExpenditureDocketType;
        readonly IStockIssueDocketTypeBusiness busStockIssueDocketType;
        readonly IStockReceiveDocketTypeBusiness busStockReceiveDocketType;
        readonly ITaxPercentBusiness busTaxPercent;
        readonly IWarehouseBusiness busWarehouse;
        readonly IWarehouseTypeBusiness busWarehouseType;
        public CacheController(
            IConfiguration _config,
            IFarmRegionBusiness _busFarmRegion,
            IFishPondBusiness _busFishPond,
            IProductGroupBusiness _busProductGroup,
            IProductSubgroupBusiness _busProductSubgroup,
            IProductUnitBusiness _busProductUnit,
            IExpenditureDocketTypeBusiness _busExpenditureDocketType,
            IStockIssueDocketTypeBusiness _busStockIssueDocketType,
            IStockReceiveDocketTypeBusiness _busStockReceiveDocketType,
            ITaxPercentBusiness _busTaxPercent,
            IWarehouseBusiness _busWarehouse, 
            IWarehouseTypeBusiness _busWarehouseType
            )
        {
            config = _config;
            busFarmRegion = _busFarmRegion;
            busFishPond = _busFishPond;
            busProductGroup = _busProductGroup;
            busProductSubgroup = _busProductSubgroup;
            busProductUnit = _busProductUnit;
            busExpenditureDocketType = _busExpenditureDocketType;
            busStockIssueDocketType = _busStockIssueDocketType;
            busStockReceiveDocketType = _busStockReceiveDocketType;
            busTaxPercent = _busTaxPercent;
            busWarehouse = _busWarehouse;
            busWarehouseType = _busWarehouseType;
        }
        [HttpGet("farm-regions")]
        public async Task<IActionResult> FarmRegions()
        {
            var result = await busFarmRegion.GetCache();
            return Ok(new ApiResponse<List<IdNameModel>>(result));
        }
        [HttpGet("fish-ponds")]
        public async Task<IActionResult> FishPonds()
        {
            var result = await busFishPond.GetCache();
            return Ok(new ApiResponse<List<IdNameModel>>(result));
        }
        [HttpGet("product-groups")]
        public async Task<IActionResult> ProductGroups()
        {
            var result = await busProductGroup.GetCache();
            return Ok(new ApiResponse<List<IdNameModel>>(result));
        }
        [HttpGet("product-subgroups")]
        public async Task<IActionResult> ProductSubgroups()
        {
            var result = await busProductSubgroup.GetCache();
            return Ok(new ApiResponse<List<IdNameModel>>(result));
        }
        /// <returns>
        /// check: HasScale
        /// </returns>
        [HttpGet("product-units")]
        public async Task<IActionResult> ProductUnits()
        {
            var result = await busProductUnit.GetCache();
            return Ok(new ApiResponse<List<IdNameModel>>(result));
        }
        [HttpGet("expenditure-docket-types")]
        public async Task<IActionResult> ExpenditureDocketTypes()
        {
            var result = await busExpenditureDocketType.GetCache();
            return Ok(new ApiResponse<List<IdNameModel>>(result));
        }
        /// <returns>
        /// check: ReceiptNeeded
        /// belongId: ReceiptTypeId
        /// </returns>
        [HttpGet("stock-issue-docket-types")]
        public async Task<IActionResult> StockIssueDocketTypes()
        {
            var result = await busStockIssueDocketType.GetCache();
            return Ok(new ApiResponse<List<IdNameModel>>(result));
        }
        /// <returns>
        /// check: PayslipNeeded
        /// belongId: PayslipTypeId
        /// </returns>
        [HttpGet("stock-receive-docket-types")]
        public async Task<IActionResult> StockReceiveDocketTypes()
        {
            var result = await busStockReceiveDocketType.GetCache();
            return Ok(new ApiResponse<List<IdNameModel>>(result));
        }
        [HttpGet("tax-percents")]
        public async Task<IActionResult> TaxPercents()
        {
            var result = await busTaxPercent.GetCache();
            return Ok(new ApiResponse<List<IdNameModel>>(result));
        }
        [HttpGet("warehouses")]
        public async Task<IActionResult> Warehouses()
        {
            var result = await busWarehouse.GetCache();
            return Ok(new ApiResponse<List<IdNameModel>>(result));
        }
        [HttpGet("warehouse-types")]
        public async Task<IActionResult> WarehouseTypes()
        {
            var result = await busWarehouseType.GetCache();
            return Ok(new ApiResponse<List<IdNameModel>>(result));
        }
    }
}
