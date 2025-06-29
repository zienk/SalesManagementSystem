using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IOrderRepository
    {
        public List<Order> GetAllOrders();
        public Order? GetOrderById(int orderId);
        public List<Order> GetOrdersByCustomerId(int customerId);
        public bool CreateOrder(Order order);
        public bool UpdateOrder(Order order);
        public bool DeleteOrder(int orderId);
        public List<Order> SearchOrders(string searchText);
        public List<Order> GetOrdersByEmployeeId(int employeeId);
        public List<Order> SearchOrdersByEmployeeId(string searchText, int employeeId);
        public Order? GetLastOrderOfEmployee(int employeeId, int customerId);
    }
}
