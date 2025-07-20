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
        List<Order> GetAllOrders();
        Order? GetOrderById(int orderId);
        List<Order> GetOrdersByCustomerId(int customerId);
        bool CreateOrder(Order order);
        bool UpdateOrder(Order order);
        bool DeleteOrder(int orderId);
        List<Order> SearchOrders(string searchText);
        List<Order> GetOrdersByEmployeeId(int employeeId);
        List<Order> SearchOrdersByEmployeeId(string searchText, int employeeId);
        Order? GetLastOrderOfEmployee(int employeeId, int customerId);
    }
}
