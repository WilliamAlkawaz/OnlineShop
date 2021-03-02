using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Domain.Entities
{
    public class Category_Product
    {
        [Key]
        [Column(Order = 1)]
        public int id_category { get; set; }
        public virtual Category Category { get; set; }
        [Key]
        [Column(Order = 2)]
        public int id_product { get; set; }
        public virtual Product Product { get; set; }
    }        
}



