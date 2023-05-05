using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfApp1.Views;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views.Users.DefferedProducts;

namespace WpfApp1.ViewModels.Users
{
    /// <summary>
    /// Модель представления для раздела "Пользователи / Список товаров в корзине".
    /// </summary>
    internal class UserDefferedProductsViewModel : SectionWidgetWithImagesViewModel
    {
        /// <summary>
        /// Ссылка на окно работы с записью раздела. 
        /// </summary>
        private UserDefferedProductsItemWithImages _itemForm;

        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as UserDefferedProductsItemWithImages;
        }

        /// <summary>
        /// Коллекция пользователей, используется для заполнения выпадающего списка в окне работы с записью раздела.
        /// </summary>
        public List<Models.Users> Users { get; set; }

        /// <summary>
        /// Коллекция товаров, используется для заполнения выпадающего списка в окне работы с записью раздела.
        /// </summary>
        public List<Models.Products> Products { get; set; }

        /// <summary>
        /// Конструктор класса UserDefferedProductsViewModel, в качестве параметра принимает ссылку на представление раздела.
        /// </summary>
        /// <param name="sectionWidget">Представление раздела.</param>
        public UserDefferedProductsViewModel(SectionWidget sectionWidget) : base(sectionWidget) {}

        protected override void MakeCurrentItemEmpty()
        {
            CurrentItem = new DeferredProducts();
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
            SectionData = UserService.GetUserDeferredProducts();
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
            //Изображения в разделе можно только просматривать.
            throw new Exception("Сохранение изображений в данном разделе не предусмотрено");
        }

        public override void LoadCurrentItemImages()
        {
            CurrentItemFromContext = UserService.GetDeferredProductsWithProductImages(CurrentItem as DeferredProducts);
            CurrentItem = CurrentItemFromContext.Clone();
        }

        /// <summary>
        /// Метод, подтягивающий к текущей записи изображения товара.
        /// </summary>
        public void LoadDefferedProductImages()
        {
            CurrentItem.Product = ProductService.GetProductWithImages(CurrentItem.Product);
        }
    }
}
