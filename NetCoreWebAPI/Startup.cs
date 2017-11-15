using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetCoreWebAPI.Entities;
using NetCoreWebAPI.Services;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using NetCoreWebAPI.Middleware;

namespace NetCoreWebAPI
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(o =>
            {
                if (o.SerializerSettings.ContractResolver != null)
                {
                    var castedResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
                    castedResolver.NamingStrategy = null;
                }
            });
            var connectionString = Configuration["connectionStrings:DefaultConnection"];
            services.AddDbContext<CityInfoContext>(o => o.UseSqlServer(connectionString));
            services.AddScoped<ICityInfoRepository, CityInfoRepository>();
            services.AddScoped<ICityRepository, Data.City>();
            services.AddScoped<IPOIRepository, Data.POI>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, CityInfoContext cityInfoContext)
        {
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCustomExceptionHandler();
            }
            else
            {
                app.UseCustomExceptionHandler();
                app.UseExceptionHandler();
            }

            cityInfoContext.EnsureSeedDataForContext();

            app.UseStatusCodePages();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<City, Models.CityWithoutPointsOfInterestDto>();
                cfg.CreateMap<City, Models.CityDto>();
                cfg.CreateMap<PointOfInterest, Models.PointsOfInterestDto>();
                cfg.CreateMap<Models.PointOfInterestCreationDto, PointOfInterest>();
                cfg.CreateMap<Models.PointOfInterestUpdateDto, PointOfInterest>();
                cfg.CreateMap<PointOfInterest, Models.PointOfInterestUpdateDto>();
            });


            app.UseMvc();

            app.UseStatusCodePages();
        }
    }
}
