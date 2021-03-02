using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Domain.Entities
{
    public class Attributee
    {
        [Key]
        [DisplayName("Attribute Id")]
        public int id_attribute { get; set; }
        [DisplayName("Name")]
        [MaxLength(100, ErrorMessage = "Name is too long")]
        [Required]
        public string name { get; set; }

        public virtual ICollection<AttributeValue> AttributeValue { get; set; }
    }
}
