using FLS.ServerSide.Business.Biz;
using FLS.ServerSide.Business.Interfaces;
using FLS.ServerSide.EFCore.Services;
using FLS.ServerSide.Model.Scope;
using Microsoft.Extensions.DependencyInjection;

namespace FLS.ServerSide.API.Systems
{
    public static class ConfigureStartupServices
    {
        public static void AddScopeContext(this IServiceCollection services)
        {
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IScopeContext, ScopeContext>();
            // -- Businesses
            services.AddScoped<ICustomerBusiness, CustomerBusiness>();
            services.AddScoped<IFarmingSeasonBusiness, FarmingSeasonBusiness>();
            services.AddScoped<IFarmRegionBusiness, FarmRegionBusiness>();
            services.AddScoped<IFishPondBusiness, FishPondBusiness>();
            services.AddScoped<ILivestockProcessBusiness, LivestockProcessBusiness>();
            services.AddScoped<IProductBusiness, ProductBusiness>();
            services.AddScoped<IProductGroupBusiness, ProductGroupBusiness>();
            services.AddScoped<IProductSubgroupBusiness, ProductSubgroupBusiness>();
            services.AddScoped<IProductUnitBusiness, ProductUnitBusiness>();
            services.AddScoped<IProductUnitProductBusiness, ProductUnitProductBusiness>();
            services.AddScoped<IExpenditureDocketTypeBusiness, ExpenditureDocketTypeBusiness>();
            services.AddScoped<IStockIssueDocketBusiness, StockIssueDocketBusiness>();
            services.AddScoped<IStockIssueDocketTypeBusiness, StockIssueDocketTypeBusiness>();
            services.AddScoped<IStockReceiveDocketBusiness, StockReceiveDocketBusiness>();
            services.AddScoped<IStockReceiveDocketTypeBusiness, StockReceiveDocketTypeBusiness>();
            services.AddScoped<ISupplierBusiness, SupplierBusiness>();
            services.AddScoped<ISupplierBranchBusiness, SupplierBranchBusiness>();
            services.AddScoped<ITaxPercentBusiness, TaxPercentBusiness>();
            services.AddScoped<IWarehouseBusiness, WarehouseBusiness>();
            services.AddScoped<IWarehouseTypeBusiness, WarehouseTypeBusiness>();
            // -- Services
            services.AddScoped<ICurrentInStockService, CurrentInStockService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IExpenditureDocketService, ExpenditureDocketService>();
            services.AddScoped<IExpenditureDocketDetailService, ExpenditureDocketDetailService>();
            services.AddScoped<IFarmingSeasonService, FarmingSeasonService>();
            services.AddScoped<IFarmingSeasonHistoryService, FarmingSeasonHistoryService>();
            services.AddScoped<IFarmRegionService, FarmRegionService>();
            services.AddScoped<IFeedConversionRateService, FeedConversionRateService>();
            services.AddScoped<IFishPondService, FishPondService>();
            services.AddScoped<ILivestockHistoryDetailService, LivestockHistoryDetailService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductGroupService, ProductGroupService>();
            services.AddScoped<IProductSubgroupService, ProductSubgroupService>();
            services.AddScoped<IProductUnitService, ProductUnitService>();
            services.AddScoped<IProductUnitProductService, ProductUnitProductService>();
            services.AddScoped<IExpenditureDocketTypeService, ExpenditureDocketTypeService>();
            services.AddScoped<IStockHistoryDetailService, StockHistoryDetailService>();
            services.AddScoped<IStockIssueDocketService, StockIssueDocketService>();
            services.AddScoped<IStockIssueDocketDetailService, StockIssueDocketDetailService>();
            services.AddScoped<IStockIssueDocketTypeService, StockIssueDocketTypeService>();
            services.AddScoped<IStockReceiveDocketService, StockReceiveDocketService>();
            services.AddScoped<IStockReceiveDocketDetailService, StockReceiveDocketDetailService>();
            services.AddScoped<IStockReceiveDocketTypeService, StockReceiveDocketTypeService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<ISupplierBranchService, SupplierBranchService>();
            services.AddScoped<ITaxPercentService, TaxPercentService>();
            services.AddScoped<IWarehouseService, WarehouseService>();
            services.AddScoped<IWarehouseTypeService, WarehouseTypeService>();
        }
    }
}
