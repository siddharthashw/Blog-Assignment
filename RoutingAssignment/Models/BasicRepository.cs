using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using IdentitySample.Models;

namespace RoutingAssignment.Models
{
    public class BasicRepository : IBasicRepository
    {
        private ApplicationDbContext db;

        public BasicRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Basic> GetBlog()
        {
            return db.basics.ToList();
        }

        public Basic GetBlogById(int? id)
        {
            return db.basics.Find(id);
        }

        public void InsertBlog(Basic student)
        {
            db.basics.Add(student);
        }

        public void DeleteBlog(int? studentid)
        {
            Basic student = db.basics.Find(studentid);
            db.basics.Remove(student);
        }

        public void UpdateBlog(Basic student)
        {
            db.Entry(student).State = EntityState.Modified;
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
    }
}
