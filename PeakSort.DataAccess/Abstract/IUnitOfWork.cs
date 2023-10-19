using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakSort.DataAccess.Abstract
{
   public interface IUnitOfWork:IAsyncDisposable
    {
        IAboutRepository Abouts { get; }
        ICategoryRepository Categorys { get; }
        IContactRepository Contacts { get; }
        IProductRepository Products { get; }
        IProjectRepository Projects { get; }
        IReferenceRepository References { get; }


        Task<int> SaveAsync();
    }
}
