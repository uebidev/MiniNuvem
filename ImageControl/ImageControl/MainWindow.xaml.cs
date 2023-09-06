﻿using Microsoft.Win32;
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

namespace ImageControl
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

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            openFileDialog.Title = "procure uma imagem";
            openFileDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            txtPath.Text = openFileDialog.FileName;
        }

        private void btnLoadImage_Click(object sender, RoutedEventArgs e)
        {
            Uri image_path = new Uri(txtPath.Text);
            imgImagem.Source = new BitmapImage(image_path);
        }

        private void btnFill_Click(object sender, RoutedEventArgs e)
        {
            imgImagem.Stretch = Stretch.Fill;

        }

        private void btnZoom_Click(object sender, RoutedEventArgs e)
        {
            imgImagem.Stretch = Stretch.Uniform;
        }
    }
}
