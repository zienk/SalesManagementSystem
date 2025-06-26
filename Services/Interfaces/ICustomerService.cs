using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomers();
        Customer? GetCustomerById(int customerId);
        Customer? GetCustomerByPhone(string phone);
        bool CreateCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        bool DeleteCustomer(int customerId);
        List<Customer> SearchCustomer(string searchText);
    }
}