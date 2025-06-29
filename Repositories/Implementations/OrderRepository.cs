using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {

        private readonly LucySalesDataContext _context;

        public OrderRepository()
        {
            _context = new LucySalesDataContext();
        }

        public bool CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAllOrders()
            => _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .ToList();

        public Order? GetOrderById(int orderId)
            => _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .FirstOrDefault(od => od.OrderId == orderId);
                
        public List<Order> GetOrdersByCustomerId(int customerId)
            => _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .Where(o => o.CustomerId == customerId)
                .ToList();

        public List<Order> GetOrdersByEmployeeId(int employeeId)
        {
            return _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .Where(o => o.EmployeeId == employeeId)
                .ToList();
        }

        public List<Order> SearchOrders(string searchText)
        {
            searchText = searchText.Trim().ToLower();

            return _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .Where(o => o.Customer.CompanyName.ToLower().Contains(searchText) ||
                            o.Customer.ContactName.ToLower().Contains(searchText) ||
                            o.Employee.Name.ToLower().Contains(searchText) ||
                            o.OrderDetails.Any(od => od.Product.ProductName.ToLower().Contains(searchText)))
                .ToList();
        }

        public bool UpdateOrder(Order order)
        {
            var existing = GetOrderById(order.OrderId);
            if (existing == null) return false;

            _context.Entry(existing).CurrentValues.SetValues(order);
            _context.SaveChanges();

            return true;
        }

        public List<Order> SearchOrdersByEmployeeId(string searchText, int employeeId)
        {
            searchText = searchText.Trim().ToLower();
            return _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .Where(o => o.EmployeeId == employeeId &&
                            (o.Customer.CompanyName.ToLower().Contains(searchText) ||
                             o.Customer.ContactName.ToLower().Contains(searchText) ||
                             o.Employee.Name.ToLower().Contains(searchText) ||
                             o.OrderDetails.Any(od => od.Product.ProductName.ToLower().Contains(searchText))))
                .ToList();
        }

        public Order? GetLastOrderOfEmployee(int employeeId, int customerId)
        {
            return _context.Orders
                .Where(o => o.EmployeeId == employeeId && o.CustomerId == customerId)
                .OrderByDescending(o => o.OrderId)
                .FirstOrDefault();
        }
    }
}
