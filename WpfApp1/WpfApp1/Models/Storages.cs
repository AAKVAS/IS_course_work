﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WpfApp1.Models
{
    public partial class Storages
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