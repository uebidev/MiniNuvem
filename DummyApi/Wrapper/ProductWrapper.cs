using Dominio.Entity;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Globalization;
namespace Wrapper
{
    public class ProductWrapper :Product, INotifyPropertyChanged
    {
        private Product _product; // Adicionado um campo para a instância de Product

        public ProductWrapper(Product product)
        {
            _product = product; // Inicializa o campo com a instância de Product
        }


        public long IId
        {
            get { return _product.Id; }
            set { _product.Id = value; NotifyPropertyChanged(); }
        }

        public string ITitle
        {
            get { return _product.Title; }
            set { _product.Title = value; NotifyPropertyChanged(); }
        }

        public double IPrice
        {
            get { return _product.Price; }
            set { _product.Price = value; NotifyPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
