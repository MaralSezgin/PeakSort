using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PeakSort.Business.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;



namespace PeakSort.MVCWebUI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddControllersWithViews().AddRazorRuntimeCompilation();//sen bir mvc uygulamas� olarak �al��mala�s�n
            services.AddAutoMapper(typeof(Startup));//Automapper kullanmalar�z� d�zenler derlenme s�ras�nda
            services.LoadServices();//bu servisleri biz yazd�k
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();//bulunmayan bir sayfaya gidince bize 404 d�nd�r

            }

            app.UseStaticFiles();//statik file kullan�m� s�yl�yoruz�rnegin resim dosyas�
            app.UseRouting();

            app.UseEndpoints(endpoints =>//herhanbi bir istek gelince buraya girer
            {
                endpoints.MapAreaControllerRoute(
                     name: "Admin",
                     areaName: "Admin",
                     pattern: "Admin/{controller=Home}/{action=Index}/{id?}"

                    );
                endpoints.MapDefaultControllerRoute();//default olarak home controler ve index e gidr
            });
        }
    }
}
