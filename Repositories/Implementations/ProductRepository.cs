using DataAccessLayer.Entities;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly LucySalesDataContext _context;

        public ProductRepository()
        {
            _context = new LucySalesDataContext();
        }

        public bool CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }
        public List<Product> GetAllProducts()
        {
            throw new NotImplementedException();
        }
        
        public bool UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
        public bool DeleteProduct(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
