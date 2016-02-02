using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using IdentitySample.Models;

namespace RoutingAssignment.Models
{
    public class Repository<T> : IRepository<T> where T : class 
    {
        private ApplicationDbContext db;
        private DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            this.db = db;
            dbSet = db.Set<T>();
        }

        public virtual IEnumerable<T> List()
        {
            return dbSet.AsEnumerable();
        }

        public virtual T GetById(int? id)
        {
            return dbSet.Find(id);
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int? id)
        {
            T detail=dbSet.Find(id);
            dbSet.Remove(detail);
            db.SaveChanges();
        }

        public void Edit(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool _disposed ;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal object As<T1>()
        {
            throw new NotImplementedException();
        }
    }
}