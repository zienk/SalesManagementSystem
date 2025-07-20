using DataAccessLayer.Entities;
using Services.Implementations;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly EmployeeService _employeeService;
        private readonly CustomerService _customerService;

        public LoginWindow()
        {
            InitializeComponent();
            _employeeService = new EmployeeService();
            _customerService = new CustomerService();
        }

        private void btnEmployeeLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password.Trim();

            var employee = _employeeService.GetEmployee(username, password);

            if (employee != null)
            {
                MainWindow mainWindow = new MainWindow(employee);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login failed!");
            }
        }

        private void btnCustomerLogin_Click(object sender, RoutedEventArgs e)
        {
            string phone = txtPhone.Text.Trim();

            var customer = _customerService.GetCustomerByPhone(phone);

            if (customer != null)
            {
                CustomerMainWindow customerMainWindow = new CustomerMainWindow(customer);
                customerMainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid phone number.", "Login failed!");
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
