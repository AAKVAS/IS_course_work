﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WpfApp1.Views;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views.Users.DefferedProducts;

namespace WpfApp1.ViewModels.Users
{
    internal class UserDefferedProductsViewModel : SectionWidgetWithImagesViewModel
    {
        private UserDefferedProductsItemWithImages _itemForm;
        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as UserDefferedProductsItemWithImages;
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


        private UserService _userService;
        private ProductService _productService;

        public List<Models.Users> Users { get; set; }
        public List<Models.Products> Products { get; set; }

        public UserDefferedProductsViewModel(SectionWidget sectionWidget) : base(sectionWidget) {
            _userService = App.UserService;
            _productService = App.ProductService;
            UpdateSectionData();
        }

        protected override void MakeCurrentItemEmpty()
        {
            _currentItem = new DeferredProducts();
        }

        protected override void CreateNewItemForm()
        {
            Users = App.Context.Users.ToList();
            Products = App.Context.Products.ToList();
            _itemForm = new UserDefferedProductsItemWithImages(this);
        }

        protected override void AddCurrentItem()
        {
            App.Context.DeferredProducts.Add(CurrentItem);
        }

        protected override void DeleteCurrentItem()
        {
            App.Context.DeferredProducts.Remove(CurrentItemFromContext);
        }

        public override void UpdateSectionData()
        {
            _sectionData = _userService.GetUserDeferredProducts();
        }

        protected override void FillItem()
        {
        }

        protected override string GetErrors()
        {
            StringBuilder errorBuilder = new StringBuilder();
            if (CurrentItem.User == null)
            {
                errorBuilder.AppendLine("Свойство \"Пользователь\" обязательно для заполнения;");
            }
            if (CurrentItem.Product == null)
            {
                errorBuilder.AppendLine("Свойство \"Товар\" обязательно для заполнения;");
            }
            return errorBuilder.ToString();
        }

        protected override dynamic CreateNewImage(byte[] image)
        {
            throw new Exception("Сохранение изображений в данном разделе не предусмотрено");
        }

        public override void LoadCurrentItemImages()
        {
            CurrentItemFromContext = _userService.GetDeferredProductsWithProductImages(CurrentItem as DeferredProducts);
            CurrentItem = CurrentItemFromContext.Clone();
        }

        public void LoadDefferedProductImages()
        {
            CurrentItem.Product = _productService.GetProductWithImages(CurrentItem.Product);
        }


    }
}
