using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfApp1.Views;
using WpfApp1.Services;
using WpfApp1.Views.Orders.OrderHistory;
using WpfApp1.Models.DTO;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace WpfApp1.ViewModels.Orders
{
    /// <summary>
    /// Модель представления для подраздела "Доставки / История заказов / Сотрудники в заказах".
    /// </summary>
    internal class OrderHistoryWorkersViewModel : SectionWidgetViewModel
    {
        /// <summary>
        /// Окно работы с текущей записью подраздела.
        /// </summary>
        private OrderHistoryWorkersItem _itemForm;

        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as OrderHistoryWorkersItem;
        }

        /// <summary>
        /// Ссылка на текущую запись в разделе "Доставки / История заказов".
        /// </summary>
        private OrderHistoryDTO _currentOrderHistoryDTO { get; set; }

        /// <summary>
        /// Ссылка на модель представления раздела "Доставки / История заказов".
        /// </summary>
        private OrderHistoryViewModel _orderHistoryViewModel;

        /// <summary>
        /// Коллекция сотрудников, необходимая для выпадающего списка в окне работы с записью подраздела.
        /// </summary>
        public List<Models.Workers> Workers { get; set; }

        /// <summary>
        /// Конструктор класса OrderHistoryWorkersViewModel, принимающий в качестве параметра ссылку на представление раздела. 
        /// </summary>
        /// <param name="sectionWidget"></param>
        public OrderHistoryWorkersViewModel(SectionWidget sectionWidget) : base(sectionWidget) {
            OrderHistoryWorkersSectionWidget orderHistoryWorkersSectionWidget = sectionWidget as OrderHistoryWorkersSectionWidget;
            _currentOrderHistoryDTO = orderHistoryWorkersSectionWidget.CurrentOrderHistoryDTO;
            _orderHistoryViewModel = orderHistoryWorkersSectionWidget.OrderHistoryViewModel;
            UpdateSectionData();
        }

        protected override void MakeCurrentItemEmpty()
        {
            CurrentItem = new WorkersInOrdersDTO();
        }

        protected override void CreateNewItemForm()
        {
            Workers = App.Context.Workers.Include(w => w.Post).ToList();
            _itemForm = new OrderHistoryWorkersItem(this);
        }

        protected override void AddCurrentItem()
        {
            App.Context.WorkersInOrdersDTO.Add(CurrentItem);
        }

        protected override void DeleteCurrentItem()
        {
            App.Context.WorkersInOrdersDTO.Remove(CurrentItemFromContext);
        }

        public override void UpdateSectionData()
        {
            SectionData = OrderService.GetWorkersInOrder(_currentOrderHistoryDTO);
        }

        protected override void FillItem() {
            OrderHistoryWorkersItem orderHistoryWorkersItem = ItemForm as OrderHistoryWorkersItem;

            //Берём сотрудника из выпадающего списка и заполняем WorkersInOrdersDTO.
            Models.Workers worker = orderHistoryWorkersItem?.cbWorker.SelectedItem as Models.Workers;
            if (worker != null)
            {
                CurrentItem.WorkerId = worker.Id;
                CurrentItem.WorkerLastname = worker.Lastname;
                CurrentItem.WorkerFirstname = worker.Firstname;
                CurrentItem.WorkerPatronymic = worker.Patronymic;
                CurrentItem.WorkerPost = worker?.Post?.Title;
                CurrentItem.OrderId = _orderHistoryViewModel.CurrentItem?.OrderId ?? 0;
                CurrentItem.StatusChangedAt = _orderHistoryViewModel.CurrentItem.StatusChangedAt;
            }
        }

        protected override string GetErrors()
        {
            StringBuilder errorBuilder = new StringBuilder();

            if (CurrentItem.WorkerId == null || CurrentItem.WorkerId == 0)
            {
                errorBuilder.AppendLine("Свойство \"Сотрудник\" обязательно для заполнения;");
            }

            return errorBuilder.ToString();
        }

        /// <summary>
        /// Метод, добавляющий запрос на добавление сотрудника, участвовашего в изменении статуса доставки, в конец списка отложенных запросов.
        /// Добавляет сотрудника в таблицу подраздела "Доставки / История заказов / Сотрудники в заказах".
        /// </summary>
        protected override void Insert()
        {
            if (SectionData.Where(w => w.WorkerId == CurrentItem.WorkerId).Any())
            {
                MessageBox.Show("Такой пользователь уже участвует в изменении статуса");
            }
            else
            {
                _orderHistoryViewModel.DefferedQueries.AddQuery(OrderService.GetInsertWorkerInOrderHistoryQuery(CurrentItem));
                SectionData.Add(CurrentItem);
            }
            ItemForm.Close();
        }

        /// <summary>
        /// Метод, добавляющий запрос на удаление сотрудника, участвовашего в изменении статуса доставки, в конец списка отложенных запросов.
        /// Удаляет сотрудника из таблицы подраздела "Доставки / История заказов / Сотрудники в заказах".
        /// </summary>
        protected override void Delete()
        {
            _orderHistoryViewModel.DefferedQueries.AddQuery(OrderService.GetDeleteWorkerInOrderHistoryQuery(CurrentItemFromContext));
            SectionData.Remove(CurrentItemFromContext);
            MakeCurrentItemEmpty();
        }
    }
}
