using Microsoft.Extensions.DependencyInjection;
using PeakSort.Business.Abstract;
using PeakSort.Business.Concrete;
using PeakSort.DataAccess.Abstract;
using PeakSort.DataAccess.Concrete;
using PeakSort.DataAccess.Concrete.EntityFramework.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakSort.Business.Extensions
{
   public static class ServiceCollectionExtensions
    {
        public static IServiceCollection  LoadServices(this IServiceCollection serviscollection)
        {
            serviscollection.AddDbContext<PeaksortContext>();
            serviscollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviscollection.AddScoped<ICategoryService, CategoryManager>();
            serviscollection.AddScoped<IProductService, ProductManager>();
            return serviscollection;
        }
    }
}
