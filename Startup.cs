using Locadora.Context;
using Locadora.Interface;
using Locadora.Logica;
using Locadora.Repository;
using Locadora.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Locadora
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
			services.AddDbContext<LocadoraApiContext>(opt => {
				opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
			});

			

			services.AddControllers(options =>
			{
				options.OutputFormatters.RemoveType<SystemTextJsonOutputFormatter>();
				options.OutputFormatters.Add(new SystemTextJsonOutputFormatter(new JsonSerializerOptions(JsonSerializerDefaults.Web)
				{
					ReferenceHandler = ReferenceHandler.Preserve,
				}));
			});

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Locadora", Version = "v1" });
			});

			services.AddTransient<LocadoraApiContext>();

			services.AddTransient<IClienteRepository, ClienteRepository>();
			services.AddTransient<IClienteService, ClienteService>();

			services.AddTransient<IFilmeRepository, FilmeRepository>();
			services.AddTransient<IFilmeService, FilmeService>();

			services.AddTransient<ILocacaoRepository, LocacaoRepository>();
			services.AddTransient<ILocacaoService, LocacaoService>();

			services.AddTransient<ILocacaoItemRepository, LocacaoItemRepository>();
			services.AddTransient<ILocacaoItemService, LocacaoItemService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Locadora v1"));
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
