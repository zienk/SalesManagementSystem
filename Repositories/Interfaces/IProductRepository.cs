using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IProductRepository
    {
        public bool CreateProduct(Product product);
        public List<Product> GetAllProducts();
        public bool UpdateProduct(Product product);
        public bool DeleteProduct(int productId);
    }
}
