using MyStore.Domain.Abstract;
using MyStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Domain.Concrete
{
    public class EFCategory_ProductRepository : ICategory_ProductRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Category_Product> Categories_Products
        {
            get { return context.Categories_Products; }
        }
    }
}
