using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MundoRock.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MundoRock
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromSeconds(300);
                    options.SlidingExpiration = true;
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/Logout";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                    options.Cookie.Name = "mundorockSesion";
                });
               
            services.AddDbContext<mundorockContext>(options =>
            {
                options.UseMySql("server=localhost;user=root;password=adlogcat45;database=mundorock", 
                    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.23-mysql"));
            });

            services.AddScoped<MundoRock.Services.AlbumesService>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseFileServer();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapDefaultControllerRoute();//usuario anonimo
            });
        }
    }
}
