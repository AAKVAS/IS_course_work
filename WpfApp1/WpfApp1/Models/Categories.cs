﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WpfApp1.Models
{
    public partial class Categories
    {
        public Categories()
        {
            InverseParentCategory = new HashSet<Categories>();
            Products = new HashSet<Products>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int? ParentCategoryId { get; set; }

        public virtual Categories ParentCategory { get; set; }
        public virtual ICollection<Categories> InverseParentCategory { get; set; }
        public virtual ICollection<Products> Products { get; set; }
    }
}