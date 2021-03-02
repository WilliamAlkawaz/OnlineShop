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
using System.IO;
using System.Reflection;

namespace MyStore.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        static IEnumerable<string> list;
        static string id_category_selected; 
        private EFDbContext db = new EFDbContext();

        // GET: Products
        public ActionResult Index()
        {
            ViewBag.im = db.Images; 
            return View(db.Products.ToList());
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            Product product = new Product(); 
            ViewBag.id_category_default = new SelectList(db.Categories, "id_category", "name");
            return View(product);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_product,name,id_manufacturer,id_category_default,type,on_sale,quantity,price,additional_shipping_cost,reference,width,height,depth,weight,quantity_discount,available_date,date_add,date_upd,description_short,description")] Product product)
        {
            if (ModelState.IsValid)
            {
                Session["Message"] = "You have just added " + product.name;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = product.id_product });
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_category_default = new SelectList(db.Categories, "id_category", "name");
            var Data = from s in db.Categories
                       select new { s.id_category, s.id_parent, s.name };
            var cp = from s in db.Categories_Products
                     where s.id_product == id
                     select new { s.id_category };
            var result = (from t1 in db.Categories_Products
                           where t1.id_product == id
                           join t2 in db.Categories
                          on t1.id_category equals t2.id_category
                          select new { t2.name, });
            ViewBag.Data = LINQToDataTable(Data);

            ViewBag.Count = Data.Count();
            ViewBag.Index = db.Categories.OrderBy(x => x.id_category).Take(2).First().id_category;
            ViewBag.cp = LINQToDataTable(result); 
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_product,name,id_manufacturer,id_category_default,type,on_sale,quantity,price,additional_shipping_cost,reference,width,height,depth,weight,quantity_discount,available_date,date_add,date_upd,description_short,description")] Product product)
        {
            if (ModelState.IsValid)
            {
                
                    //if (!String.IsNullOrEmpty(id_category_selected))
                    {
                    var d = db.Categories.Where(x => x.name == id_category_selected).Select(x=>x.id_category).SingleOrDefault(); 
                      //  (from x in db.Categories
                        //              where x.name == id_category_selected
                          //            select x.id_category);
                        product.id_category_default = d;
                    }

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();

                var g = db.Categories_Products.Where(x => x.id_product == product.id_product).AsEnumerable().ToList();
                db.Categories_Products.RemoveRange(g);
                db.SaveChanges();

                if (list.Count() != 0)
                {
                    foreach (var item in list)
                    {
                        if (!String.IsNullOrEmpty(item.ToString()))
                        {
                            Category_Product cp = new Category_Product();
                            cp.id_product = product.id_product;
                            cp.id_category = (int)(from c in db.Categories
                                                   where c.name == item.ToString()
                                                   select c.id_category)
                                              .Single();
                            int jj = (from c in db.Categories_Products
                                      where c.id_category == cp.id_category && c.id_product == cp.id_product
                                      select new { c.id_category, c.id_product }).ToList().Count();

                            if (jj == 0)
                            {
                                db.Categories_Products.Add(cp);
                                db.SaveChanges();
                            }

                            else 
                            {
                                TempData["msg"] = "<script>alert('Error: Category already added');</script>";
                                //list = null;
                                //return RedirectToAction("Edit", "Products", new { id = product.id_product });
                            }

                            //dt.Tables.Rows.Add(cp); 
                            //dt.Rows.Add(cp); 
                        }
                    }
                    list = null;
                }

                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Product product = db.Products.Find(id);
            ViewBag.im = db.Images;

            return View(product); 
        }

        public ActionResult Testing()
        {
            return View(); 
        }

        public PartialViewResult All(int id)
        {
            List<Image> model = db.Images.Where(x => x.id_product == id).ToList();

            if (model.Count() == 0)
            {
                Image i = new Image();
                i.id_product = id;
                List<Image> li = new List<Image>();
                li.Add(i);
                return PartialView("_File", li);
            }
            else
                return PartialView("_File", model);
        }

        [HttpPost]
        public ActionResult Upload(int model)
        {

            for (int i = 0; i < Request.Files.Count; i++)
            {
                Image p = new Image();
                p.cover = true;
                p.id_product = model;
                db.Images.Add(p);
                db.SaveChanges();
                var ims = db.Images.OrderByDescending(x => x.id_image).Take(1).Single();
                var file = Request.Files[i];
                var fileName = ims.id_image.ToString() + ".jpg"; //Path.GetFileName(file.FileName);
                var path = Server.MapPath("~/Images/" + model.ToString() + "/");
                if (this.CreateFolderIfNeeded(path))
                    file.SaveAs(path + fileName);
            }

            return RedirectToAction("All", new { id = model });
        }

        public PartialViewResult LinkCategory(int id)
        {
                return PartialView("_TreeView");
        }

        [HttpPost]
        public ActionResult GetSelectedCategory(string selectedItem)
        {
            // call with two parameters and return them back
            id_category_selected = selectedItem;
            return View();             
        }

        [HttpPost]
        public void GetSelectedCategories(IEnumerable<string> options, string selected)
        {
            list = options;
            id_category_selected = selected; 
        }

        private bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    /*TODO: You must process this exception.*/
                    result = false;
                }
            }
            return result;
        }

        public DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();


            // column names
            PropertyInfo[] oProps = null;


            if (varlist == null) return dtReturn;


            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;


                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }


                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }


                DataRow dr = dtReturn.NewRow();


                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }


                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
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
