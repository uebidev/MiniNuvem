using Dominio.Entity;
using Newtonsoft.Json;
using Service.Path;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
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
    /// Lógica interna para UpdateProducts.xaml
    /// </summary>
    public partial class UpdateProducts : Window, INotifyPropertyChanged
    {
        public UpdateProducts()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private ObservableCollection<ProductWrapper> displayedProducts;

        private ProductWrapper product;
        public ProductWrapper Product
        {
            get { return product; }
            set { product = value; NotifyPropertyChanged(); }
        }


        private async void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            
                bool isAdded = await ProductService.UpdateProductAsync(long.Parse(txtId.Text), Product);

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