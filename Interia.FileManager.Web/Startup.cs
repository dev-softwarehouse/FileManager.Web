using System.Collections.Generic;
using System.Globalization;
using FileSignatures;
using Interia.Core.Contracts.FileProviders.Factory;
using Interia.Core.Contracts.Validations;
using Interia.Core.Validations;
using Interia.FileManager.Web.Services;
using Interia.FileProviders.Factory;
using Interia.FileProviders.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using ConfigurationExtensions = Interia.Core.Extensions.ConfigurationExtensions;

namespace Interia.FileManager.Web
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
			ConfigurationExtensions.Configuration = Configuration;
			services.AddSingleton<IFileFormatInspector>(new FileFormatInspector());
			services.AddSingleton<AllowedExtensionsAttribute>();

			services.AddRazorPages();
			services.AddTransient<IStartupFilter, SettingValidationFilter>();
			services.Configure<FileUploadOptions>(
				Configuration.GetSection(FileUploadOptions.FileUploadSettings));
			services.AddSingleton(resolver =>
				resolver.GetRequiredService<IOptions<FileUploadOptions>>().Value);
			services.AddSingleton<IValidatableObject>(resolver =>
				resolver.GetRequiredService<IOptions<FileUploadOptions>>().Value);
			services.AddSingleton<IFileProviderFactory, FileProviderFactory>();
			services.AddSingleton<IFileService, FileService>();
			services.AddDatabaseDeveloperPageExceptionFilter();
			services.AddLocalization();
			services.Configure<RequestLocalizationOptions>(
				opts =>
				{
					/* your configurations*/
					var supportedCultures = new List<CultureInfo>
					{
						new CultureInfo("en"),
						new CultureInfo("fr")
					};

					opts.DefaultRequestCulture = new RequestCulture(new CultureInfo("en"));
					// Formatting numbers, dates, etc.
					opts.SupportedCultures = supportedCultures;
					// UI strings that we have localized.
					opts.SupportedUICultures = supportedCultures;
				});
			
			services.AddControllersWithViews();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseMigrationsEndPoint();
			}
			else
			{
				app.UseExceptionHandler("/File/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			var localizeOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
			app.UseRequestLocalization(localizeOptions.Value);

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					"default",
					"{controller=File}/{action=Index}/{id?}");
				endpoints.MapRazorPages();
			});
		}
	}
}