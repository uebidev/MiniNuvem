using System.ComponentModel;
using System.Runtime.CompilerServices;
using Dominio.Entities;
using Newtonsoft.Json;

namespace Wrapper
{
    public class ProductWrapper : Product, INotifyPropertyChanged, ICloneable
    {
        private Product _product; // Adicionado um campo para a instância de Product

        public ProductWrapper()
        {
            
        }
        public ProductWrapper(Product product)
        {
            _product = product; // Inicializa o campo com a instância de Product
        }
        private bool _isModified;
        public bool IsModified
        {
            get { return _isModified; }
            set { _isModified = value; NotifyPropertyChanged(); }
        }
        private bool _isAdd;
        public bool IsAdd
        {
            get { return _isAdd; }
            set { _isAdd = value; NotifyPropertyChanged(); }
        }

        public long Id
        {
            get { return _product.Id; }
            set { _product.Id = value; NotifyPropertyChanged(); }
        }

        public string Title
        {
            get { return _product.Title; }
            set { _product.Title = value; 
                
                NotifyPropertyChanged(); }
        }

        public double Price
        {
            get { return _product.Price; }
            set { _product.Price = value; NotifyPropertyChanged(); }
        }

        public string Description
        {
            get { return _product.Description; }
            set { _product.Description = value; NotifyPropertyChanged(); }
        }

        public Product Product { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public object Clone()
        {
            string productJson = JsonConvert.SerializeObject(this._product);
            return new ProductWrapper(JsonConvert.DeserializeObject<Product>(productJson));
        }

        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
