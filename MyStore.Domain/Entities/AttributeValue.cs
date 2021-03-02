using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Domain.Entities
{
    public class AttributeValue
    {
        [Key]
        public int id_attributevalue { get; set; }
        [Required]
        public int id_attribute { get; set; }
        public virtual Attributee Attributee { get; set; }
        [MaxLength(100, ErrorMessage = "Value is too long")]
        [Required]
        public string value { get; set; }
        public virtual ICollection<ProductAttributeValue> ProductAttributeValue { get; set; }
    }
}
