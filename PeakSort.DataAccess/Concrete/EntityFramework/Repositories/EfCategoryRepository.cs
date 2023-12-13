using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PeakSort.Core.DataAccess.Concrete.EntityFramework;
using PeakSort.DataAccess.Abstract;
using PeakSort.DataAccess.Concrete.EntityFramework.Contexts;
using PeakSort.Entities.Concrete;

namespace PeakSort.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfCategoryRepository:EfEntityRepositoryBase<Category>,ICategoryRepository
    {
        public EfCategoryRepository(DbContext context) : base(context)
        {
        }

        public async Task<Category> GetById(int categoryId)
        {
            return await ProgrammersBlogContext.Categories.SingleOrDefaultAsync(c => c.Id == categoryId);
        }

        private ProgrammersBlogContext ProgrammersBlogContext
        {
            get
            {
                return _context as ProgrammersBlogContext;
            }
        }
    }
}
