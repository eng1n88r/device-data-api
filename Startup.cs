using DeviceDataApi.Contracts;
using DeviceDataApi.DataProcessors;
using DeviceDataApi.DataProcessors.Interfaces;
using DeviceDataApi.Repositories;
using DeviceDataApi.Repositories.Interfaces;
using DeviceDataApi.Services;
using DeviceDataApi.Services.Interfaces;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Phema.Caching;

namespace DeviceDataApi
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
			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "DeviceDataApi", Version = "v1" });
			});

			services.AddDistributedCache()
				.AddDistributedMemoryCache();

			services.AddScoped<IRepository, InMemoryDistributedRepository>();
			services.AddScoped<IDeviceDataProcessor<DeviceTypeAData>, DeviceTypeADataProcessor>();
			services.AddScoped<IDeviceDataProcessor<DeviceTypeBData>, DeviceTypeBDataProcessor>();
			services.AddScoped<IDataProcessingService, DataProcessingService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DeviceDataApi v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
