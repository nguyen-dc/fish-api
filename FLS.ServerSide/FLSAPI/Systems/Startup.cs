using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FLS.ServerSide.API.Systems;
using FLS.ServerSide.Business.Biz;
using FLS.ServerSide.Business.Interfaces;
using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.EFCore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FLS.ServerSide.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // -------- Camel Json Response Format --------
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var serializer = JsonSerializer.Create(settings);
            services.Add(new ServiceDescriptor(typeof(JsonSerializer),
                        provider => serializer,
                        ServiceLifetime.Transient));
            // -------- MVC --------
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            });
            // -------- Database Context --------
            services.AddDbContext<FLSDbContext>(
                options => options.UseMySql
                (
                    Configuration.GetConnectionString("MySQLDB")
                )
            );
            // -------- AutoMapper ---------
            services.AddAutoMapper(x => x.AddProfile(new MappingProfiles()));

            // -------- Singleton & Scoped --------
            services.AddSingleton(Configuration);
            // -- Businesses
            services.AddScoped<ICustomerBusiness, CustomerBusiness>();
            services.AddScoped<IFarmingSeasonBusiness, FarmingSeasonBusiness>();
            services.AddScoped<IFarmRegionBusiness, FarmRegionBusiness>();
            services.AddScoped<IFishPondBusiness, FishPondBusiness>();
            services.AddScoped<IProductBusiness, ProductBusiness>();
            services.AddScoped<IProductGroupBusiness, ProductGroupBusiness>();
            services.AddScoped<IProductSubgroupBusiness, ProductSubgroupBusiness>();
            services.AddScoped<IProductUnitBusiness, ProductUnitBusiness>();
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
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IExpenditureDocketService, ExpenditureDocketService>();
            services.AddScoped<IExpenditureDocketDetailService, ExpenditureDocketDetailService>();
            services.AddScoped<IFarmingSeasonService, FarmingSeasonService>();
            services.AddScoped<IFarmRegionService, FarmRegionService>();
            services.AddScoped<IFishPondService, FishPondService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductGroupService, ProductGroupService>();
            services.AddScoped<IProductSubgroupService, ProductSubgroupService>();
            services.AddScoped<IProductUnitService, ProductUnitService>();
            services.AddScoped<IExpenditureDocketTypeService, ExpenditureDocketTypeService>();
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
