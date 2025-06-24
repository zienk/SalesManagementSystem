using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Entities
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime OrderDate { get; set; }

        // Navigation
        public Customer? Customer { get; set; }
        public Employee? Employee { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = new();
    }
}
