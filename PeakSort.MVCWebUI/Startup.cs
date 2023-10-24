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
using PeakSort.Business.AutoMapper.Profiles;

namespace PeakSort.MVCWebUI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddControllersWithViews().AddRazorRuntimeCompilation();//sen bir mvc uygulamasý olarak çalýþmalaýsýn
            services.AddAutoMapper(typeof(CategoryProfile),typeof(ProductProfile),typeof(UserProfile));//Automapper kullanmalarýzý düzenler derlenme sýrasýnda

            services.AddSession();
            services.LoadServices();//bu servisleri biz yazdýk
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Admin/User/Login");
                options.LogoutPath = new PathString("/Admin/User/Logout");
                options.Cookie = new CookieBuilder
                {
                    Name = "Peaksort",
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    SecurePolicy = CookieSecurePolicy.SameAsRequest // Always
                };
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = System.TimeSpan.FromDays(7);
                options.AccessDeniedPath = new PathString("/Admin/User/AccessDenied");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();//bulunmayan bir sayfaya gidince bize 404 döndür

            }

            app.UseSession();//UseAuthentication session kullanýyor o yüzden ilk ekledik

            app.UseStaticFiles();//statik file kullanýmý söylüyoruzörnegin resim dosyasý
            app.UseRouting();


            //UseRouting() den sonra yazmamýz kullanýcý gitmek istedigi yere yonlendiriliyor sonra UseAuthentication() giriyior
            app.UseAuthentication();//kimlik konrolu
            app.UseAuthorization();

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
