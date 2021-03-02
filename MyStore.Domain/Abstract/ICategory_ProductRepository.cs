using MyStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Domain.Abstract
{
    public interface ICategory_ProductRepository
    {
        IQueryable<Category_Product> Categories_Products { get; }
    }
}
