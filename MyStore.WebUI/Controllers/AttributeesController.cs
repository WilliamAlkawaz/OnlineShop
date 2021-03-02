using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyStore.Domain.Concrete;
using MyStore.Domain.Entities;

namespace MyStore.WebUI.Controllers
{
    public class AttributeesController : Controller
    {
        private EFDbContext db = new EFDbContext();

        // GET: Attributees
        public ActionResult Index()
        {
            return View(db.Attributes.ToList());
        }

        // GET: Attributees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attributee attributee = db.Attributes.Find(id);
            if (attributee == null)
            {
                return HttpNotFound();
            }
            return View(attributee);
        }

        // GET: Attributees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Attributees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_attribute,name")] Attributee attributee)
        {
            if (ModelState.IsValid)
            {
                db.Attributes.Add(attributee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(attributee);
        }

        // GET: Attributees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attributee attributee = db.Attributes.Find(id);
            if (attributee == null)
            {
                return HttpNotFound();
            }
            return View(attributee);
        }

        // POST: Attributees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_attribute,name")] Attributee attributee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attributee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(attributee);
        }

        // GET: Attributees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attributee attributee = db.Attributes.Find(id);
            if (attributee == null)
            {
                return HttpNotFound();
            }
            return View(attributee);
        }

        // POST: Attributees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attributee attributee = db.Attributes.Find(id);
            db.Attributes.Remove(attributee);
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
