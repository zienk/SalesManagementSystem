using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using Repositories.Implementations;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepo;

        public OrderDetailService()
        {
            _orderDetailRepo = new OrderDetailRepository();
        }

        public bool CreateOrderDetail(OrderDetail orderDetail)
        {
            if (orderDetail == null) return false;

            return _orderDetailRepo.CreateOrderDetail(orderDetail);
        }

        public List<OrderDetail> GetAllOrderDetails()
            => _orderDetailRepo.GetAllOrderDetails();

        public List<OrderDetail> GetOrderDetailsByCustomerId(int customerID)
            => _orderDetailRepo.GetOrderDetailsByCustomerId(customerID);

        public List<OrderDetail> GetOrderDetailsByOrderId(int orderId)
            => _orderDetailRepo.GetOrderDetailsByOrderId(orderId);
    }
}
