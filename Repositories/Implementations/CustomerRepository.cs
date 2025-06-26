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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly LucySalesDataContext _context;

        public CustomerRepository()
        {
            _context = new LucySalesDataContext();
        }

        public bool CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return true;
        }

        public List<Customer> GetAllCustomers()
            => _context.Customers.ToList();

        public Customer? GetCustomerById(int customerId)
            => _context.Customers
                .FirstOrDefault(c => c.CustomerId == customerId);

        public Customer? GetCustomerByPhone(string phone)
            => _context.Customers
                .FirstOrDefault(c => c.Phone == phone);

        public bool UpdateCustomer(Customer customer)
        {
            var existing = GetCustomerById(customer.CustomerId);
            if (existing == null) return false;

            _context.Entry(existing).CurrentValues.SetValues(customer);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteCustomer(int customerId)
        {
            var existing = GetCustomerById(customerId);
            if (existing == null) return false;

            _context.Customers.Remove(existing);
            _context.SaveChanges();
            return true;
        }

        public List<Customer> SearchCustomer(string searchText)
        {
            searchText = searchText.Trim().ToLower();

            return _context.Customers
                .Where(c => c.CompanyName.ToLower().Contains(searchText) ||
                            (c.ContactName != null && c.ContactName.ToLower().Contains(searchText)) ||
                            (c.Phone != null && c.Phone.ToLower().Contains(searchText)))
                .ToList();
        }
    }
}