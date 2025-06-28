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
    /// Interaction logic for CustomerMainWindow.xaml
    /// </summary>
    public partial class CustomerMainWindow : Window
    {
        private readonly ICustomerService _customerService;
        private Customer _loggedInCustomer;

        public CustomerMainWindow(Customer customer)
        {
            InitializeComponent();

            _loggedInCustomer = customer;
            _customerService = new CustomerService();

            txtWelcome.Text = $"Welcome, {_loggedInCustomer.CompanyName}!";

            // Khởi tạo dữ liệu cho form chỉnh sửa thông tin
            LoadCustomerData();
        }

        private void LoadCustomerData()
        {
            txtCompanyName.Text = _loggedInCustomer.CompanyName;
            txtContactName.Text = _loggedInCustomer.ContactName;
            txtContactTitle.Text = _loggedInCustomer.ContactTitle;
            txtAddress.Text = _loggedInCustomer.Address;
            txtPhone.Text = _loggedInCustomer.Phone;
        }

        private void btnViewOrders_Click(object sender, RoutedEventArgs e)
        {
            // Hiển thị lịch sử đơn hàng
            gridProfile.Visibility = Visibility.Collapsed;
            lvOrders.Visibility = Visibility.Visible;

            // Lấy danh sách đơn hàng của khách hàng
            lvOrders.ItemsSource = _loggedInCustomer.Orders.ToList();
        }

        private void btnEditProfile_Click(object sender, RoutedEventArgs e)
        {
            // Hiển thị form chỉnh sửa thông tin
            lvOrders.Visibility = Visibility.Collapsed;
            gridProfile.Visibility = Visibility.Visible;
        }

        private void btnSaveProfile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Cập nhật thông tin khách hàng
                _loggedInCustomer.CompanyName = txtCompanyName.Text;
                _loggedInCustomer.ContactName = txtContactName.Text;
                _loggedInCustomer.ContactTitle = txtContactTitle.Text;
                _loggedInCustomer.Address = txtAddress.Text;
                _loggedInCustomer.Phone = txtPhone.Text;

                bool success = _customerService.UpdateCustomer(_loggedInCustomer);
                if (success)
                {
                    MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Failed to update profile. Please try again.", "Error", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK);
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            // Hiển thị thông báo xác nhận đăng xuất
            MessageBoxResult result = MessageBox.Show("Do you want to logout", "Yes or No?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Mở cửa sổ đăng nhập
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();

                // Đóng cửa sổ hiện tại
                this.Close();
            }
        }
    }
}
