﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WpfApp1.Models
{
    /// <summary>
    /// Модель, описывающая параметры товаров.
    /// </summary>
    public partial class ProductsParameters
    {
        public int ProductId { get; set; }
        public string ParameterTitle { get; set; } = null!;
        public string ParameterValue { get; set; } = null!;

        public virtual Products Product { get; set; }
    }
}