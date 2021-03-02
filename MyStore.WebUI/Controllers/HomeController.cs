using MyStore.Domain.Concrete;
using MyStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MyStore.WebUI.Controllers
{
    public class ViewModel
    {
        public ViewModel(string name1, decimal price1, int discount1)
        {
            this.name = name1;
            this.price_after_discount = price1 - price1 * (Decimal.Parse(discount1.ToString()) / 100);
            this.price = price1;
            this.discount = discount1;
        }
        public int id_product; 
        public string name;
        public string description;
        public int quantity; 
        public decimal price;
        public int discount;
        public decimal price_after_discount;
        public List<Image> images;
        public List<ProductAttributeValue> pavalue; 
    }

    public class HomeController : Controller
    {
        private EFDbContext db = new EFDbContext();
        
        public ActionResult getProducts(int id)
        {
            List<Category_Product> model1 = db.Categories_Products.Where(x => x.id_category == id).ToList();
            List<Product> model = new List<Product>(); 
            foreach (var item in model1)
            {
                model.Add(db.Products.Where(x => x.id_product == item.id_product).ToList().Single());                 
            }
            ViewBag.im = db.Images;

            return View(model); 
        }

        public PartialViewResult Menu(string category)
        {
            //var Data = from s in db.Categories
            //           select new { s.id_category, s.id_parent, s.name };
            ViewBag.SelectedCategory = category;
            var Data = from s in db.Categories
                       select new { s.id_category, s.id_parent, s.name };
            //var Data = LINQToDataTable(db.Categories);
            //ViewBag.Data = LINQToDataTable(db.Categories); ;
            ViewBag.Count = Data.Count();
            ViewBag.Index = db.Categories.OrderBy(x => x.id_category).Take(2).First().id_category;
            return PartialView(db.Categories);
        }

        public ActionResult Product_Detail(int id)
        {
            Product product = db.Products.Where(x => x.id_product == id).Single(); 
            ViewModel vm = new ViewModel(product.name, product.price, product.additional_shipping_cost);
            vm.id_product = id;
            vm.description = product.description_short;
            vm.quantity = product.quantity; 
            vm.images = db.Images.Where(x => x.id_product == id).ToList();
            var pavalue = db.ProductAttributeValues.Where(x => x.id_product == id).Join(db.AttributeValues, e => e.id_attributevalue, d => d.id_attribute, (name, attributevalue) => new
            {
                name.id_attributevalue,
                attributevalue.value
            }).ToList();
            var jj = pavalue.Join(db.AttributeValues, e=>e.id_attributevalue, d=>d.id_attribute, (values, names) => new
            {
                values.id_attributevalue,
                names.id_attribute                
            }).ToList();
            var ll = db.Attributes.ToList();
            ViewBag.pavalues = new SelectList(pavalue, "id_attributevalue", "value");
            ViewBag.names = ll.Take(1).Single().name;  
              //  SelectList(db.Categories, "id_category", "name");
            return View(vm); 
        }

        //public DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        //{
        //    DataTable dtReturn = new DataTable();


        //    // column names
        //    PropertyInfo[] oProps = null;


        //    if (varlist == null) return dtReturn;


        //    foreach (T rec in varlist)
        //    {
        //        // Use reflection to get property names, to create table, Only first time, others will follow
        //        if (oProps == null)
        //        {
        //            oProps = ((Type)rec.GetType()).GetProperties();
        //            foreach (PropertyInfo pi in oProps)
        //            {
        //                Type colType = pi.PropertyType;


        //                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
        //                {
        //                    colType = colType.GetGenericArguments()[0];
        //                }


        //                dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
        //            }
        //        }


        //        DataRow dr = dtReturn.NewRow();


        //        foreach (PropertyInfo pi in oProps)
        //        {
        //            dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
        //            (rec, null);
        //        }


        //        dtReturn.Rows.Add(dr);
        //    }
        //    return dtReturn;
        //}
    }
}