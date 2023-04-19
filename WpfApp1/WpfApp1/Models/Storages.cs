using System.Collections.Generic;

namespace WpfApp1.Models
{
    /// <summary>
    /// Модель, описывающая склад.
    /// </summary>
    public partial class Storages : ICopied<Storages>
    {
        public Storages()
        {
            OrderHistory = new HashSet<OrderHistory>();
            Orders = new HashSet<Orders>();
            ProductsOnStorages = new HashSet<ProductsOnStorages>();
            ReceiptOfProductsToStorages = new HashSet<ReceiptOfProductsToStorages>();
            StorageWorkerShifts = new HashSet<StorageWorkerShifts>();
        }

        public int Id { get; set; }
        public string Country { get; set; } = null!;
        public string FederalSubject { get; set; } = null!;
        public string Locality { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string HouseNumber { get; set; } = null!;
        public int? StorageType { get; set; }

        public virtual StorageTypes StorageTypeNavigation { get; set; }
        public virtual ICollection<OrderHistory> OrderHistory { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<ProductsOnStorages> ProductsOnStorages { get; set; }
        public virtual ICollection<ReceiptOfProductsToStorages> ReceiptOfProductsToStorages { get; set; }
        public virtual ICollection<StorageWorkerShifts> StorageWorkerShifts { get; set; }

        public Storages Clone()
        {
            Storages storages = new Storages();
            storages.Id = Id;
            storages.Country = Country;
            storages.FederalSubject = FederalSubject;
            storages.Locality = Locality;
            storages.Street = Street;
            storages.HouseNumber = HouseNumber;
            storages.StorageType = StorageType;
            storages.StorageTypeNavigation = StorageTypeNavigation;
            storages.OrderHistory = OrderHistory;
            storages.Orders = Orders;
            storages.ProductsOnStorages = ProductsOnStorages;
            storages.ReceiptOfProductsToStorages = ReceiptOfProductsToStorages;
            storages.StorageWorkerShifts = StorageWorkerShifts;
            return storages;
        }

        public void Copy(Storages storages)
        {
            Id = storages.Id;
            Country = storages.Country;
            FederalSubject = storages.FederalSubject;
            Locality = storages.Locality;
            Street = storages.Street;
            HouseNumber = storages.HouseNumber;
            StorageType = storages.StorageType;
            StorageTypeNavigation = storages.StorageTypeNavigation;
            OrderHistory = storages.OrderHistory;
            Orders = storages.Orders;
            ProductsOnStorages = storages.ProductsOnStorages;
            ReceiptOfProductsToStorages = storages.ReceiptOfProductsToStorages;
            StorageWorkerShifts = storages.StorageWorkerShifts;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj is not Storages)
            {
                return false;
            }
            return (obj as Storages).Id == Id;
        }
    }
}