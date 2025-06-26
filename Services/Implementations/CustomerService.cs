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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomerService()
        {
            _customerRepo = new CustomerRepository();
        }

        public bool CreateCustomer(Customer customer)
        {
            if (customer == null) return false;
            
            return _customerRepo.CreateCustomer(customer);
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepo.GetAllCustomers();
        }

        public Customer? GetCustomerById(int customerId)
        {
            if (customerId <= 0) return null;

            return _customerRepo.GetCustomerById(customerId);
        }

        public Customer? GetCustomerByPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return null;

            return _customerRepo.GetCustomerByPhone(phone);
        }

        public bool UpdateCustomer(Customer customer)
        {
            if (customer == null) return false;

            return _customerRepo.UpdateCustomer(customer);
        }

        public bool DeleteCustomer(int customerId)
        {
            if (customerId <= 0) return false;

            return _customerRepo.DeleteCustomer(customerId);
        }

        public List<Customer> SearchCustomer(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return new List<Customer>();

            return _customerRepo.SearchCustomer(searchText);
        }
    }
}