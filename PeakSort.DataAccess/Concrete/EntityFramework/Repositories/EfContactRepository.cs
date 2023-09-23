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
    class EfContactRepository : EfEntityRepositoryBase<Contact>, IContactRepository
    {
        public EfContactRepository(DbContext context) : base(context)
        {

        }
    }
}
