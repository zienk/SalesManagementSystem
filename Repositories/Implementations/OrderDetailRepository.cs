using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class OrderDetailRepository : IOrderDetailRepository
    {

        private readonly LucySalesDataContext _context;

        public OrderDetailRepository()
        {
            _context = new LucySalesDataContext();
        }
        
        public OrderDetail? GetOrderDetailById(int orderDetailId)
        {
            if (orderDetailId <= 0) return null;
            return _context.OrderDetails
                .Include(od => od.Product)
                .Include(od => od.Order)
                .FirstOrDefault(od => od.OrderId == orderDetailId);
        }

        public bool CreateOrderDetail(OrderDetail orderDetail)
        { 
            _context.OrderDetails.Add(orderDetail);
            return _context.SaveChanges() > 0;
        }

        public List<OrderDetail> GetAllOrderDetails()
            => _context.OrderDetails
                .Include(od => od.Product)
                .Include(od => od.Order)
                .ToList();

        public List<OrderDetail> GetOrderDetailsByCustomerId(int customerID)
            => _context.OrderDetails
                .Include(od => od.Product)
                .Include(od => od.Order)
                .Where(od => od.Order.CustomerId == customerID)
                .ToList();

        public List<OrderDetail> GetOrderDetailsByOrderId(int orderId)
            => _context.OrderDetails
                .Include(od => od.Product)
                .Include(od => od.Order)
                .Where(od => od.OrderId == orderId)
                .ToList();
    }
}
