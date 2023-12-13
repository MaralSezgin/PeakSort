using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PeakSort.Core.DataAccess.Concrete.EntityFramework;
using PeakSort.DataAccess.Abstract;
using PeakSort.Entities.Concrete;

namespace PeakSort.DataAccess.EntityFramework.Repositories
{
    public class EfCommentRepository:EfEntityRepositoryBase<Comment>,ICommentRepository
    {
        public EfCommentRepository(DbContext context) : base(context)
        {
        }
    }
}
