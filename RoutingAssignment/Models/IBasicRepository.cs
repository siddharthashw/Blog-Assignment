using System;
using System.Collections.Generic;

namespace RoutingAssignment.Models
{
    public interface IBasicRepository: IDisposable
    {
        IEnumerable<Basic> GetBlog();
        Basic GetBlogById(int? id);
        void InsertBlog(Basic blog);
        void DeleteBlog(int? id);
        void UpdateBlog(Basic blog);
        void Save();
    }
}
