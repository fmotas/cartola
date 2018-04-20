using System;
using System.Data.SqlClient;
using System.IO;
using CartolaUI.Data;
using CartolaUI.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ServiceStack.OrmLite;

namespace CartolaUI
{
	public class Startup
	{
		private readonly IOptions<MyConfig> config;

		public Startup(IConfiguration configuration, IOptions<MyConfig> config)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Environment.CurrentDirectory)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

			this.config = config;

			Configuration = builder.Build();
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();

			// Add functionality to inject IOptions<T>
			services.AddOptions();

			// Add our Config object so it can be injected
			services.Configure<MyConfig>(Configuration.GetSection("MyConfig"));
			 services.AddDbContext<Brasileirao2018DbContext>(options => options.UseSqlServer(Configuration.GetSection("MyConfig").GetConnectionString("Brasileirao2018")));


		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
			}

			app.UseStaticFiles();

			app.UseMvc();
		}
		
		public class MyConfig
		{
			public Logging Logging { get; set; }
			public Connectionstrings ConnectionStrings { get; set; }
		}

		public class Logging
		{
			public bool IncludeScopes { get; set; }
			public Loglevel LogLevel { get; set; }
		}

		public class Loglevel
		{
			public string Default { get; set; }
		}

		public class Connectionstrings
		{
			public string Brasileirao2018 { get; set; }
		}

	}
}
