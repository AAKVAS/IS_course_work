using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WpfApp1.Views;
using WpfApp1.Services;
using WpfApp1.Views.Orders.OrderHistory;
using WpfApp1.Models.DTO;
using System.Windows;

namespace WpfApp1.ViewModels.Orders
{
    internal class OrderHistoryWorkersViewModel : SectionWidgetViewModel
    {
        private OrderHistoryWorkersItem _itemForm;
        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as OrderHistoryWorkersItem;
        }

        private ObservableCollection<dynamic> _sectionData;
        public override ObservableCollection<dynamic> SectionData
        {
            get => _sectionData;
            set => _sectionData = value;
        }

        private OrderHistoryDTO _orderHistoryDTO;
        private OrderHistoryViewModel _orderHistoryViewModel;
        public List<Models.Workers> Workers;

        public OrderHistoryWorkersViewModel(SectionWidget sectionWidget) : base(sectionWidget) {
            OrderHistoryWorkersSectionWidget orderHistoryWorkersSectionWidget = sectionWidget as OrderHistoryWorkersSectionWidget;
            _orderHistoryDTO = orderHistoryWorkersSectionWidget.OrderHistoryDTO;
            _orderHistoryViewModel = orderHistoryWorkersSectionWidget.OrderHistoryViewModel;
            Workers = App.Context.Workers.ToList();
            UpdateSectionData();
        }

        protected override void MakeCurrentItemEmpty()
        {
            CurrentItem = new WorkersInOrdersDTO();
        }

        protected override void CreateNewItemForm()
        {
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
            _sectionData = OrderService.GetWorkersInOrder(_orderHistoryDTO);
        }

        protected override void FillItem() {
            OrderHistoryWorkersItem orderHistoryWorkersItem = ItemForm as OrderHistoryWorkersItem;
            int workerId = (orderHistoryWorkersItem?.cbWorker.SelectedItem as Models.Workers)?.Id ?? 0;
            Models.Workers worker = WorkerService.GetWorkerById(workerId);
            if (worker != null)
            {
                CurrentItem.WorkerId = workerId;
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

        protected override void TryInsert()
        {
            MakeCurrentItemEmpty();

            _itemFormMode = ItemFormMode.Insert;
            CreateNewItemForm();
            ItemForm.Title = SectionTitle;
            ItemForm.Mode = _itemFormMode;
            ItemForm.ShowDialog();
        }

        protected override void Insert()
        {
            if (SectionData.Where(w => w.WorkerId == CurrentItem.WorkerId).Any())
            {
                MessageBox.Show("Такой пользователь уже участвует в изменении статуса");
                ItemForm.Close();
            }
            else
            {
                _orderHistoryViewModel.DefferedQueries.AddQuery(OrderService.GetInsertWorkerInOrderHistoryQuery(CurrentItem));
                SectionData.Add(CurrentItem);
                ItemForm.Close();
            }
        }

        protected override void Delete()
        {
            _orderHistoryViewModel.DefferedQueries.AddQuery(OrderService.GetDeleteWorkerInOrderHistoryQuery(CurrentItemFromContext));
            SectionData.Remove(CurrentItemFromContext);
            MakeCurrentItemEmpty();
        }
    }
}
