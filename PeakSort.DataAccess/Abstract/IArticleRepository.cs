using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeakSort.Core.DataAccess.Abstract;
using PeakSort.Entities.Concrete;

namespace PeakSort.DataAccess.Abstract
{
    public interface IArticleRepository : IEntityRepository<Article>
    {
    }
}
