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
using Dominio.Entity;
using Service;
using Services;
using Wrapper;

namespace DummyApi
{
    /// <summary>
    /// Lógica interna para AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        public AddProduct()
        {
            InitializeComponent();
        }
        private async void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product newProduct = new Product
                {
                    Id = long.Parse(txtId.Text),
                    Title = TxtTitle.Text,
                    Price = long.Parse(TxtPrice.Text)
                };
                bool isAdded = await ProductService.AddProductAsync(new ProductWrapper(newProduct));

                if (isAdded)
                {
                    MessageBox.Show("Novo produto adicionado com sucesso!");
                }
                else
                {
                    MessageBox.Show("Não foi possível adicionar o novo produto.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }
    }
}
