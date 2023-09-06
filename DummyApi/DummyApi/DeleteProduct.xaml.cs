using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Wrapper;

namespace DummyApi
{
    /// <summary>
    /// Lógica interna para DeleteProduct.xaml
    /// </summary>
    public partial class DeleteProduct : Window, INotifyPropertyChanged
    {
        public DeleteProduct(ProductWrapper selectedItem)
        {
            InitializeComponent();
                  
        }

        private ProductWrapper product;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ProductWrapper Product
        {
            get { return product; }
            set { product = value;NotifyPropertyChanged(); }
        }

        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private async void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (long.TryParse(txtId.Text.Trim(), out long productId))
                {
                    bool isDeleted = await ProductService.DeleteProductAsync(productId);

                    if (isDeleted)
                    {
                        MessageBox.Show("Produto excluído com sucesso!");
                    }
                    else
                    {
                        MessageBox.Show("Não foi possível excluir o produto.");
                    }
                }
                else
                {
                    MessageBox.Show("ID de produto inválido.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }
    }
}
