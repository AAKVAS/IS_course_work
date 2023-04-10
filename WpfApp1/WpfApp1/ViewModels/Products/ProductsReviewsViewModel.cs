using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WpfApp1.Views;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views.Products.Reviews;

namespace WpfApp1.ViewModels.Products
{
    internal class ProductsReviewsViewModel : SectionWidgetWithImagesViewModel
    {
        private ProductsReviewsItemWithImages _itemForm;
        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as ProductsReviewsItemWithImages;
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

        private ReviewImage _image;
        public override dynamic CurrentImage 
        {
            get => _image;
            set => _image = value;
        }

        private ProductService _productService;

        public List<Models.Products> Products;
        public List<Models.Users> Users;

        public ProductsReviewsViewModel(SectionWidget sectionWidget) : base(sectionWidget) {
            _productService = App.ProductService;
            UpdateSectionData();
            Products = App.Context.Products.ToList();
            Users = App.Context.Users.ToList();
        }

        protected override void MakeCurrentItemEmpty()
        {
            _currentItem = new Models.Reviews();
        }

        protected override void CreateNewItemForm()
        {
            _itemForm = new ProductsReviewsItemWithImages(this);
        }

        protected override void AddCurrentItem()
        {
            App.Context.Reviews.Add(CurrentItem);
        }

        protected override void DeleteCurrentItem()
        {
            App.Context.Reviews.Remove(CurrentItemFromContext);
        }

        public override void UpdateSectionData()
        {
            _sectionData = _productService.GetProductsReviews();
        }

        protected override void FillItem() {}

        protected override string GetErrors()
        {
            StringBuilder errorBuilder = new StringBuilder();

            if (CurrentItem.OrderId == 0)
            {
                errorBuilder.AppendLine("Поле \"Id заказа\" обязательно для заполнения;");
            }
            else if (_itemFormMode == ItemFormMode.Insert && _productService.GetReviewByOrderId(CurrentItem.OrderId) != null)
            {
                errorBuilder.AppendLine("Такое значение \"Id заказа\" уже используется;");
            }
            if (string.IsNullOrWhiteSpace(CurrentItem.ReviewText))
            {
                errorBuilder.AppendLine("Свойство \"Отзыв\" обязательно для заполнения;");
            }
            if (CurrentItem.Stars <= 0 || CurrentItem.Stars > 5)
            {
                errorBuilder.AppendLine("\"Оценка\" представляет из себя положительное число в диапазоне от 1 до 5 включительно;");
            }
            return errorBuilder.ToString();
        }

        protected override dynamic CreateNewImage(byte[] image)
        {
            ReviewImage productImage = new ReviewImage();
            productImage.Image = image;
            return productImage;
        }

        public override void LoadCurrentItemImages()
        {
            CurrentItemFromContext = _productService.GetReviewsWithImages(CurrentItem as Reviews);
            CurrentItem = CurrentItemFromContext.Clone();
        }

    }
}

