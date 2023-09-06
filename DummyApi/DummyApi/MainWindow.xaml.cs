using Dominio.Entity;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Wrapper;

namespace DummyApi
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<ProductWrapper> displayedProducts = new ObservableCollection<ProductWrapper>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnGet_Click(object sender, RoutedEventArgs e)
        {
            string searchText = TxtPesquisa.Text.Trim();
            displayedProducts.Clear(); // Limpa a lista de produtos exibidos

            if (string.IsNullOrEmpty(searchText))
            {
                List<Product> products = await ProductService.GetAllAsync();
                foreach (var product in products)
                {
                    displayedProducts.Add(new ProductWrapper(product)); // Usando ProductWrapper aqui
                }
            }
            else if (int.TryParse(searchText, out int productId))
            {
                await SearchById(productId);
                ProductsListView.ItemsSource = displayedProducts;
            }
            else
            {
                List<Product> products = await ProductService.GetNameAsync(searchText);
                foreach (var product in products)
                {
                    displayedProducts.Add(new ProductWrapper(product)); // Usando ProductWrapper aqui
                }
            }
            ProductsListView.ItemsSource = displayedProducts;
        }

        private async Task SearchById(int productId)
        {
            try
            {
                Product product = await ProductService.GetByIdAsync(productId);

                if (product != null)
                {
                    displayedProducts.Add(new ProductWrapper(product)); // Usando ProductWrapper aqui
                }
                else
                {
                    MessageBox.Show("Produto não encontrado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void btnPostProduct_Click(object sender, RoutedEventArgs e)
        {
            AddProduct p = new AddProduct();
            p.ShowDialog();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            UpdateProducts p = new UpdateProducts();
            p.ShowDialog();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsListView.SelectedItem == null) return;
            DeleteProduct p = new DeleteProduct((ProductWrapper)ProductsListView.SelectedItem);
            p.ShowDialog();
        }
    }
}
    