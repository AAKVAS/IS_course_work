using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfApp1.Views;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views.Orders.OrdersReadyToReceive;
using System.Windows;

namespace WpfApp1.ViewModels.Orders
{
    /// <summary>
    /// Модель представления для раздела "Доставки / Доставки, готовые к получению".
    /// </summary>
    internal class OrdersReadyToReceiveViewModel : SectionWidgetWithImagesViewModel
    {
        /// <summary>
        /// Ссылка на окно работы с записью раздела. 
        /// </summary>
        private OrdersReadyToReceiveItemWithImages _itemForm;

        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as OrdersReadyToReceiveItemWithImages;
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
        /// Коллекция пунктов выдачи, используется для заполнения выпадающего списка в окне работы с записью раздела.
        /// </summary>
        public List<Models.Storages> PickUpPoints { get; set; }

        /// <summary>
        /// Коллекция статусов заказа, используется для заполнения выпадающего списка в окне работы с записью раздела.
        /// </summary>
        public List<OrderStatuses> Statuses { get; set; }

        /// <summary>
        /// Конструктор класса OrdersReadyToReceiveViewModel, в качестве параметра принимает ссылку на представление раздела.
        /// </summary>
        /// <param name="sectionWidget">Представление раздела.</param>
        public OrdersReadyToReceiveViewModel(SectionWidget sectionWidget) : base(sectionWidget) {}

        protected override void MakeCurrentItemEmpty()
        {
            CurrentItem = new OrderHistory();
        }

        protected override void CreateNewItemForm()
        {
            //Обновляем данные в выпадающих списках.
            Users = App.Context.Users.ToList();
            Products = App.Context.Products.ToList();
            PickUpPoints = StorageService.GetPickUpPoints();
            Statuses = OrderService.GetStatusesForReadyToRecieveOrder();

            _itemForm = new OrdersReadyToReceiveItemWithImages(this);
        }

        protected override void AddCurrentItem()
        {
            //Добавление новых записей невозможно.
            throw new NotImplementedException();
        }

	    protected override void DeleteCurrentItem()
        {
            //Удаление записей невозможно.
            throw new NotImplementedException();
        }

        public override void UpdateSectionData()
        {
            SectionData = OrderService.GetOrdersReadyToReceive();
        }

        protected override string GetErrors()
        {
            //Ошибок в записи быть не может.
            return new StringBuilder().ToString();
        }

        protected override dynamic CreateNewImage(byte[] image)
        { 
            ProductImage productImage = new();
            productImage.ProductImage1 = image;
            return productImage;
        }

        public override void LoadCurrentItemImages()
        {
            CurrentItemFromContext = OrderService.GetOrderHistoryWithProductImages(CurrentItem);
            CurrentItem = CurrentItemFromContext.Clone();
        }

        /// <summary>
        /// Метод, изменяющий текущий статус доставки.
        /// Явлется не обновлением существующего статуса, а добавлением нового статуса доставки.
        /// </summary>
        protected override void Update()
        {
            CurrentItemFromContext.Copy(CurrentItem);
            var entry = App.Context.Entry(CurrentItemFromContext);
            try
            {
                OrderService.ChangeOrderStatus(CurrentItemFromContext);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Изменение записи завершилось ошибкой");
            }
            finally
            {
                entry.Reload();
                MakeCurrentItemEmpty();
            }
            ItemForm.Close();
            UpdateItems();
        }
    }
}

