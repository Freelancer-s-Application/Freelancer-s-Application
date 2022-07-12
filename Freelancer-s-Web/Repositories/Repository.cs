using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Freelancer_s_Web.Models;

namespace Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly FreelancerContext _dbContext;
        internal DbSet<T> dbSet;

        public Repository(FreelancerContext dbContext)
        {
            _dbContext = dbContext;
            this.dbSet = _dbContext.Set<T>();
        }

        public void Add(T t)
        {
            dbSet.Add(t);
        }

        public T Get(int Id)
        {
            return dbSet.Find(Id);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.AsNoTracking().Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includedProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includedProp);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            return query;

        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            //if (filter != null)
            //{
            //    query.Where(filter);
            //}

            if (includeProperties != null)
            {
                foreach (var includedProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.AsNoTracking().Include(includedProp);
                }
            }

            if (filter != null)
            {
                return query.FirstOrDefault(filter);
            } else
            {
                return query.FirstOrDefault();
            }
        }

        public void Remove(T t)
        {
            dbSet.Remove(t);
        }

        public void Remove(int id)
        {
            T deletedDto = dbSet.Find(id);
            if (deletedDto != null)
            {
                dbSet.Remove(deletedDto);
            }
        }

        public void RemoveRange(IEnumerable<T> t)
        {
            dbSet.RemoveRange(t);
        }
    }
}
