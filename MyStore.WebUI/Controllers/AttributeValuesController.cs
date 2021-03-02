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
    public class AttributeValuesController : Controller
    {
        private EFDbContext db = new EFDbContext();

        // GET: AttributeValues
        public ActionResult Index()
        {
            var attributeValues = db.AttributeValues.Include(a => a.Attributee);
            return View(attributeValues.ToList());
        }

        // GET: AttributeValues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttributeValue attributeValue = db.AttributeValues.Find(id);
            if (attributeValue == null)
            {
                return HttpNotFound();
            }
            return View(attributeValue);
        }

        // GET: AttributeValues/Create
        public ActionResult Create()
        {
            ViewBag.id_attribute = new SelectList(db.Attributes, "id_attribute", "name");
            return View();
        }

        // POST: AttributeValues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_attributevalue,id_attribute,value")] AttributeValue attributeValue)
        {
            if (ModelState.IsValid)
            {
                db.AttributeValues.Add(attributeValue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_attribute = new SelectList(db.Attributes, "id_attribute", "name", attributeValue.id_attribute);
            return View(attributeValue);
        }

        // GET: AttributeValues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttributeValue attributeValue = db.AttributeValues.Find(id);
            if (attributeValue == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_attribute = new SelectList(db.Attributes, "id_attribute", "name", attributeValue.id_attribute);
            return View(attributeValue);
        }

        // POST: AttributeValues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_attributevalue,id_attribute,value")] AttributeValue attributeValue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attributeValue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_attribute = new SelectList(db.Attributes, "id_attribute", "name", attributeValue.id_attribute);
            return View(attributeValue);
        }

        // GET: AttributeValues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttributeValue attributeValue = db.AttributeValues.Find(id);
            if (attributeValue == null)
            {
                return HttpNotFound();
            }
            return View(attributeValue);
        }

        // POST: AttributeValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AttributeValue attributeValue = db.AttributeValues.Find(id);
            db.AttributeValues.Remove(attributeValue);
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
