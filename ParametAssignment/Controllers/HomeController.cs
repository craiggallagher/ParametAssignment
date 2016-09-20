using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ParametAssignment.Models;

namespace ParametAssignment.Controllers
{
    public class HomeController : Controller
    {
        private TestEntities db = new TestEntities();

        // GET: Home
        public ActionResult Index(string searchString)
        {
            var movies = from m in db.myTables
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Name.Contains(searchString));
            }

            return View(movies);
        }

        // GET: Home/Details/5
        // GET: Tasks/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var task = db.myTables.Find(id);
            }
            catch (HttpException e)
            {
                throw new HttpException(404, "Details not found.");
            }

            return View(db.myTables.Find(id));
        }
        [HttpGet]
        public PartialViewResult ShowTableByID(int id)
        {
            try
            {
                var tasks = db.myTables.ToList();
            }
            catch (HttpException e)
            {
                throw new HttpException(404, "Details nout found.");
            }

            return PartialView("_Details", db.myTables.Find(id));
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TableID,Name,Email,Phone_Numer")] myTable myTable)
        {
            if (ModelState.IsValid)
            {
                db.myTables.Add(myTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(myTable);
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            myTable myTable = db.myTables.Find(id);
            if (myTable == null)
            {
                return HttpNotFound();
            }
            return View(myTable);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        public PartialViewResult EditTableID(int id)
        {
            return PartialView("_Edit", db.myTables.Find(id));
        }

        [HttpPost, ActionName("EditTableID")]
        public ActionResult Edit(myTable editTable)
        {
            try
            {

                db.Entry(editTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // GET: Home/Delete/5
        public PartialViewResult TableByID(int id)
        {
            return PartialView("_Delete", db.myTables.Find(id));
        }
        [HttpPost, ActionName("TableByID")]
        public ActionResult DeleteConfirmed(int id)
        {
            db.myTables.Remove(db.myTables.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            myTable table = db.myTables.Find(id);
            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
