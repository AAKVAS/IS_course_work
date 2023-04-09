using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WpfApp1.Views;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views.Orders.OrderHistory;
using WpfApp1.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using Microsoft.Data.SqlClient;

namespace WpfApp1.ViewModels.Orders
{
    public class OrderHistoryViewModel : SectionWidgetViewModel
    {
        private OrderHistoryItem _itemForm;
        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as OrderHistoryItem;
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

        private OrderService _orderService;
        private SectionCreator _sectionCreator;
        private SectionService _sectionService;
        public DefferedQueries DefferedQueries { get; set; }

        public List<Models.Products> Products { get; set; }
        public List<OrderStatuses> Statuses { get; set; }
        public List<Storages> Storages { get; set; }

        public OrderHistoryViewModel(SectionWidget sectionWidget) : base(sectionWidget) {
            _orderService = App.OrderService;
            _sectionCreator = App.SectionCreator;
            _sectionService = App.SectionService;
            UpdateSectionData();
            Products = App.Context.Products.ToList();
            Statuses = App.Context.OrderStatuses.ToList();
            Storages = App.Context.Storages.ToList();
            DefferedQueries = new();
        }

        public void FillSectionInItemForm(OrderHistoryItem item)
        {
            Sections section = _sectionService.GetSectionBySectionKey("workers_in_orders");
            SectionWidget sectionWidget = new OrderHistoryWorkersSectionWidget(section, this);

            if (sectionWidget != null)
            {
                if (_itemFormMode == ItemFormMode.Read)
                {
                    sectionWidget.CollapseAllButtonsExceptReadButton();
                }

                string parentSectionTitle = _sectionService.GetSectionParent(section).Title;
                sectionWidget.ViewModel.SectionTitle = parentSectionTitle + " / " + section.Title;

                item.ucSection.Content = sectionWidget;
            }
        }

        protected override void MakeCurrentItemEmpty()
        {
            _currentItem = new OrderHistoryDTO();
        }

        protected override void CreateNewItemForm()
        {
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
            _sectionData = _orderService.GetOrderHistory();
        }

        protected override void FillItem() {
            OrderHistoryItem orderHistoryItem = ItemForm as OrderHistoryItem;
            CurrentItem.StatusId = (orderHistoryItem.cbStatus.SelectedValue as OrderStatuses)?.Id ?? 0;
            CurrentItem.ProductId = (orderHistoryItem.cbProduct.SelectedValue as Models.Products)?.Id ?? 0;
            CurrentItem.StorageId = (orderHistoryItem.cbStorage.SelectedValue as Storages)?.Id ?? 0;
        }

        protected override string GetErrors()
        {
            StringBuilder errorBuilder = new StringBuilder();

            if (CurrentItem.OrderId == 0)
            {
                errorBuilder.AppendLine("Поле \"Id заказа\" обязательно для заполнения;");
            }
            if (CurrentItem.ProductId == 0)
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
                DefferedQueries.PushQueryToFront(_orderService.GetInsertOrderHistoryQuery(CurrentItem));
                DefferedQueries.CommonParameters.Add(new SqlParameter("@order_id", CurrentItem.OrderId));
                DefferedQueries.CommonParameters.Add(new SqlParameter("@status_changed_at", CurrentItem.StatusChangedAt));
                
                DefferedQueries.ExecuteQueries();
                RefreshDataGrid();
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
                CurrentItemFromContext.Copy(CurrentItem);
                DefferedQueries.PushQueryToFront(_orderService.GetUpdateOrderHistoryQuery(CurrentItem));
                DefferedQueries.ExecuteQueries();

                RefreshDataGrid();
                UpdateItems();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Изменение записи завершилось ошибкой");
            }
            ItemForm.Close();

        }

        protected override void Delete()
        {
            try
            {
                _orderService.DeleteOrderHistory(CurrentItemFromContext);
                RefreshDataGrid();
                UpdateItems();
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
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

        private void RefreshDataGrid()
        {
            SectionWidget.DataGrid.ItemsSource = SectionData;
        }

    }
}
