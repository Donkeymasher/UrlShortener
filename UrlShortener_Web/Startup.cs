using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using UrlShortener.Services.Data.Helpers;
using UrlShortener.Services.Data.Configuration;
using UrlShortener.Services.Data.Factories;

namespace UrlShortener
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions<ConnectionStrings>().Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection("ConnectionStrings").Bind(settings);
            });

            services.AddSingleton<ContextFactory>().Configure<IOptions<ConnectionStrings>>((configuration) =>
            {
                new ContextFactory(configuration);
            });

            services.AddScoped<IRepositoryAccessHandler, RepositoryAccessHandler>().Configure<IOptions<ConnectionStrings>>((configuration) =>
            {
                new RepositoryAccessHandler(new ContextFactory(configuration));
            });

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseExceptionHandler("/Home/Index");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "shortenUrl",
                    pattern: "Home/{id}",
                    defaults: new { controller = "Home", action = "ShortenUrl" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "/{shortenedUrlToken?}",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
