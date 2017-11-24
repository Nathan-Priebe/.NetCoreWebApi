using System;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetCoreWebAPI.Entities;
using NetCoreWebAPI.Filter;
using NetCoreWebAPI.Services;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using NetCoreWebAPI.Middleware;
using Swashbuckle.AspNetCore.Swagger;
using Cities = NetCoreWebAPI.Services.Cities;

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
            services.AddScoped<ICityRepository, Cities>();
            services.AddScoped<IPOIRepository, Poi>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title = "Cities API", Version = "v1"});
            });
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidateModelAttribute));
            }).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PoiCreationDtoValidator>()).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<POIUpdateDTOValidator>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, CityInfoContext cityInfoContext)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cities API V1");
            });

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
                cfg.CreateMap<Cities, Models.CityWithoutPointsOfInterestDto>();
                cfg.CreateMap<Cities, Models.CityDto>();
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
