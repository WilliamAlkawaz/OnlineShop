using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Domain.Entities
{
    public class Image
    {
        [Key]
        public int id_image { get; set; }
        public int id_product { get; set; }
        public bool cover { get; set; }

        public virtual Product Product { get; set; }
    }
}
