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
    /// Interaction logic for ProductManagementWindow.xaml
    /// </summary>
    public partial class ProductManagementWindow : Window
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductManagementWindow()
        {
            InitializeComponent();

            _productService = new ProductService();
            _categoryService = new CategoryService();
            LoadProducts();
            LoadCategories();
        }

        private void LoadCategories()
        {
            cboCategory.ItemsSource = _categoryService.GetAllCategories();
        }

        private void LoadProducts()
        {
            lvProduct.ItemsSource = _productService.GetAllProducts();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text;

            var result = _productService.SearchProduct(searchText);

            if (result != null && result.Count != 0)
            {
                lvProduct.ItemsSource = result;
            }
            else
            {
                MessageBox.Show("No products found!!!", "Search Result", MessageBoxButton.OK);
                LoadProducts();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Lấy product đang được chọn trong ListView
            var selected = lvProduct.SelectedItem as Product; // ép kiểu về Product
            if (selected == null)
            {
                MessageBox.Show("Please select product to delete!!!", "Eror", MessageBoxButton.OK);
                return;
            }

            // Xác nhận trước khi xóa
            MessageBoxResult comfirm = MessageBox.Show
                (
                    $"Do you want to delele Product: {selected.ProductName} ???"
                );
            if (comfirm == MessageBoxResult.No) return;

            try
            {
                bool success = _productService.DeleteProduct(selected.ProductId);
                if (success)
                {
                    LoadProducts();
                    LoadCategories();
                    MessageBox.Show(
                        "Product deleted successfully!",
                        "Success",
                        MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show(
                        "Delete product failed!. Please try again! :<",
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
            Product product = new Product();

            product.ProductName = txtName.Text;
            product.SupplierId = 1; // Set default
            product.CategoryId = cboCategory.SelectedValue as int?; // Lấy giá trị CategoryId từ ComboBox
            product.QuantityPerUnit = txtQpu.Text;
            product.UnitPrice = decimal.Parse(txtPrice.Text);
            product.UnitsInStock = int.Parse(txtQuantity.Text);
            product.UnitsOnOrder = 0; // Set default
            product.ReorderLevel = 0; // Set default
            product.Discontinued = false; // Set default

            bool success = _productService.CreateProduct(product);
            if (success)
            {
                LoadProducts();
                LoadCategories();
                MessageBox.Show("Product created successfully!", "Success", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Create product failed! Please try again! :<", "Error", MessageBoxButton.OK);

            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Lấy product đang được chọn trong ListView
                var selected = lvProduct.SelectedItem as Product; // ép kiểu về Product
                if (selected == null)
                {
                    MessageBox.Show("Please select product to update!!!", "Eror", MessageBoxButton.OK);
                    return;
                }
                // Cập nhật thông tin sản phẩm
                selected.ProductName = txtName.Text;
                selected.SupplierId = 1; // Set default
                selected.CategoryId = cboCategory.SelectedValue as int?; // Lấy giá trị CategoryId từ ComboBox
                selected.QuantityPerUnit = txtQpu.Text;
                selected.UnitPrice = decimal.Parse(txtPrice.Text);
                selected.UnitsInStock = int.Parse(txtQuantity.Text);
                selected.UnitsOnOrder = 0; // Set default
                selected.ReorderLevel = 0; // Set default
                bool success = _productService.UpdateProduct(selected);
                if (success)
                {
                    LoadProducts();
                    LoadCategories();
                    MessageBox.Show("Product updated successfully!", "Success", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Update product failed! Please try again! :<", "Error", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void lvProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return; // Người dùng chưa chọn dòng nào

            Product product = e.AddedItems[0] as Product; // Lấy sản phẩm đc chọn
            
            txtName.Text = product.ProductName;
            cboCategory.SelectedValue = product.CategoryId; // Set giá trị CategoryId cho ComboBox
            txtQpu.Text = product.QuantityPerUnit;
            txtPrice.Text = product.UnitPrice.ToString();
            txtQuantity.Text = product.UnitsInStock.ToString();

        }
    }
}