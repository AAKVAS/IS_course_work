using System.Windows;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Orders;
using WpfApp1.ViewModels.Users;

namespace WpfApp1.Views.Orders.OrderHistory
{
    public partial class OrderHistoryWorkersItem: ItemForm
    {
        private OrderHistoryWorkersViewModel _viewModel;

        public OrderHistoryWorkersItem(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            _viewModel = (OrderHistoryWorkersViewModel)_sectionWidgetViewModel;
            DataContext = _viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TryCloseForm();
        }

        protected override void SetFormModeToInsert()
        {
            btnDataAction.Visibility = Visibility.Visible;
            btnDataAction.Content = "Сохранить";
        }

        protected override void SetFormModeToUpdate()
        {
            btnDataAction.Visibility = Visibility.Visible;
            btnDataAction.Content = "Изменить";
        }

        protected override void SetFormModeToRead()
        {
            btnDataAction.Visibility = Visibility.Collapsed;
            DisableAllInputs();
        }

        private void DisableAllInputs()
        {
            cbWorker.IsEnabled = false;
        }

        private void ItemForm_Loaded(object sender, RoutedEventArgs e)
        {
            cbWorker.ItemsSource = _viewModel.Workers;
            Models.Workers currentWorker = _viewModel.Workers.Find(p => p.Id == _viewModel.CurrentItem.WorkerId);
            if (currentWorker != null)
            {
                cbWorker.SelectedItem = currentWorker;
            }
        }
    }
}
