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
    public class ProductAttributeValuesController : Controller
    {
        private EFDbContext db = new EFDbContext();

        // GET: ProductAttributeValues
        public ActionResult Index()
        {
            var productAttributeValues = db.ProductAttributeValues.Include(p => p.AttributeValue).Include(p => p.Product);
            return View(productAttributeValues.ToList());
        }

        // GET: ProductAttributeValues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductAttributeValue productAttributeValue = db.ProductAttributeValues.Find(id);
            if (productAttributeValue == null)
            {
                return HttpNotFound();
            }
            return View(productAttributeValue);
        }

        // GET: ProductAttributeValues/Create
        public ActionResult Create()
        {
            ViewBag.id_attributevalue = new SelectList(db.AttributeValues, "id_attributevalue", "value");
            ViewBag.id_product = new SelectList(db.Products, "id_product", "name");
            return View();
        }

        // POST: ProductAttributeValues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_product,id_attributevalue")] ProductAttributeValue productAttributeValue)
        {
            if (ModelState.IsValid)
            {
                db.ProductAttributeValues.Add(productAttributeValue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_attributevalue = new SelectList(db.AttributeValues, "id_attributevalue", "value", productAttributeValue.id_attributevalue);
            ViewBag.id_product = new SelectList(db.Products, "id_product", "name", productAttributeValue.id_product);
            return View(productAttributeValue);
        }

        // GET: ProductAttributeValues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductAttributeValue productAttributeValue = db.ProductAttributeValues.Find(id);
            if (productAttributeValue == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_attributevalue = new SelectList(db.AttributeValues, "id_attributevalue", "value", productAttributeValue.id_attributevalue);
            ViewBag.id_product = new SelectList(db.Products, "id_product", "name", productAttributeValue.id_product);
            return View(productAttributeValue);
        }

        // POST: ProductAttributeValues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_product,id_attributevalue")] ProductAttributeValue productAttributeValue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productAttributeValue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_attributevalue = new SelectList(db.AttributeValues, "id_attributevalue", "value", productAttributeValue.id_attributevalue);
            ViewBag.id_product = new SelectList(db.Products, "id_product", "name", productAttributeValue.id_product);
            return View(productAttributeValue);
        }

        // GET: ProductAttributeValues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductAttributeValue productAttributeValue = db.ProductAttributeValues.Find(id);
            if (productAttributeValue == null)
            {
                return HttpNotFound();
            }
            return View(productAttributeValue);
        }

        // POST: ProductAttributeValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductAttributeValue productAttributeValue = db.ProductAttributeValues.Find(id);
            db.ProductAttributeValues.Remove(productAttributeValue);
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
