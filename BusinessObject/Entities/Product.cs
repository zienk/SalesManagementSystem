using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Entities
{
    public class Product
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; } 
        public int CategoryID { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        

        // Navigation
        public Category? Category { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = new();
    }
}
