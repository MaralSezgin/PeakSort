using Microsoft.Extensions.DependencyInjection;
using PeakSort.Business.Abstract;
using PeakSort.Business.Concrete;
using PeakSort.DataAccess.Abstract;
using PeakSort.DataAccess.Concrete;
using PeakSort.DataAccess.Concrete.EntityFramework.Contexts;
using PeakSort.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PeakSort.Business.Extensions
{
   public static class ServiceCollectionExtensions
    {
        public static IServiceCollection  LoadServices(this IServiceCollection serviscollection)
        {
            serviscollection.AddIdentity<User, Role>(x =>
            {
                //user password setting
                x.Password.RequireDigit = false;//şifre rakamlı olsun mu(rakam zrounlu degil)
                x.Password.RequiredLength = 6;//şifre 10karakter
                x.Password.RequiredUniqueChars = 2;// 2 özel karakter istiyoruz *_- .. gibi
                x.Password.RequireNonAlphanumeric = false;// @ gibi rakterere gerek yok bak
                x.Password.RequireLowercase = false;//büyük küçük takılmıyor
                x.Password.RequireUppercase = false;

                //user username and email setting
                x.User.AllowedUserNameCharacters= "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";//kulanıcı adına bu karakterlere izin veriyoruz
                x.User.RequireUniqueEmail = true;//aynı mailden başka olmasın

           


            } ).AddEntityFrameworkStores<PeaksortContext>();

          



            serviscollection.AddDbContext<PeaksortContext>();
            serviscollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviscollection.AddScoped<ICategoryService, CategoryManager>();
            serviscollection.AddScoped<IProductService, ProductManager>();
            return serviscollection;
        }
    }
}
