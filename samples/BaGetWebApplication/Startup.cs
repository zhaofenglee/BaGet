using BaGet;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BaGetWebApplication
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBaGetWebApplication(app =>
            {
                app.AddSqlServerDatabase();
                app.AddFileStorage();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // Add BaGet's endpoints.
                var baget = new BaGetEndpointBuilder();

                baget.MapEndpoints(endpoints);
            });
        }
    }
}
