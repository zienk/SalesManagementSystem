using DataAccessLayer.Entities;
using Repositories.Implementations;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;

        public OrderService()
        {
            _orderRepo = new OrderRepository();
        }

        public bool CreateOrder(Order order)
        {
            if (order == null) return false;

            return _orderRepo.CreateOrder(order);
        }

        public bool DeleteOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAllOrders()
            => _orderRepo.GetAllOrders();

        public Order? GetOrderById(int orderId)
        {
            if (orderId <= 0) return null;

            return _orderRepo.GetOrderById(orderId);
        }

        public List<Order>? GetOrdersByCustomerId(int customerId)
        {
            if (customerId <= 0) return null;

            return _orderRepo.GetOrdersByCustomerId(customerId);
        }

        public List<Order> GetOrdersByEmployeeId(int employeeId)
        {
            if (employeeId <= 0) return null;

            return _orderRepo.GetOrdersByEmployeeId(employeeId);
        }

        public List<Order> SearchOrders(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return null;

            return _orderRepo.SearchOrders(searchText);
        }

        public bool UpdateOrder(Order order)
        {
            if (order == null) return false;

            return _orderRepo.UpdateOrder(order);
        }
    }
}
