using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> AsQueryable();
        IEnumerable<T> GetAll();
        IEnumerable<T> Where(Expression<Func<T, bool>> predicate);
        bool Any(Expression<Func<T, bool>> predicate);
        IQueryable<T> Include<TProperty>(Expression<Func<T, TProperty>> path);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        T Single(Expression<Func<T, bool>> predicate = null);
        T SingleOrDefault(Expression<Func<T, bool>> predicate = null);
        T First(Expression<Func<T, bool>> predicate = null);
        T FirstOrDefault(Expression<Func<T, bool>> predicate = null);
        T GetByUniqueId(object id);
        T Add(T entity);

        ICollection<T> Add(ICollection<T> entity);
        void Update(T entity);
        EntityEntry Entry(T entity);
        void Update(ICollection<T> entity);
        void Delete(T entity);
        // void AddOrUpdate(T entity);
        IEnumerable<T> Query(
          Expression<Func<T, bool>> filter = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);


    }
}
