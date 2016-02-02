using System;
using System.ComponentModel.DataAnnotations.Schema;
using IdentitySample.Models;

namespace RoutingAssignment.Models
{
    public class Basic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime Date { get; set; }      

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }
    }
}