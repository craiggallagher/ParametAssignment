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
    public class myTablesController : Controller
    {
        private TestEntities db = new TestEntities();

        // GET: myTables
        public ActionResult Index()
        {
            return View(db.myTables.ToList());
        }

        // GET: myTables/Details/5
        public ActionResult Details(int? id)
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

        // GET: myTables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: myTables/Create
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

        // GET: myTables/Edit/5
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

        // POST: myTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TableID,Name,Email,Phone_Numer")] myTable myTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(myTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(myTable);
        }

        // GET: myTables/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: myTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            myTable myTable = db.myTables.Find(id);
            db.myTables.Remove(myTable);
            db.SaveChanges();
            return RedirectToAction("Index");
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
