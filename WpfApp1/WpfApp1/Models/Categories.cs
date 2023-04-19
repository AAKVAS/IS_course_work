﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfApp1.Models
{
    /// <summary>
    /// Модель, описывающая категории товаров.
    /// </summary>
    public partial class Categories : ICopied<Categories>
    {
        public Categories()
        {
            InverseParentCategory = new ObservableCollection<Categories>();
            Products = new ObservableCollection<Products>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int? ParentCategoryId { get; set; }

        public virtual Categories ParentCategory { get; set; }
        public virtual ObservableCollection<Categories> InverseParentCategory { get; set; }
        public virtual ObservableCollection<Products> Products { get; set; }

        [NotMapped]
        public string? ParentCategoryTitle
        {
            get => ParentCategory?.Title;
        }

        public Categories Clone()
        {
            Categories category = new Categories();
            category.Id = Id;
            category.Title = Title;
            category.ParentCategoryId = ParentCategoryId;
            category.ParentCategory = ParentCategory;
            category.Products = new ObservableCollection<Products>();
            category.InverseParentCategory = new ObservableCollection<Categories>(InverseParentCategory);
            return category;
        }

        public void Copy(Categories category)
        {
            Id = category.Id;
            Title = category.Title;
            ParentCategoryId = category.ParentCategoryId;
            ParentCategory = category.ParentCategory;
            Products = category.Products;
            InverseParentCategory = category.InverseParentCategory;
        }
    }
}