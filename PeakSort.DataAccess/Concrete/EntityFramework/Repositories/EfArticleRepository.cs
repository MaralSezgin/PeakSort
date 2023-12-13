using Microsoft.EntityFrameworkCore;
using PeakSort.Core.DataAccess.Concrete.EntityFramework;
using PeakSort.DataAccess.Abstract;
using PeakSort.Entities.Concrete;

namespace PeakSort.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfArticleRepository:EfEntityRepositoryBase<Article>,IArticleRepository
    {
        public EfArticleRepository(DbContext context) : base(context)
        {
        }

    }
}
