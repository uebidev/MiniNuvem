using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace SalvaTxtWpf
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
        string path;

        private void BtnSaveInText_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(@"C:\Users\uebid\txt.txt", txtTexto.Text);
        }

        private void BtnLoadFromText_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\Users\uebid\Documents";
            path = openFileDialog.FileName;
            openFileDialog.Filter = "Arquivos de Texto (*.txt)|*.txt|Todos os arquivos (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                
                path = selectedFilePath;
            }

            string linha = "";
            using (StreamReader sr = new StreamReader(path))
            {
                List<string> list = new();
                while (!sr.EndOfStream)
                {
                    linha = sr.ReadLine();
                    txtTexto.Text += linha;
                }
                sr.Close();
            }
        }

        private void BtnAddClear_Click(object sender, RoutedEventArgs e)
        {
            txtTexto.Clear();
        }

        private void BtnSaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = @"C:\Users\uebid\Documents";
            saveFileDialog.Filter = "Arquivos de Texto (*.txt)|*.txt|Todos os arquivos (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = saveFileDialog.FileName;
                try
                {
                    using (StreamWriter sw = new StreamWriter(selectedFilePath))
                    {
                        sw.Write(txtTexto.Text);
                    }

                    MessageBox.Show($"Texto salvo com sucesso em {selectedFilePath}", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao salvar o texto: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
