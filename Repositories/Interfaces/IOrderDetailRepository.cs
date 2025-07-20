using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IOrderDetailRepository
    {

        List<OrderDetail> GetAllOrderDetails();
        List<OrderDetail> GetOrderDetailsByOrderId(int orderId);
        List<OrderDetail> GetOrderDetailsByCustomerId(int customerID);
        bool CreateOrderDetail(OrderDetail orderDetail);
    }
}
