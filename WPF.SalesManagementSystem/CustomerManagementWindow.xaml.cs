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
    /// Interaction logic for CustomerManagementWindow.xaml
    /// </summary>
    public partial class CustomerManagementWindow : Window
    {
        private readonly ICustomerService _customerService;
        private Employee _loggedInEmployee;

        public CustomerManagementWindow(Employee loggedInEmployee)
        {
            InitializeComponent();

            _loggedInEmployee = loggedInEmployee;
            txtWelcome.Text = $"Welcome, {_loggedInEmployee.JobTitle}: {_loggedInEmployee.Name} !";

            _customerService = new CustomerService();
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            lvCustomer.ItemsSource = _customerService.GetAllCustomers();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text;

            var result = _customerService.SearchCustomer(searchText);

            if (result != null && result.Count != 0)
            {
                lvCustomer.ItemsSource = result;
            }
            else
            {
                MessageBox.Show("No customers found!!!", "Search Result", MessageBoxButton.OK);
                LoadCustomers();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Lấy customer đang được chọn trong ListView
            var selected = lvCustomer.SelectedItem as Customer; // ép kiểu về Customer
            if (selected == null)
            {
                MessageBox.Show("Please select customer to delete!!!", "Error", MessageBoxButton.OK);
                return;
            }

            // Xác nhận trước khi xóa
            MessageBoxResult confirm = MessageBox.Show
                (
                    $"Do you want to delete Customer: {selected.CompanyName} ???",
                    "Confirm Delete",
                    MessageBoxButton.YesNo
                );
            if (confirm == MessageBoxResult.No) return;

            try
            {
                bool success = _customerService.DeleteCustomer(selected.CustomerId);
                if (success)
                {
                    LoadCustomers();
                    MessageBox.Show(
                        "Customer deleted successfully!",
                        "Success",
                        MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show(
                        "Delete customer failed!. Please try again! :<",
                        "Error",
                        MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return;
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer customer = new Customer();

                customer.CompanyName = txtCompanyName.Text;
                customer.ContactName = txtContactName.Text;
                customer.ContactTitle = txtContactTitle.Text;
                customer.Address = txtAddress.Text;
                customer.Phone = txtPhone.Text;

                bool success = _customerService.CreateCustomer(customer);
                if (success)
                {
                    LoadCustomers();
                    MessageBox.Show("Customer created successfully!", "Success", MessageBoxButton.OK);
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Create customer failed! Please try again! :<", "Error", MessageBoxButton.OK);
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
                // Lấy customer đang được chọn trong ListView
                var selected = lvCustomer.SelectedItem as Customer; // ép kiểu về Customer
                if (selected == null)
                {
                    MessageBox.Show("Please select customer to update!!!", "Error", MessageBoxButton.OK);
                    return;
                }
                // Cập nhật thông tin khách hàng
                selected.CompanyName = txtCompanyName.Text;
                selected.ContactName = txtContactName.Text;
                selected.ContactTitle = txtContactTitle.Text;
                selected.Address = txtAddress.Text;
                selected.Phone = txtPhone.Text;

                bool success = _customerService.UpdateCustomer(selected);
                if (success)
                {
                    LoadCustomers();
                    MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Update customer failed! Please try again! :<", "Error", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void lvCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return; // Người dùng chưa chọn dòng nào

            Customer customer = e.AddedItems[0] as Customer; // Lấy khách hàng được chọn
            
            txtCompanyName.Text = customer.CompanyName;
            txtContactName.Text = customer.ContactName;
            txtContactTitle.Text = customer.ContactTitle;
            txtAddress.Text = customer.Address;
            txtPhone.Text = customer.Phone;
        }

        private void ClearFields()
        {
            txtCompanyName.Text = string.Empty;
            txtContactName.Text = string.Empty;
            txtContactTitle.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPhone.Text = string.Empty;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(_loggedInEmployee);
            mainWindow.Show();
            this.Close();
        }

    }
}