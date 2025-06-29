using DataAccessLayer.Entities;
using Services.Implementations;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace WPF.SalesManagementSystem
{
    public partial class OrderHistoryWindow : Window
    {
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;
        private Employee _loggedInEmployee;

        public OrderHistoryWindow(Employee loggedInEmployee)
        {
            InitializeComponent();
            _loggedInEmployee = loggedInEmployee;
            txtWelcome.Text = $"Order History for {_loggedInEmployee.JobTitle}: {_loggedInEmployee.Name}";

            _orderService = new OrderService();
            _orderDetailService = new OrderDetailService();

            LoadOrders();
        }

        private void LoadOrders()
        {
            var orders = _orderService.GetOrdersByEmployeeId(_loggedInEmployee.EmployeeId)
                .Select(o => new
                {
                    o.OrderId,
                    o.Customer,
                    o.OrderDate,
                    TotalAmount = o.OrderDetails.Sum(od => od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount)),
                    o.OrderDetails
                }).ToList();
            lvOrder.ItemsSource = orders;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text;
            var orders = _orderService.SearchOrdersByEmployeeId(searchText, _loggedInEmployee.EmployeeId)
                .Select(o => new
                {
                    o.OrderId,
                    o.Customer,
                    o.OrderDate,
                    TotalAmount = o.OrderDetails.Sum(od => od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount)),
                    o.OrderDetails
                }).ToList();
            lvOrder.ItemsSource = orders;
        }

        private void lvOrder_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;
            dynamic selectedOrder = e.AddedItems[0];
            if (selectedOrder == null) return;
            var details = selectedOrder.OrderDetails as IEnumerable<OrderDetail>;
            lvOrderDetails.ItemsSource = details.Select(od => new
            {
                Product = od.Product,
                od.Quantity,
                od.UnitPrice,
                TotalPrice = od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount)
            }).ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(_loggedInEmployee);
            mainWindow.Show();
            this.Close();
        }
    }
} 