using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WpfApp1.Views;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views.Orders.OrdersReadyToReceive;

namespace WpfApp1.ViewModels.Orders
{
    internal class OrdersReadyToReceiveViewModel : SectionWidgetWithImagesViewModel
    {
        private OrdersReadyToReceiveItemWithImages _itemForm;
        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as OrdersReadyToReceiveItemWithImages;
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

        private OrderService _orderService;
        private ProductService _productService;

        public List<Models.Users> Users;
        public List<Models.Products> Products;
        public ObservableCollection<Storages> PickUpPoints;

        public OrdersReadyToReceiveViewModel(SectionWidget sectionWidget) : base(sectionWidget) {
            _orderService = App.OrderService;
            _productService = App.ProductService;
            Users = App.Context.Users.ToList();
            Products = App.Context.Products.ToList();
            PickUpPoints = App.StorageService.GetPickUpPoints();
            UpdateSectionData();
        }

        protected override void MakeCurrentItemEmpty()
        {
            _currentItem = new OrderHistory();
        }

        protected override void CreateNewItemForm()
        {
            _itemForm = new OrdersReadyToReceiveItemWithImages(this);
        }

        protected override void AddCurrentItem()
        {
            throw new NotImplementedException();
        }

	    protected override void DeleteCurrentItem()
        {
            throw new NotImplementedException();
        }

        public override void UpdateSectionData()
        {
            _sectionData = _orderService.GetOrdersReadyToReceive();
        }

        protected override void FillItem()
        {
        }

        protected override string GetErrors()
        {
            StringBuilder errorBuilder = new StringBuilder();

            /*
            if (string.IsNullOrWhiteSpace(CurrentItem.Title))
            {
                errorBuilder.AppendLine("Поле \"Название\" обязательно для заполнения;");
            }
            if (CurrentItem.Price <= 0)
            {
                errorBuilder.AppendLine("\"Цена\" представляет из себя положительное число;");
            }
            if (string.IsNullOrWhiteSpace(CurrentItem.Description))
            {
                errorBuilder.AppendLine("Поле \"Описание\" обязательно для заполнения;");
            }
            if (CurrentItem.Category == null)
            {
                errorBuilder.AppendLine("Свойство \"Категория\" обязательно для заполнения;");
            }
            if (CurrentItem.Supplier == null)
            {
                errorBuilder.AppendLine("Свойство \"Поставщик\" обязательно для заполнения;");
            }
            if (CurrentItem.SupplierPercent <= 0 || CurrentItem.SupplierPercent >= 100)
            {
                errorBuilder.AppendLine("\"Процент поставщика с продажи\" представляет из себя положительное число в диапазоне от 0 до 100;");
            }
            */

            return errorBuilder.ToString();
        }

        protected override dynamic CreateNewImage(byte[] image)
        { 
            ProductImage productImage = new ProductImage();
            productImage.ProductImage1 = image;
            return productImage;
        }

        public override void LoadCurrentItemImages()
        {
            CurrentItemFromContext = _productService.GetProductWithImages(CurrentItem.Order.Product);
            CurrentItem = CurrentItemFromContext.Clone();
        }

    }
}

