using Dominio.Entities;
using Dominio.Entities.ProductValidation;
using FluentValidation;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Wrapper;
using System.Windows.Input;
using FastReport;
using System.Drawing;
using System.Data;
using Microsoft.Win32;
using System.IO;
using FastReport.Export.Csv;

namespace DummyAppV2
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<ProductWrapper> displayedProducts = new ObservableCollection<ProductWrapper>();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private ProductWrapper _product;

        public ProductWrapper Product
        {
            get { return _product; }
            set { _product = value; OnPropertyChanged(); }
        }

        private ProductWrapper _productEdit;

        public ProductWrapper ProductEdit
        {
            get { return _productEdit; }
            set { _productEdit = value; OnPropertyChanged(); }
        }
        public MainWindow()
        {
            InitializeComponent();
            LoadAllProducts();

        }

        private async void LoadAllProducts()
        {
            try
            {
                List<Product> products = await ProductService.GetAllAsync();
                foreach (var product in products)
                {
                    displayedProducts.Add(new ProductWrapper(product));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar produtos: " + ex.Message);
            }
            ProductsListView.ItemsSource = displayedProducts;
        }

        private async void BtnGet_Click(object sender, RoutedEventArgs e)
        {

            string searchText = TxtPesquisa.Text.Trim();
            displayedProducts.Clear();

            if (string.IsNullOrEmpty(searchText))
            {
                List<Product> products = await ProductService.GetAllAsync();
                foreach (var product in products)
                {
                    displayedProducts.Add(new ProductWrapper(product));
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
                    displayedProducts.Add(new ProductWrapper(product));
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
                    displayedProducts.Add(new ProductWrapper(product));
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

        //private async void DeleteProduct_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        Button deleteButton = sender as Button;

        //        if (deleteButton.DataContext is ProductWrapper selectedProduct)
        //        {
        //            long productId = selectedProduct.Id;

        //            bool isDeleted = await ProductService.DeleteProductAsync(productId);

        //            if (isDeleted)
        //            {
        //                displayedProducts.Remove(selectedProduct);

        //                MessageBox.Show("Produto excluído com sucesso!");
        //            }
        //            else
        //            {
        //                MessageBox.Show("Não foi possível excluir o produto.");
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Erro ao obter informações do produto.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Erro: " + ex.Message);
        //    }
        //} 

        //private void BtnModificar_Click(object sender, RoutedEventArgs e)
        //{
        //    pnlJanelinha.Visibility = Visibility.Visible;
        //    ProductEdit = (ProductWrapper)Product.Clone();
        //    EditOverlay.Visibility = Visibility.Visible;
        //    lblEditar.Visibility = Visibility.Visible;
        //    BtnGet.Visibility = Visibility.Hidden;
        //    BtnSalvar.Visibility = Visibility.Visible;
        //}

        private void BtnAdicionarJanela_Click(object sender, RoutedEventArgs e)
        {
            pnlJanelinha.Visibility = Visibility.Visible;
            ProductEdit = null;
            Product = null;
            EditOverlay.Visibility = Visibility.Visible;
            lblAdd.Visibility = Visibility.Visible;
            BtnGet.Visibility = Visibility.Hidden;
            BtnSalvar.Visibility = Visibility.Visible;
        }

        public async void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {

            if (lblEditar.Visibility == Visibility.Visible)
            {
                BtnSalvar.Visibility = Visibility.Hidden;
                try
                {

                    ProductWrapper p = ProductEdit;



                    if (p.Title == "")
                    {
                        MessageBox.Show("Titulo nulo");
                        return;
                    }

                    //var validation = new ProductValidation();
                    //    validation.Validate(p, p =>
                    //{
                    //    p.IncludeRuleSets();
                    //    p.ThrowOnFailures();
                    //});

                    Product isAdded = await ProductService.UpdateProductAsync(ProductEdit.Id, new ProductWrapper(p));
                    if (isAdded != null)
                    {

                        int count = ProductsListView.SelectedIndex;
                        displayedProducts.RemoveAt(ProductsListView.SelectedIndex);
                        displayedProducts.Insert(count, p);
                        p.IsModified = true;
                        EditOverlay.Visibility = Visibility.Hidden;
                        lblEditar.Visibility = Visibility.Hidden;
                        pnlJanelinha.Visibility = Visibility.Hidden;
                    }

                    else
                    {
                        MessageBox.Show("Não foi possível Modificar o novo produto.");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }

            }
            else if (lblAdd.Visibility == Visibility.Visible)
            {
                BtnSalvar.Visibility = Visibility.Hidden;
                try
                {
                    Product p = new Product();
                    {
                        p.Id = displayedProducts.Count + 1;
                        p.Title = txtTitle.Text;
                        p.Price = double.Parse(TxtPreco.Text);
                    };
                    var validation = new ProductValidation();
                    validation.Validate(p, p =>
                    {
                        p.IncludeRuleSets();
                        p.ThrowOnFailures();
                    });
                    Product isAdded = await ProductService.AddProductAsync(new ProductWrapper(p));

                    if (isAdded != null)
                    {

                        displayedProducts.Add(new ProductWrapper(p));
                        displayedProducts.Last().IsAdd = true;
                        EditOverlay.Visibility = Visibility.Hidden;
                        lblAdd.Visibility = Visibility.Hidden;
                        pnlJanelinha.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        MessageBox.Show("Não foi possível adicionar o novo produto.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
            }
        }

        private void BtnFechar_Click(object sender, RoutedEventArgs e)
        {
            EditOverlay.Visibility = Visibility.Hidden;
            lblAdd.Visibility = Visibility.Hidden;
            lblEditar.Visibility = Visibility.Hidden;
            BtnGet.Visibility = Visibility.Visible;
            pnlJanelinha.Visibility = Visibility.Hidden;
        }

        //private void btnImprimir_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        List<ProductWrapper> products = displayedProducts.ToList();

        //        using (Report report = new Report())
        //        {
        //            report.Load(@"C:\Users\uebid\source\repos\DummyAppV2\Dominio\Entities\Uproduct3.frx");
        //            report.RegisterData(products, "ProductRef");
        //            Color zebrastripColor = Color.White;


        //            report.Prepare();
        //            report.ShowPrepared();
        //            foreach (Base band in report.AllObjects)
        //            {
        //                if (band is DataBand dataBand)
        //                {
        //                    dataBand.Fill = new SolidFill(zebrastripColor);
        //                    zebrastripColor = (zebrastripColor == Color.White) ? Color.LightGray : Color.White;
        //                }
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Erro ao gerar o relatório: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);

        //    }

        //}
        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<ProductWrapper> products = displayedProducts.ToList();

                using (Report report = new Report())
                {
                    report.Load(@"C:\Users\uebid\source\repos\DummyAppV2\Dominio\Entities\Uproduct3.frx");
                    report.RegisterData(products, "ProductRef");


                    Color zebrastripColor = Color.White;

                    DataBand dataBand = report.FindObject("Data1") as DataBand;


                    dataBand.BeforePrint += (sender, args) =>
                    {
                        DataBand currentBand = sender as DataBand;
                        currentBand.Fill = new SolidFill(zebrastripColor);
                        zebrastripColor = (zebrastripColor == Color.White) ? Color.LightGray : Color.White;
                    };

                    report.Prepare();
                    report.ShowPrepared();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gerar o relatório: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void KeydownEnter(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter && lblEditar.Visibility == Visibility.Visible || e.Key == Key.Enter && lblAdd.Visibility == Visibility.Visible)
            {

                BtnSalvar_Click(sender, e);
            }
            if (e.Key == Key.Enter && pnlJanelinha.Visibility == Visibility.Hidden)
            {
                BtnGet_Click(sender, e);
            }

        }

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is FrameworkElement sourceElement)
            {

                ContextMenu menu = new ContextMenu();

                MenuItem editarMenuItem = new MenuItem();
                editarMenuItem.Header = "Editar";
                editarMenuItem.Click += EditarMenuItem_Click;
                menu.Items.Add(editarMenuItem);

                MenuItem excluirMenuItem = new MenuItem();
                excluirMenuItem.Header = "Excluir";
                excluirMenuItem.Click += ExcluirMenuItem_Click;
                menu.Items.Add(excluirMenuItem);

                menu.PlacementTarget = sourceElement;
                menu.IsOpen = true;
            }

        }

        private async void ExcluirMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsListView.SelectedItem is ProductWrapper selectedProduct)
            {
                try
                {
                    long productId = selectedProduct.Id;

                    bool isDeleted = await ProductService.DeleteProductAsync(productId);

                    if (isDeleted)
                    {
                        displayedProducts.Remove(selectedProduct);
                        MessageBox.Show("Produto excluído com sucesso!");
                    }
                    else
                    {
                        MessageBox.Show("Não foi possível excluir o produto.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
            }
        }

        private void EditarMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsListView.SelectedItem is ProductWrapper selectedProduct)
            {
                pnlJanelinha.Visibility = Visibility.Visible;
                ProductEdit = (ProductWrapper)selectedProduct.Clone();
                EditOverlay.Visibility = Visibility.Visible;
                lblEditar.Visibility = Visibility.Visible;
                BtnGet.Visibility = Visibility.Hidden;
                BtnSalvar.Visibility = Visibility.Visible;
            }
        }

        private void btnExportExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Visible = true;

                Microsoft.Office.Interop.Excel.Workbook workbook = excelApp.Workbooks.Add();
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

                for (int col = 0; col < ProductsListView.Columns.Count; col++)
                {
                    worksheet.Cells[1, col + 1] = ProductsListView.Columns[col].Header;
                }


                for (int row = 0; row < ProductsListView.Items.Count; row++)
                {
                    var product = (Wrapper.ProductWrapper)ProductsListView.Items[row];
                    worksheet.Cells[row + 2, 1] = product.Id; 
                    worksheet.Cells[row + 2, 2] = product.Title;
                    worksheet.Cells[row + 2, 3] = product.Price.ToString();
                    worksheet.Cells[row + 2, 4] = product.Description;
                }


                workbook.SaveAs(@"C:\Users\uebid\Documents\TesteExcel\arquivo.xlsx");


                excelApp.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao exportar para o Excel: " + ex.Message);
            }
            //ProductsListView.SelectAllCells();
            //ProductsListView.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            //ApplicationCommands.Copy.Execute(null, ProductsListView);
            ////CSVExport result1 = (CSVExport)Clipboard.GetData(DataFormats.Text);
            //string result2 = (string)Clipboard.GetData(DataFormats.Text);
            //ProductsListView.UnselectAllCells();

            //string path = @"C:\Users\uebid\Documents\TesteExcel\teste.xlsx";
            //using (StreamWriter sw = new StreamWriter(path))
            //{
            //    try
            //    {
            //        sw.WriteLine(result2.Replace(',', ' '));
            //        sw.Close();
            //    }
            //    catch (Exception a)
            //    {
            //        MessageBox.Show(a.Message);
            //    }

            //}
        }
    }
}