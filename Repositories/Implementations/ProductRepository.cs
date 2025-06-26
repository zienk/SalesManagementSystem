using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// - Repository: chỉ lo truy xuất dữ liệu, không chứa logic nghiệp vụ hay validation.
// - expose intentions, hide implementation: nghĩa là bạn chỉ công khai các hành động (method) mà service/repo cung cấp, còn cách nó hoạt động (triển khai, truy vấn, tối ưu) phải được ẩn đi để giữ code gọn gàng và dễ thay thế.

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
            _context.Products.Add(product);
            _context.SaveChanges();
            return true;
        }
        public List<Product> GetAllProducts()
            => _context.Products
                .Where(p => p.Discontinued == false)
                .Include(p => p.Category).ToList();

        public Product? GetProductById(int productId)
            => _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.ProductId == productId);

        public bool UpdateProduct(Product product)
        {
            var existing = GetProductById(product.ProductId);
            if (existing == null) return false;

            _context.Entry(existing).CurrentValues.SetValues(product);
            _context.SaveChanges();
            return true;
        }
        public bool DeleteProduct(int productId)
        {
            var existing = GetProductById(productId);
            if (existing == null) return false;

            existing.Discontinued = true;
            _context.SaveChanges();
            return true;
        }

        public List<Product> SearchProduct(string searchText)
        {
            searchText = searchText.Trim().ToLower();

            return _context.Products
                .Where(p => p.ProductName.ToLower().Contains(searchText) ||
                            p.Category.CategoryName.ToLower().Contains(searchText))
                .ToList();
        }

    }
}
