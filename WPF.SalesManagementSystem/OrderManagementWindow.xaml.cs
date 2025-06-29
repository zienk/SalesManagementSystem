using DataAccessLayer.Entities;
using Services.Implementations;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF.SalesManagementSystem
{
    /// <summary>
    /// Interaction logic for OrderManagementWindow.xaml
    /// </summary>
    public partial class OrderManagementWindow : Window
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        private Employee _loggedInEmployee;
        private List<OrderDetail> _orderDetailsTemp = new List<OrderDetail>();

        public OrderManagementWindow(Employee loggedInEmployee)
        {
            InitializeComponent();

            _loggedInEmployee = loggedInEmployee;
            txtWelcome.Text = $"Welcome, {_loggedInEmployee.JobTitle}: {_loggedInEmployee.Name} !";
            txtEmployeeName.Text = _loggedInEmployee.Name;

            _orderService = new OrderService();
            _customerService = new CustomerService();
            _productService = new ProductService();

            LoadOrders();
            LoadCustomers();
            LoadProducts();
        }

        private void LoadOrders()
        {
            lvOrder.ItemsSource = _orderService.GetOrdersByEmployeeId(_loggedInEmployee.EmployeeId);
        }

        private void LoadCustomers()
        {
            cboCustomer.ItemsSource = _customerService.GetAllCustomers();
        }

        private void LoadProducts()
        {
            cboProduct.ItemsSource = _productService.GetAllProducts();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text;
            var result = _orderService.SearchOrdersByEmployeeId(searchText, _loggedInEmployee.EmployeeId);
            if (result != null && result.Count != 0)
            {
                lvOrder.ItemsSource = result;
            }
            else
            {
                MessageBox.Show("No orders found!!!", "Search Result", MessageBoxButton.OK);
                LoadOrders();
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_orderDetailsTemp.Count == 0)
                {
                    MessageBox.Show("Please add at least one product to the order!", "Error", MessageBoxButton.OK);
                    return;
                }
                Order order = new Order();
                order.CustomerId = cboCustomer.SelectedValue as int? ?? 0;
                order.EmployeeId = _loggedInEmployee.EmployeeId;
                order.OrderDate = dpOrderDate.SelectedDate ?? DateTime.Now;

                bool success = _orderService.CreateOrder(order);
                if (success)
                {
                    // Lấy OrderId vừa tạo bằng service
                    var createdOrder = _orderService.GetLastOrderOfEmployee(_loggedInEmployee.EmployeeId, order.CustomerId);
                    if (createdOrder == null)
                    {
                        MessageBox.Show("Cannot find created order!", "Error", MessageBoxButton.OK);
                        return;
                    }
                    var orderDetailService = new OrderDetailService();
                    foreach (var od in _orderDetailsTemp)
                    {
                        od.OrderId = createdOrder.OrderId;
                        orderDetailService.CreateOrderDetail(od);
                    }
                    LoadOrders();
                    _orderDetailsTemp.Clear();
                    UpdateOrderDetailsListView();
                    MessageBox.Show("Order and order details created successfully!", "Success", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Create order failed! Please try again! :<", "Error", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = lvOrder.SelectedItem as Order;
                if (selected == null)
                {
                    MessageBox.Show("Please select order to update!!!", "Error", MessageBoxButton.OK);
                    return;
                }
                selected.CustomerId = cboCustomer.SelectedValue as int? ?? 0;
                selected.EmployeeId = _loggedInEmployee.EmployeeId;
                selected.OrderDate = dpOrderDate.SelectedDate ?? DateTime.Now;

                bool success = _orderService.UpdateOrder(selected);
                if (success)
                {
                    LoadOrders();
                    MessageBox.Show("Order updated successfully!", "Success", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Update order failed! Please try again! :<", "Error", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selected = lvOrder.SelectedItem as Order;
            if (selected == null)
            {
                MessageBox.Show("Please select order to delete!!!", "Error", MessageBoxButton.OK);
                return;
            }
            MessageBoxResult confirm = MessageBox.Show($"Do you want to delete Order ID: {selected.OrderId} ?", "Confirm Delete", MessageBoxButton.YesNo);
            if (confirm == MessageBoxResult.No) return;
            try
            {
                bool success = _orderService.DeleteOrder(selected.OrderId);
                if (success)
                {
                    LoadOrders();
                    MessageBox.Show("Order deleted successfully!", "Success", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Delete order failed! Please try again! :<", "Error", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void lvOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                UpdateOrderDetailsListView(); // Hiển thị danh sách tạm khi tạo mới
                txtEmployeeName.Text = _loggedInEmployee.Name;
                return;
            }
            Order order = e.AddedItems[0] as Order;
            if (order == null) return;
            cboCustomer.SelectedValue = order.CustomerId;
            txtEmployeeName.Text = order.Employee?.Name ?? _loggedInEmployee.Name;
            dpOrderDate.SelectedDate = order.OrderDate;

            // Hiển thị chi tiết đơn hàng đã lưu
            var orderDetailService = new OrderDetailService();
            var details = orderDetailService.GetOrderDetailsByOrderId(order.OrderId);
            lvOrderDetails.ItemsSource = details.Select(od => new
            {
                ProductName = od.Product?.ProductName,
                Quantity = od.Quantity,
                UnitPrice = od.UnitPrice,
                TotalPrice = od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount)
            }).ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(_loggedInEmployee);
            mainWindow.Show();
            this.Close();
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = cboProduct.SelectedItem as DataAccessLayer.Entities.Product;
            if (selectedProduct == null)
            {
                MessageBox.Show("Please select a product!", "Error", MessageBoxButton.OK);
                return;
            }
            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity!", "Error", MessageBoxButton.OK);
                return;
            }
            // Kiểm tra nếu đã có sản phẩm này thì cộng dồn số lượng
            var existing = _orderDetailsTemp.FirstOrDefault(od => od.ProductId == selectedProduct.ProductId);
            if (existing != null)
            {
                existing.Quantity += (short)quantity;
            }
            else
            {
                _orderDetailsTemp.Add(new OrderDetail
                {
                    ProductId = selectedProduct.ProductId,
                    Product = selectedProduct,
                    Quantity = (short)quantity,
                    UnitPrice = selectedProduct.UnitPrice ?? 0,
                    Discount = 0
                });
            }
            UpdateOrderDetailsListView();
        }

        private void UpdateOrderDetailsListView()
        {
            lvOrderDetails.ItemsSource = null;
            lvOrderDetails.ItemsSource = _orderDetailsTemp.Select(od => new
            {
                ProductName = od.Product?.ProductName,
                Quantity = od.Quantity,
                UnitPrice = od.UnitPrice,
                TotalPrice = od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount)
            }).ToList();
        }
    }
}
