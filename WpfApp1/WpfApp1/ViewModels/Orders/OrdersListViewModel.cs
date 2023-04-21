using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfApp1.Views;
using WpfApp1.Services;
using WpfApp1.Views.Orders.OrdersList;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.ViewModels.Orders
{
    internal class OrdersListViewModel : SectionWidgetViewModel
    {
        private OrdersListItem _itemForm;
        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as OrdersListItem;
        }

        public List<Models.Users> Users { get; set; }
        public List<Models.Storages> PickUpPoints { get; set; }
        public List<Models.Products> Products { get; set; }

        public OrdersListViewModel(SectionWidget sectionWidget) : base(sectionWidget) {
            UpdateSectionData();
        }

        protected override void MakeCurrentItemEmpty()
        {
            CurrentItem = new Models.Orders();
        }

        protected override void CreateNewItemForm()
        {
            Users = App.Context.Users.ToList();
            PickUpPoints = StorageService.GetPickUpPoints();
            Products = App.Context.Products.ToList();
            _itemForm = new OrdersListItem(this);
        }

        protected override void AddCurrentItem()
        {
            App.Context.Orders.Add(CurrentItem);
        }

        protected override void DeleteCurrentItem()
        {
            App.Context.Orders.Remove(CurrentItemFromContext);
        }

        public override void UpdateSectionData()
        {
            SectionData = OrderService.GetOrdersList();
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
            if (Validation.GetHasError((ItemForm as OrdersListItem).tbProductCount)  || CurrentItem.ProductCount <= 0)
            {
                errorBuilder.AppendLine("\"Количество товаров\" представляет из себя положительное число;");
            }
            if (CurrentItem.PickUpPoint == null)
            {
                errorBuilder.AppendLine("Свойство \"Пункт выдачи\" обязательно для заполнения;");
            }
            if (CurrentItem.CreatedAt == null || CurrentItem.CreatedAt < new DateTime(1900, 1, 1) || CurrentItem.CreatedAt > new DateTime(3000, 12, 31))
            {
                errorBuilder.AppendLine("Свойство \"Дата формирования заказа\" обязательно для заполнения, допустимые значения от 1900.01.01 до 3000.12.31;");
            }
            if (CurrentItem.EstimatedDeliveryAt == null || CurrentItem.EstimatedDeliveryAt < new DateTime(1900, 1, 1) || CurrentItem.EstimatedDeliveryAt > new DateTime(3000, 12, 31))
            {
                errorBuilder.AppendLine("Свойство \"Ориентировочная дата выдачи\" обязательно для заполнения, допустимые значения от 1900.01.01 до 3000.12.31;");
            }
            if (CurrentItem.CreatedAt != null && CurrentItem.EstimatedDeliveryAt != null && CurrentItem.CreatedAt >= CurrentItem.EstimatedDeliveryAt)
            {
                errorBuilder.AppendLine("Значние свойства \"Дата формирования заказа\" должно быть меньше свойства\"Ориентировочная дата выдачи\";");
            }

            return errorBuilder.ToString();
        }

        protected override void Insert()
        {
            var entry = App.Context.Entry(CurrentItem);
            try
            {
                OrderService.InsertOrder(CurrentItem);
                ItemForm.Close();
                UpdateItems();
            }
            catch (Exception ex)
            {
                entry.Reload();
                MessageBox.Show("Добавление записи завершилось ошибкой");
                ItemForm.Close();
            }
        }

        protected override void Delete()
        {
            var entry = App.Context.Entry(CurrentItemFromContext);
            try
            {
                OrderService.DeleteOrder(CurrentItemFromContext);
                UpdateItems();
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show("Вы не можете удалить эту запись, так как она используется в другом разделе");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Удаление записи завершилось ошибкой");
            }
            finally
            {
                entry.Reload();
                MakeCurrentItemEmpty();
            }
        }
    }
}
