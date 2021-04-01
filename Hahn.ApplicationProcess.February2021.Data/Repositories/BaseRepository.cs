using Hahn.ApplicationProcess.February2021.Domain.DBContext;
using Hahn.ApplicationProcess.February2021.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DbSet<T> _dbSet;
        protected readonly ApplicationContext _db;

        public BaseRepository(ApplicationContext dbCon)
        {
            _db = dbCon;
            _dbSet = _db.Set<T>();
        }

        public virtual IQueryable<T> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<T> Include<TProperty>(Expression<Func<T, TProperty>> path)
        {
            return _dbSet.Include(path);
        }
        public IEnumerable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Any(predicate);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);

        }

        public T Single(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
                return _dbSet.Single();
            else
                return _dbSet.Single(predicate);
        }

        public T SingleOrDefault(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
                return _dbSet.SingleOrDefault();
            else
                return _dbSet.SingleOrDefault(predicate);
        }

        public T First(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
                return _dbSet.First();
            else
                return _dbSet.First(predicate);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
                return _dbSet.FirstOrDefault();
            else
                return _dbSet.FirstOrDefault(predicate);
        }

        public T GetByUniqueId(object id)
        {
            return _dbSet.Find(new object[] { id });
        }

        public T Add(T entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public ICollection<T> Add(ICollection<T> entity)
        {
            ICollection<T> retVal = new List<T>();
            foreach (var t in entity)
            {
                _dbSet.Add(t);
                retVal.Add(t);
            }

            return retVal;
        }
        public void Update(ICollection<T> entity)
        {
            ICollection<T> retVal = new List<T>();
            foreach (var t in entity)
            {
                _db.Entry(t).State = EntityState.Modified;
            }

        }
        public void Update(T entity)
        {


            _db.Entry(entity).State = EntityState.Modified;
            //_db.SaveChanges();
            //_db.Update(entity);
            // return entity;
        }

        public void Delete(T entity)
        {
            _db.Remove(entity);
        }

        public EntityEntry Entry(T entity)
        {
            return _db.Entry(entity);
        }

        public virtual IEnumerable<T> Query(
           Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
    }
}
