using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Bookstore.Models;

namespace Bookstore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration temp)
        {
            Configuration = temp;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //This is how you tell asp.net to use the controllers
            services.AddControllersWithViews();

            services.AddDbContext<BookstoreContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:BookDBConnection"]);
            });

            // Add repository 
            services.AddScoped<IBooksRepository, EFBooksRepository>();

            //Enable to use razor pages
            services.AddRazorPages();

            services.AddDistributedMemoryCache();

            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Corresponds to the wwwroot
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("typepage",
                "{Category}/Page{pageNum}",
                new { Controller = "Home", action = "Index" });

                endpoints.MapControllerRoute(
                    "Paging","{pageNum}",new { Controller = "Home", action = "Index", pageNum = 1 });

                endpoints.MapControllerRoute("type",
                    "{Category}",
                    new { Controller = "Home", action = "Index", pageNum = 1 });

                //This is doing what we have done im the past. name:"default", patter:"{controller=Home}/{action=Index}/{id?}");
                endpoints.MapDefaultControllerRoute();

                endpoints.MapRazorPages();
            });
        }
    }
}