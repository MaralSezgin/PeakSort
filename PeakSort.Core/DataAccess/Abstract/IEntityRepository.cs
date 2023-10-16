using PeakSort.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PeakSort.Core.DataAccess.Abstract
{
   public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] incluedeProperties);
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate=null, params Expression<Func<T, object>>[] incluedeProperties);

        Task<T> AddAsync(T entity);
        Task DeleteAsync(T entity);

        Task<T> UpdateAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate );
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);



    }
}
