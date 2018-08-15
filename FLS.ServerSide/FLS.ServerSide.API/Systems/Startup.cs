using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FLS.ServerSide.API.Systems;
using FLS.ServerSide.Business.Biz;
using FLS.ServerSide.Business.Interfaces;
using FLS.ServerSide.EFCore.Entities;
using FLS.ServerSide.EFCore.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
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
            services.AddMvc().AddJsonOptions(options => {
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

            // -------- Authentication Bearer --------
            var secretKey = Configuration.GetSection("Auth:Key").Value;
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            string issuer = Configuration.GetSection("Auth:Issuer").Value;
            string audience = Configuration.GetSection("Auth:Audience").Value;
            string cookieName = Configuration.GetSection("Auth:CookieName").Value;
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateLifetime = true
            };
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddJwtBearer(option =>
            {
                option.TokenValidationParameters = tokenValidationParameters;
                option.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Query.TryGetValue("access_token", out StringValues token)
                        )
                        {
                            context.Token = token;
                        }
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        var te = context.Exception;
                        return Task.CompletedTask;
                    }
                };
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Bearer",
                policy => policy
                    .AddAuthenticationSchemes(
                    JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser());
            });
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                );
            });
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new AuthorizeFilter("Bearer"));
            });

            // -------- Singleton & Scoped --------
            services.AddSingleton(Configuration);
            // -- Businesses
            services.AddScoped<IProductGroupBusiness, ProductGroupBusiness>();
            // -- Services
            services.AddScoped<IProductGroupService, ProductGroupService>();
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
