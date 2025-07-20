using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IProductService
    {
        bool CreateProduct(Product product);
        List<Product> GetAllProducts();
        Product? GetProductById(int productId);
        bool UpdateProduct(Product product);
        bool DeleteProduct(int productId);
        List<Product> SearchProduct(string searchText);
    }
}
