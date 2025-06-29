using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IOrderDetailService
    {
        public List<OrderDetail> GetAllOrderDetails();
        public List<OrderDetail> GetOrderDetailsByOrderId(int orderId);
        public List<OrderDetail> GetOrderDetailsByCustomerId(int customerID);
        public bool CreateOrderDetail(OrderDetail orderDetail);
    }
}
