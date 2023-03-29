using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1.Models
{
    public partial class DeferredProducts : INotifyPropertyChanged, ICopied<DeferredProducts>
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Id { get; set; }

        private Products _products;
        public virtual Products Product 
        {
            get => _products; 
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

        public virtual Users User { get; set; }

        public object Clone()
        {
            DeferredProducts deferredProducts = new DeferredProducts();
            deferredProducts.UserId = UserId;
            deferredProducts.ProductId = ProductId;
            deferredProducts.Product = Product;
            deferredProducts.User = User;
            deferredProducts.Id = Id;
            return deferredProducts;
        }

        public void Copy(DeferredProducts deferredProducts)
        {
            UserId = deferredProducts.UserId;
            ProductId = deferredProducts.ProductId;
            Id = deferredProducts.Id;
            Product = deferredProducts.Product;
            User = deferredProducts.User;
        }
    }
}