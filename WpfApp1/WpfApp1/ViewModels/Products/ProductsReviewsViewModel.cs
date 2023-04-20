using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WpfApp1.Views;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views.Products.Reviews;
using System.Windows.Controls;
using ValidationLib;

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

        public List<Models.Products> Products { get; set; }
        public List<Models.Users> Users { get; set; }

        public ProductsReviewsViewModel(SectionWidget sectionWidget) : base(sectionWidget) {
            UpdateSectionData();
        }

        protected override void MakeCurrentItemEmpty()
        {
            CurrentItem = new Reviews();
        }

        protected override void CreateNewItemForm()
        {
            Products = App.Context.Products.ToList();
            Users = App.Context.Users.ToList();
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
            _sectionData = ProductService.GetProductsReviews();
        }

        protected override string GetErrors()
        {
            StringBuilder errorBuilder = new StringBuilder();
            int orderId = CurrentItem.OrderId;

            if (Validation.GetHasError((ItemForm as ProductsReviewsItemWithImages).tbId) || orderId <= 0)
            {
                errorBuilder.AppendLine("Поле \"Id заказа\" - положительное число;");
            }
            else if (_itemFormMode == ItemFormMode.Insert && ProductService.GetReviewByOrderId(orderId) != null)
            {
                errorBuilder.AppendLine("Такое значение \"Id заказа\" уже используется;");
            }
            if (!App.Context.Orders.Where(o => o.Id == orderId).Any())
            {
                errorBuilder.AppendLine("Заказа с таким \"Id заказа\" не найдено;");
            }
            if (!StringValidator.IsValid(CurrentItem.ReviewText))
            {
                errorBuilder.AppendLine("Свойство \"Отзыв\" обязательно для заполнения, максимальная длина - 255 символов;");
            }
            if (Validation.GetHasError((ItemForm as ProductsReviewsItemWithImages).tbStars) || CurrentItem.Stars <= 0 || CurrentItem.Stars > 5)
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
            CurrentItemFromContext = ProductService.GetReviewsWithImages(CurrentItem as Reviews);
            CurrentItem = CurrentItemFromContext.Clone();
        }

    }
}

