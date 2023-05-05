using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfApp1.Views;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views.Orders.OrderHistory;
using WpfApp1.Models.DTO;
using System.Windows;
using Microsoft.Data.SqlClient;
using System.Windows.Controls;

namespace WpfApp1.ViewModels.Orders
{
    /// <summary>
    /// Модель представления для раздела "Доставки / История заказов".
    /// </summary>
    public class OrderHistoryViewModel : SectionWidgetViewModel
    {
        /// <summary>
        /// Ссылка на окно работы с записью раздела. 
        /// </summary>
        private OrderHistoryItem _itemForm;

        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as OrderHistoryItem;
        }

        /// <summary>
        /// Отложенные запросы добавления/удаления сотрудников, причастных к изменению статуса заказа.
        /// </summary>
        public DefferedQueries DefferedQueries { get; set; }

        /// <summary>
        /// Коллекция товаров, используется для заполнения выпадающего списка в окне работы с записью раздела.
        /// </summary>
        public List<Models.Products> Products { get; set; }

        /// <summary>
        /// Коллекция статусов, используется для заполнения выпадающего списка в окне работы с записью раздела.
        /// </summary>
        public List<OrderStatuses> Statuses { get; set; }

        /// <summary>
        /// Коллекция складов, используется для заполнения выпадающего списка в окне работы с записью раздела.
        /// </summary>
        public List<Models.Storages> Storages { get; set; }

        /// <summary>
        /// Конструктор класса OrderHistoryViewModel, в качестве параметра принимает ссылку на представление раздела.
        /// </summary>
        /// <param name="sectionWidget">Представление раздела.</param>
        public OrderHistoryViewModel(SectionWidget sectionWidget) : base(sectionWidget) 
        {
            DefferedQueries = new();
        }

        /// <summary>
        /// Метод, заполняющий таблицу сотрудников, причастных к изменению статуса заказа внутри окна работы с записью.
        /// В качестве параметра принимает ссылку на окно работы с записью раздела, в котором нужно заполнить таблицу.
        /// </summary>
        /// <param name="item">Окно работы с запиьсю раздела "Доставки / История заказов".</param>
        public void FillSectionInItemForm(OrderHistoryItem item)
        {
            //Создаём представление подраздела сотрудников, причастных к изменению заказа.
            Sections section = SectionService.GetSectionBySectionKey("workers_in_orders");
            SectionWidget sectionWidget = new OrderHistoryWorkersSectionWidget(section, this);

            //Оставляем только кнопку просмотра, если режим работы с окном раздела - просмотр.
            if (_itemFormMode == ItemFormMode.Read)
            {
                sectionWidget.CollapseAllButtonsExceptReadButton();
            }

            //Даём заголовок разделу.
            string parentSectionTitle = SectionService.GetSectionParent(section).Title;
            sectionWidget.ViewModel.SectionTitle = parentSectionTitle + " / " + section.Title;

            //Помещяем таблицу сотрудников, причастных к изменению заказа в текущее окно работы с записью раздела.
            item.ucSection.Content = sectionWidget;
        }

        protected override void MakeCurrentItemEmpty()
        {
            CurrentItem = new OrderHistoryDTO();
        }

        protected override void CreateNewItemForm()
        {
            //Обновляем данные в выпадающих списках.
            Products = App.Context.Products.ToList();
            Statuses = App.Context.OrderStatuses.ToList();
            Storages = App.Context.Storages.ToList();
            _itemForm = new OrderHistoryItem(this);
        }

        protected override void AddCurrentItem()
        {
            App.Context.OrderHistoryDTO.Add(CurrentItem);
        }

        protected override void DeleteCurrentItem()
        { 
            App.Context.OrderHistoryDTO.Remove(CurrentItemFromContext);
        }

        public override void UpdateSectionData()
        {
            SectionData = OrderService.GetOrderHistory();
        }

        protected override void FillItem() {
            //Заполняем идентификаторы статуса измения доставки, товара и склада в OrderHistoryDTO значениями из выпадающих списков.
            OrderHistoryItem orderHistoryItem = ItemForm as OrderHistoryItem;
            CurrentItem.StatusId = (orderHistoryItem.cbStatus.SelectedValue as OrderStatuses)?.Id ?? 0;
            CurrentItem.ProductId = (orderHistoryItem.cbProduct.SelectedValue as Models.Products)?.Id ?? 0;
            CurrentItem.StorageId = (orderHistoryItem.cbStorage.SelectedValue as Models.Storages)?.Id ?? 0;
        }

        protected override string GetErrors()
        {
            StringBuilder errorBuilder = new StringBuilder();

            if (Validation.GetHasError((ItemForm as OrderHistoryItem).tbId) || CurrentItem.OrderId == 0)
            {
                errorBuilder.AppendLine("Поле \"Id заказа\" - положительное число;");
            }
            if (CurrentItem.ProductId == 0)
            {
                errorBuilder.AppendLine("Свойство \"Товар\" обязательно для заполнения;");
            }
            if (CurrentItem.StatusId == 0)
            {
                errorBuilder.AppendLine("Свойство \"Статус заказа\" обязательно для заполнения;");
            }
            if (CurrentItem.StorageId == 0)
            {
                errorBuilder.AppendLine("Свойство \"Склад\" обязательно для заполнения;");
            }

            return errorBuilder.ToString();
        }

        protected override void Insert()
        {
            try
            {
                //Добавляем запрос на добавление изменения статуса заказа в начало транзакции.
                DefferedQueries.PushQueryToFront(OrderService.GetInsertOrderHistoryQuery(CurrentItem));
                
                //Добавляем к общим параметрам Id заказа и дату изменения заказа. Они должны быть одинаковыми у всех запросов в транзакции.
                DefferedQueries.CommonParameters.Add(new SqlParameter("@order_id", CurrentItem.OrderId));
                DefferedQueries.CommonParameters.Add(new SqlParameter("@status_changed_at", CurrentItem.StatusChangedAt));
                
                //Выполняем транзакцию и обновляем данные в таблице раздела.
                DefferedQueries.ExecuteQueries();;
                UpdateItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Добавление записи завершилось ошибкой");
            }
            ItemForm.Close();
        }

        protected override void Update()
        {
            try
            {
                //Копируем данные о текущем разделе.
                CurrentItemFromContext.Copy(CurrentItem);

                //Добавляем запрос на обновление изменения статуса заказа в начало транзакции.
                DefferedQueries.PushQueryToFront(OrderService.GetUpdateOrderHistoryQuery(CurrentItem));

                //Добавляем к общим параметрам Id заказа и дату изменения заказа. Они должны быть одинаковыми у всех запросов в транзакции.
                DefferedQueries.CommonParameters.Add(new SqlParameter("@order_id", CurrentItem.OrderId));
                DefferedQueries.CommonParameters.Add(new SqlParameter("@status_changed_at", CurrentItem.StatusChangedAt));

                //Выполняем транзакцию и обновляем данные в таблице раздела.
                DefferedQueries.ExecuteQueries();
                UpdateItems();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Изменение записи завершилось ошибкой");
            }
            ItemForm.Close();
        }

        /// <summary>
        /// Метод удаления записи из базы данных. Пытается удалить данные, если сталкивается с ошибкой, то выводит сообщение о невозможности удаления, пока не будут очищены сотрудники, причастные к изменению статуса заказа.
        /// </summary>
        protected override void Delete()
        {
            try
            {
                OrderService.DeleteOrderHistory(CurrentItemFromContext);
                UpdateItems();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Вы не можете удалить эту запись, пока не очистите список сотрудников, участвоваших в изменении заказа");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Удаление записи завершилось ошибкой");
            }
            finally
            {
                MakeCurrentItemEmpty();
            }
        }
    }
}
