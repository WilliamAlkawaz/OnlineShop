using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Domain.Entities
{
    public class Product
    {
        public Product()
        {
            this.id_manufacturer = 0;
            this.id_category_default = 0;
            this.type = " "; 
            this.on_sale = false;
            this.quantity = 0;
            this.price = 0;
            this.additional_shipping_cost = 0;
            this.reference = " "; 
            this.width = 0;
            this.height = 0;
            this.depth = 0;
            this.weight = 0;
            this.quantity_discount = false;
            this.available_date = DateTime.Now;
            this.date_add = DateTime.Now;
            this.date_upd = DateTime.Now;
            this.description_short = " ";
            this.description = " "; 
        }
        [Key]
        public int id_product { get; set; }
        [Required]
        [DisplayName("Product Name")]
        public string name { get; set; }
        [DisplayName("Manufacturer")]
        public Nullable<int> id_manufacturer { get; set; }
        [Required]
        [DisplayName("Defaul Category")]
        public Nullable<int> id_category_default { get; set; }
        public virtual Category Category { get; set; }
        [DisplayName("Type")]
        public string type { get; set; }
        [Required]
        [DisplayName("On Sale")]
        public Nullable<bool> on_sale { get; set; }
        [Required]
        [DisplayName("Quantity")]
        public int quantity { get; set; }
        [DisplayName("Price")]
        public decimal price { get; set; }
        [DisplayName("Discount\"%\"")]
        public int additional_shipping_cost { get; set; }
        public string reference { get; set; }
        public decimal width { get; set; }
        public decimal height { get; set; }
        public decimal depth { get; set; }
        public decimal weight { get; set; }
        [Required]
        public Nullable<bool> quantity_discount { get; set; }
        public System.DateTime available_date { get; set; }
        public System.DateTime date_add { get; set; }
        public System.DateTime date_upd { get; set; }
        public string description_short { get; set; }
        public string description { get; set; }

        
        //public virtual ICollection<Image> Images { get; set; }
        //public virtual Manufacturer Manufacturer { get; set; }
        //public virtual ICollection<Cart_Product> Cart_Product { get; set; }
        public virtual ICollection<Category_Product> Category_Product { get; set; }
        public virtual ICollection<ProductAttributeValue> ProductAttributeValue { get; set; }
    }
}

