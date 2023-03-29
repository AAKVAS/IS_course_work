using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1.Models
{
    public partial class Products : INotifyPropertyChanged, ICopied<Products>
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public object Clone()
        {
            Products product = new Products();
            product.Id = Id;
            product.Title = Title;
            product.Price = Price;
            product.SupplierId = SupplierId;
            product.CategoryId = CategoryId;
            product.Description = Description;
            product.SupplierPercent = SupplierPercent;
            product.Category = Category;
            product.Supplier = Supplier;

            product.DeferredProducts = new List<DeferredProducts>(DeferredProducts);
            product.Orders = new List<Orders>(Orders);
            product.PriceHistory = new List<PriceHistory>(PriceHistory);
            product.ProductsOnStorages = new List<ProductsOnStorages>(ProductsOnStorages);
            product.ProductsParameters = new List<ProductsParameters>(ProductsParameters);
            product.ReceiptOfProductsToStorages = new List<ReceiptOfProductsToStorages>(ReceiptOfProductsToStorages);
            product.Images = new ObservableCollection<ProductImage>(Images);

            return product;
        }

        public void Copy(Products product)
        {
            Id = product.Id;
            Title = product.Title;
            Price = product.Price;
            SupplierId = product.SupplierId;
            CategoryId = product.CategoryId;
            Description = product.Description;
            SupplierPercent = product.SupplierPercent;
            Category = product.Category;
            Supplier = product.Supplier;

            DeferredProducts = product.DeferredProducts;
            Orders = product.Orders;
            PriceHistory = product.PriceHistory;
            ProductsOnStorages = product.ProductsOnStorages;
            ProductsParameters = product.ProductsParameters;
            ReceiptOfProductsToStorages = product.ReceiptOfProductsToStorages;
            Images = product.Images;
        }

        public Products()
        {
            DeferredProducts = new HashSet<DeferredProducts>();
            Orders = new HashSet<Orders>();
            PriceHistory = new HashSet<PriceHistory>();
            ProductsOnStorages = new HashSet<ProductsOnStorages>();
            ProductsParameters = new HashSet<ProductsParameters>();
            ReceiptOfProductsToStorages = new HashSet<ReceiptOfProductsToStorages>();
            Images = new ObservableCollection<ProductImage>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public double SupplierPercent { get; set; }

        public virtual Categories Category { get; set; }
        public virtual Suppliers Supplier { get; set; }
        public virtual ICollection<DeferredProducts> DeferredProducts { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<PriceHistory> PriceHistory { get; set; }
        public virtual ICollection<ProductsOnStorages> ProductsOnStorages { get; set; }
        public virtual ICollection<ProductsParameters> ProductsParameters { get; set; }
        public virtual ICollection<ReceiptOfProductsToStorages> ReceiptOfProductsToStorages { get; set; }
        public virtual ICollection<PriceHistory> PriceHistories { get; set; }

        private ObservableCollection<ProductImage> _images; 
        public virtual ObservableCollection<ProductImage> Images {
            get => _images;
            set
            {
                _images = value;
                OnPropertyChanged();
            }
        }
    }
}