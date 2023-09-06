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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SistemaDeVendasV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogar_Click(object sender, RoutedEventArgs e)
        {
            if (txtNome.Text == "admin" && txtSenha.Text == "123")
            {
                MessageBox.Show("Login efetuado com sucesso!");
                var f = new MenuPdv();
         
            }
            else
            {
                MessageBox.Show("Nome ou senha invalidos!", "Erro!");
                return;
            }
        }
    }
}
