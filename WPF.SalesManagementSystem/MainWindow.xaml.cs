using DataAccessLayer.Entities;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF.SalesManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Employee _loggedInEmployee;

        public MainWindow(Employee employee)
        {
            InitializeComponent();

            _loggedInEmployee = employee;

            txtWelcome.Text = $"Welcome, {_loggedInEmployee.JobTitle}: {_loggedInEmployee.Name} !";

        }

        private void btnManageProducts_Click(object sender, RoutedEventArgs e)
        {
            ProductManagementWindow productWindow = new ProductManagementWindow(_loggedInEmployee);
            productWindow.Show();
            this.Close();
        }

        private void btnManageCustomers_Click(object sender, RoutedEventArgs e)
        {
            CustomerManagementWindow customerWindow = new CustomerManagementWindow(_loggedInEmployee);
            customerWindow.Show();
            this.Close();
        }

        private void btnManageOrders_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Order management will be implemented soon.", "Coming Soon", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void btnCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderCreateWindow createWindow = new OrderCreateWindow(_loggedInEmployee);
            createWindow.Show();
            this.Close();
        }

        private void btnOrderHistory_Click(object sender, RoutedEventArgs e)
        {
            OrderHistoryWindow historyWindow = new OrderHistoryWindow(_loggedInEmployee);
            historyWindow.Show();
            this.Close();
        }
    }
}
