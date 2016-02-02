using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using IdentitySample.Models;
using Microsoft.AspNet.Identity;
using RoutingAssignment.Models;

namespace RoutingAssignment.Controllers
{
    [Authorize]
    public class BasicsController : Controller
    {
        //Using Normal DAtabase
        //private ApplicationDbContext db = new ApplicationDbContext();


        //Using BasicRepository
        //private IBasicRepository basicRepository;
        //public BasicsController(IBasicRepository basicRepository)
        //{
        //    this.basicRepository = basicRepository;
        //}
        //public BasicsController()
        //{
        //    basicRepository = new BasicRepository(new ApplicationDbContext());
        //}


        //Using Generic Repository
        private readonly IRepository<Basic> repository;

        public BasicsController(IRepository<Basic> basicRepository)
        {
            repository = basicRepository;
            //repository = new Repository<Basic>(new ApplicationDbContext());
        }
        
        // GET: Basics
        public ActionResult Index()
        {
            //Using Normal DAtabase
            //return View(db.basics.ToList());
            
            //Using BasicRepository
            //return View(basicRepository.GetBlog());

            //Using Generic Repository
            return View(repository.List());

        }

        public ActionResult Index2()
        {
            //Using Repository
            var example = from title in repository.List()
                where title.Title.StartsWith("A")
                select title;

            //Using BasicRepository
            //var example = from title in basicRepository.GetBlog()
            //              where title.Title.StartsWith("A")
            //              select title;

            //Using Normal database
            //var example = from title in db.basics.Include("User")
            //            where title.Title.StartsWith("A")
            //              select title;
            ViewBag.Contain = example.ToList();
            return View(repository.List());
        }

        public ActionResult Index3()
        {
            //Using Repository
            var example = from title in repository.List()
                where title.Title.StartsWith("A")
                orderby title.Date >= DateTime.Today
                group title by title.User
                into grp
                select grp;

            //Using BasicRepository
            //var example = from title in basicRepository.GetBlog()
            //    where title.Title.StartsWith("A")
            //    orderby title.Date >= DateTime.Today
            //    group title by title.User
            //    into grp
            //    select grp;

            //Using Normal database
            //var example = from a in db.basics.Include("User")
            //              where a.Title.StartsWith("A")
            //              orderby a.Date >= DateTime.Today
            //              group a by a.User
            //                  into g
            //                  select g.ToList();
            ViewBag.Deposit = example.ToList();
            return View(repository.List());
        }

        public ActionResult Index5()
        {
            //Using REpository
            var example =
                repository.List()
                    .Where(title => title.Title.StartsWith("A"))
                    .OrderBy(title => title.Date >= DateTime.Today)
                    .GroupBy(title => title.User);

            //Using BasicRepository
            //var example =
            //    basicRepository.GetBlog()
            //        .Where(title => title.Title.StartsWith("A"))
            //        .OrderBy(title => title.Date >= DateTime.Today)
            //        .GroupBy(title => title.User);

            //Using Normal database
            //var example =
            //    db.basics.Where(a => a.Title.StartsWith("A")).Include("User")
            //        .OrderBy(a => a.Date >= DateTime.Today)
            //        .GroupBy(a => a.User);
            ViewBag.Deposit = example.ToList();
            return View(repository.List());
        }

        public ActionResult Index4()
        {
            //Using Repository
            var example = from title in repository.List()
                where title.Date >= DateTime.MinValue
                orderby DateTime.Today
                select title;

            //Using BasicRepository
            //var examples =
            //    (from title in basicRepository.GetBlog()
            //        where title.Date >= DateTime.MinValue
            //        orderby DateTime.Today
            //        select title);

            //Using Repository
            //var examples = (from blog in db.basics.Include("User") where blog.Date >= DateTime.Today orderby DateTime.Today select blog).ToList();
            ViewBag.Carry = example;
            return View(repository.List());
        }

        //POST:Basics/Search
        [HttpPost]
        public ActionResult Search(string Title)
        {
            var examples = from title in repository.List()
                           where title.Title.Equals(Title)
                           select title;
            return View(examples);
        }

        // GET: Basics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Using Repository
            Basic basic = repository.GetById(id);

            //Using BasicRepository
            //Basic basic = basicRepository.GetBlogById(id);
            if (basic == null)
            {
                return HttpNotFound();
            }
            return View(basic);
        }

        // GET: Basics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Basics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description")] Basic basic)
        {
            if (ModelState.IsValid)
            {
                basic.Date = DateTime.Now;
                //Using Repository
                repository.Add(basic);

                //Using BasicRepository
                //basicRepository.InsertBlog(basic);
                //db.basics.Add(basic);
                basic.UserId = User.Identity.GetUserId();
                //Using Repository
                repository.Save();

                //Using BasicRepository
                //basicRepository.Save();
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(basic);
        }

        // GET: Basics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Basic basic = repository.GetById(id);
            if (basic == null)
            {
                return HttpNotFound();
            }
            return View(basic);
        }

        // POST: Basics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description")] Basic basic)
        {
            if (ModelState.IsValid)
            {
                repository.Edit(basic);
                repository.Save();
                //db.Entry(basic).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(basic);
        }

        // GET: Basics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Basic basic = repository.GetById(id);
            if (basic == null)
            {
                return HttpNotFound();
            }
            return View(basic);
        }

        // POST: Basics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            //Basic basic = basicRepository.GetBlogById(id);
            //var basic = repository.GetById(id);
            repository.Delete(id);
            repository.Save();
            //db.basics.Remove(basic);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
