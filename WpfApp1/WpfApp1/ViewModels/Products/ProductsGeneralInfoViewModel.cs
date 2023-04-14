using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WpfApp1.Views;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views.Products.GeneralInfo;
using System.Windows.Controls;

namespace WpfApp1.ViewModels.Products
{
    internal class ProductsGeneralInfoViewModel : SectionWidgetWithImagesViewModel
    {
        private ProductsGeneralInfoItemWithImages _itemForm;
        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as ProductsGeneralInfoItemWithImages;
        }

        private ObservableCollection<dynamic> _sectionData;
        public override ObservableCollection<dynamic> SectionData
        {
            get => _sectionData;
            set => _sectionData = value;
        }

        private dynamic _currentItem;
        public override dynamic? CurrentItem
        {
            get => _currentItem;
            set => _currentItem = value;
        }

        private ProductImage _image;
        public override dynamic CurrentImage 
        {
            get => _image;
            set => _image = value;
        }

        private ProductService _productService;

        public List<Models.Suppliers> Suppliers { get; set; }
        public List<Categories> Categories { get; set; }

        public ProductsGeneralInfoViewModel(SectionWidget sectionWidget) : base(sectionWidget) {
            _productService = App.ProductService;
            UpdateSectionData();
        }

        protected override void MakeCurrentItemEmpty()
        {
            _currentItem = new Models.Products();
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
            _sectionData = _productService.GetProductsGeneralInfo();
        }

        protected override void FillItem() {}

        protected override string GetErrors()
        {
            StringBuilder errorBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(CurrentItem.Title))
            {
                errorBuilder.AppendLine("Поле \"Название\" обязательно для заполнения;");
            }
            if (string.IsNullOrWhiteSpace(CurrentItem.Description))
            {
                errorBuilder.AppendLine("Поле \"Описание\" обязательно для заполнения;");
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
            CurrentItemFromContext = _productService.GetProductWithImages(CurrentItem);
            CurrentItem = CurrentItemFromContext.Clone();
        }

    }
}
