using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Domain.Entities
{
    public class ProductAttributeValue
    {
        [Key]
        [Column(Order = 1)]
        public int id_product { get; set; }
        public virtual Product Product { get; set; }
        [Key]
        [Column(Order = 2)]
        public int id_attributevalue { get; set; }
        public virtual AttributeValue AttributeValue { get; set; }
    }
}
