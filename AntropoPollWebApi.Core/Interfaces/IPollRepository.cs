using AntropoPollWebApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AntropoPollWebApi.Core.Interfaces
{
    public interface IPollRepository<T> where T : BaseModel
    {
        IQueryable<T> GetAll();
        T Get(Guid guid);
        Task<T> GetAsync(Guid guid);
        T Insert(T entity);
        T Update(T entity);
        T Replace(T oldEntity, T newEntity);
        void Delete(T entity);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        IEnumerable<T> Include(params Expression<Func<T, object>>[] includes);
        void RemoveRange(IQueryable<T> deleteRecords);
        void UpdateRange(IQueryable<T> updateRecords);
    }
}
