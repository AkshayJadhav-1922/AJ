using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Aj.DataAccess.Repository.IRepository;
using AJ.DataAcess.Data;
using Microsoft.EntityFrameworkCore;

namespace Aj.DataAccess.Repository.IRepository
{
    internal class Repository<T> : IRepository<T> where T : class  //Remember: Make class generic and observe inheritance
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = dbSet.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRaneg(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
