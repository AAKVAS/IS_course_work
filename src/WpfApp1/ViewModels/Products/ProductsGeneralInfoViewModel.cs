﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfApp1.Views;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views.Products.GeneralInfo;
using System.Windows.Controls;
using ValidationLib;

namespace WpfApp1.ViewModels.Products
{
    /// <summary>
    /// Модель представления для раздела "Товары / Общие сведения".
    /// </summary>
    internal class ProductsGeneralInfoViewModel : SectionWidgetWithImagesViewModel
    {
        /// <summary>
        /// Ссылка на окно работы с записью раздела. 
        /// </summary>
        private ProductsGeneralInfoItemWithImages _itemForm;

        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as ProductsGeneralInfoItemWithImages;
        }

        /// <summary>
        /// Коллекция поставщиков, используется для заполнения выпадающего списка в окне работы с записью раздела.
        /// </summary>
        public List<Models.Suppliers> Suppliers { get; set; }

        /// <summary>
        /// Коллекция категорий товаров, используется для заполнения выпадающего списка в окне работы с записью раздела.
        /// </summary>
        public List<Categories> Categories { get; set; }

        /// <summary>
        /// Конструктор класса ProductsGeneralInfoViewModel, в качестве параметра принимает ссылку на представление раздела.
        /// </summary>
        /// <param name="sectionWidget">Представление раздела.</param>
        public ProductsGeneralInfoViewModel(SectionWidget sectionWidget) : base(sectionWidget) {}

        protected override void MakeCurrentItemEmpty()
        {
            CurrentItem = new Models.Products();
        }

        protected override void CreateNewItemForm()
        {
            Suppliers = App.Context.Suppliers.ToList();
            Categories = App.Context.Categories.ToList();
            _itemForm = new ProductsGeneralInfoItemWithImages(this);
        }

        protected override void AddCurrentItem()
        {
            App.Context.Products.Add(CurrentItem);
        }

        protected override void DeleteCurrentItem()
        {
            App.Context.Products.Remove(CurrentItemFromContext);
        }

        public override void UpdateSectionData()
        {
            SectionData = ProductService.GetProductsGeneralInfo();
        }

        protected override string GetErrors()
        {
            StringBuilder errorBuilder = new StringBuilder();

            if (!StringValidator.IsValid(CurrentItem.Title))
            {
                errorBuilder.AppendLine("Поле \"Название\" обязательно для заполнения, максимальная длина - 255 символов;");
            }
            if (!StringValidator.IsValid(CurrentItem.Description))
            {
                errorBuilder.AppendLine("Поле \"Описание\" обязательно для заполнения, максимальная длина - 255 символов;");
            }
            if (Validation.GetHasError((ItemForm as ProductsGeneralInfoItemWithImages).tbPrice) || CurrentItem.Price <= 0)
            {
                errorBuilder.AppendLine("Поле \"Цена\" - положительное число;");
            }
            if (CurrentItem.Supplier == null)
            {
                errorBuilder.AppendLine("Свойство \"Поставщик\" обязательно для заполнения;");
            }
            if (CurrentItem.Category == null)
            {
                errorBuilder.AppendLine("Свойство \"Категория\" обязательно для заполнения;");
            }
            if (Validation.GetHasError((ItemForm as ProductsGeneralInfoItemWithImages).tbPercent) || CurrentItem.SupplierPercent <= 0 || CurrentItem.SupplierPercent >= 100)
            {
                errorBuilder.AppendLine("\"Процент поставщика с продажи\" представляет из себя положительное число в диапазоне от 0 до 100;");
            }

            return errorBuilder.ToString();
        }

        protected override dynamic CreateNewImage(byte[] image)
        { 
            ProductImage productImage = new ProductImage();
            productImage.Image = image;
            return productImage;
        }

        public override void LoadCurrentItemImages()
        {
            CurrentItemFromContext = ProductService.GetProductWithImages(CurrentItem);
            CurrentItem = CurrentItemFromContext.Clone();
        }
    }
}