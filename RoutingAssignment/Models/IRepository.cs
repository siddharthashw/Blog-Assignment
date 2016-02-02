using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoutingAssignment.Models
{
    public interface IRepository<T>:IDisposable
    {
        IEnumerable<T> List();
        T GetById(int? id);
        void Add(T entity);
        void Delete(int? id);
        void Edit(T entity);
        void Save();
    }
}