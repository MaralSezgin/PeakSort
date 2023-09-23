using Microsoft.EntityFrameworkCore;
using PeakSort.Core.DataAccess.Concrete.EntityFramework;
using PeakSort.DataAccess.Abstract;
using PeakSort.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakSort.DataAccess.Concrete.EntityFramework.Repositories
{
    class EfProjectRepository : EfEntityRepositoryBase<Project>, IProjectRepository
    {
        public EfProjectRepository(DbContext context) : base(context)
        {

        }
    }
}
