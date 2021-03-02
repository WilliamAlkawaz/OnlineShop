using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Domain.Entities
{
    public class Category
    {
        public Category()
        {
            this.date_add = DateTime.Now;
            this.date_upd = DateTime.Now;
        }
        [Key]
        public int id_category { get; set; }
        public string name { get; set; }
        public Nullable<int> id_parent { get; set; }
        public string description { get; set; }
        public System.DateTime date_add { get; set; }
        public System.DateTime date_upd { get; set; }

        public virtual ICollection<Category> Category1 { get; set; }
        public virtual Category Category2 { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Category_Product> Category_Product { get; set; }
    }
}
