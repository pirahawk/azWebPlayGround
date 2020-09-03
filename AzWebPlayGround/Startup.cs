using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzWebPlayGround.Middleware;
using AzWebPlayGround.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AzWebPlayGround
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
            services.AddHttpContextAccessor();
            services.AddMvc(options =>
            {
                options.Filters.Add<MyJwtAuthFilterAttribute>();
            });
            services.AddRazorPages(rpOptions =>
            {
            });
            services.AddTransient<MyJwtAuthFilterAttribute>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAntiForgeryService, AntiForgeryService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthorization();
            app.UseMiddleware<IdentityProviderMiddleWare>();
            
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
