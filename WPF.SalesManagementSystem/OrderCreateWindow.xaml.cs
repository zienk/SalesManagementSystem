using DataAccessLayer.Entities;
using Services.Implementations;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace WPF.SalesManagementSystem
{
    // View model cho hiển thị chi tiết đơn hàng
    public class OrderDetailView
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public partial class OrderCreateWindow : Window
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        private Employee _loggedInEmployee;
        private List<OrderDetail> _orderDetailsTemp = new List<OrderDetail>();

        public OrderCreateWindow(Employee loggedInEmployee)
        {
            InitializeComponent();
            _loggedInEmployee = loggedInEmployee;
            txtEmployeeName.Text = _loggedInEmployee.Name;
            txtWelcome.Text = $"Create Order for {_loggedInEmployee.JobTitle}: {_loggedInEmployee.Name}";

            _orderService = new OrderService();
            _customerService = new CustomerService();
            _productService = new ProductService();

            LoadCustomerList();
            LoadProductList();
        }

        // Load danh sách khách hàng vào ComboBox
        private void LoadCustomerList()
        {
            cboCustomer.ItemsSource = _customerService.GetAllCustomers();
        }

        // Load danh sách sản phẩm vào ComboBox
        private void LoadProductList()
        {
            cboProduct.ItemsSource = _productService.GetAllProducts();
        }

        // Thêm sản phẩm vào đơn hàng tạm
        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            AddProductToOrder();
        }
        private void AddProductToOrder()
        {
            var selectedProduct = cboProduct.SelectedItem as Product;
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
                    Quantity = (short)quantity,
                    UnitPrice = selectedProduct.UnitPrice ?? 0,
                    Discount = 0
                });
            }
            UpdateOrderDetailsListView();
            txtQuantity.Text = "";
        }

        // Hiển thị danh sách sản phẩm đã chọn lên ListView
        private void UpdateOrderDetailsListView()
        {
            var viewList = _orderDetailsTemp.Select(od => new OrderDetailView
            {
                ProductName = od.Product?.ProductName ?? "",
                Quantity = od.Quantity,
                UnitPrice = od.UnitPrice,
                TotalPrice = od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount)
            }).ToList();
            lvOrderDetails.ItemsSource = null;
            lvOrderDetails.ItemsSource = viewList;
        }

        // Hiện form thêm khách hàng mới
        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            ShowNewCustomerPanel();
        }
        private void ShowNewCustomerPanel()
        {
            panelNewCustomer.Visibility = Visibility.Visible;
        }
        // Ẩn form thêm khách hàng mới
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            HideNewCustomerPanel();
        }
        private void HideNewCustomerPanel()
        {
            panelNewCustomer.Visibility = Visibility.Collapsed;
            ResetNewCustomerForm();
        }
        // Reset các trường nhập khách hàng mới
        private void ResetNewCustomerForm()
        {
            txtNewCompanyName.Text = "";
            txtNewContactName.Text = "";
            txtNewContactTitle.Text = "";
            txtNewAddress.Text = "";
            txtNewPhone.Text = "";
        }
        // Lưu khách hàng mới
        private void btnSaveCustomer_Click(object sender, RoutedEventArgs e)
        {
            SaveNewCustomer();
        }
        private void SaveNewCustomer()
        {
            var newCustomer = new Customer
            {
                CompanyName = txtNewCompanyName.Text,
                ContactName = txtNewContactName.Text,
                ContactTitle = txtNewContactTitle.Text,
                Address = txtNewAddress.Text,
                Phone = txtNewPhone.Text
            };
            if (string.IsNullOrWhiteSpace(newCustomer.CompanyName))
            {
                MessageBox.Show("Company Name is required!", "Error", MessageBoxButton.OK);
                return;
            }
            _customerService.CreateCustomer(newCustomer);
            LoadCustomerList();
            // Chọn customer vừa tạo (tìm theo tên và phone)
            var created = _customerService.GetAllCustomers().LastOrDefault(c => c.CompanyName == newCustomer.CompanyName && c.Phone == newCustomer.Phone);
            if (created != null)
                cboCustomer.SelectedValue = created.CustomerId;
            HideNewCustomerPanel();
        }

        // Tạo đơn hàng và chi tiết đơn hàng
        private void btnCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            CreateOrder();
        }
        private void CreateOrder()
        {
            if (_orderDetailsTemp.Count == 0)
            {
                MessageBox.Show("Please add at least one product to the order!", "Error", MessageBoxButton.OK);
                return;
            }
            // Lấy OrderId lớn nhất hiện tại và +1
            int newOrderId = 1;
            var allOrders = _orderService.GetAllOrders();
            if (allOrders != null && allOrders.Count > 0)
            {
                newOrderId = allOrders.Max(o => o.OrderId) + 1;
            }
            Order order = new Order();
            order.OrderId = newOrderId;
            order.CustomerId = cboCustomer.SelectedValue as int? ?? 0;
            order.EmployeeId = _loggedInEmployee.EmployeeId;
            order.OrderDate = dpOrderDate.SelectedDate ?? DateTime.Now;

            bool success = _orderService.CreateOrder(order);
            if (success)
            {
                // Không cần gọi GetLastOrderOfEmployee nữa, dùng luôn newOrderId
                var orderDetailService = new OrderDetailService();
                foreach (var od in _orderDetailsTemp)
                {
                    od.OrderId = newOrderId;
                    orderDetailService.CreateOrderDetail(od);
                }
                _orderDetailsTemp.Clear();
                UpdateOrderDetailsListView();
                MessageBox.Show("Order and order details created successfully!", "Success", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Create order failed! Please try again! :<", "Error", MessageBoxButton.OK);
            }
        }

        // Quay lại MainWindow
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(_loggedInEmployee);
            mainWindow.Show();
            this.Close();
        }
    }
} 