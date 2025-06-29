using DataAccessLayer.Entities;
using Repositories.Implementations;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// - Service: chỉ đảm nhận nghiệp vụ và xác thực đầu vào, không bao giờ thao tác trực tiếp với DbContext.
// - validate early, fail fast: nghĩa là bạn kiểm tra và loại bỏ ngay các đầu vào không hợp lệ ngay tại điểm nhận dữ liệu, để phát hiện lỗi sớm và ngăn nó lan rộng xuống các tầng khác.

namespace Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;

        public ProductService()
        {
            _productRepo = new ProductRepository();
        }

        public bool CreateProduct(Product product)
        {
            if (product == null) return false;
            
            return _productRepo.CreateProduct(product);
        }

        public List<Product> GetAllProducts()      
            => _productRepo.GetAllProducts();
        

        public Product? GetProductById(int productId)
        {
            if (productId <= 0) return null;

            return _productRepo.GetProductById(productId);
        }

        public bool UpdateProduct(Product product)
        {
            if (product == null) return false;

            return _productRepo.UpdateProduct(product);
        }

        public bool DeleteProduct(int productId)
        {
            if (productId <= 0) return false;

            return _productRepo.DeleteProduct(productId);
        }

        public List<Product> SearchProduct(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return new List<Product>();

            return _productRepo.SearchProduct(searchText);
        }

    }
}
