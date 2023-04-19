﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WpfApp1.Models
{
    /// <summary>
    /// Модель, описывающая поставщика.
    /// </summary>
    public partial class Suppliers : ICopied<Suppliers>
    {
        public Suppliers()
        {
            Products = new HashSet<Products>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public virtual ICollection<Products> Products { get; set; }

        public Suppliers Clone()
        {
            Suppliers suppliers = new();
            suppliers.Id = Id;
            suppliers.Title = Title;
            suppliers.Products = Products;
            return suppliers;
        }

        public void Copy(Suppliers suppliers)
        {
            Id = suppliers.Id;
            Title = suppliers.Title;
            Products = suppliers.Products;
        }
    }
}