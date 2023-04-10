using System.Windows;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Orders;
using WpfApp1.ViewModels.Users;

namespace WpfApp1.Views.Orders.OrderHistory
{
    public partial class OrderHistoryItem: ItemForm
    {
        private OrderHistoryViewModel _viewModel;

        public OrderHistoryItem(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            _viewModel = (OrderHistoryViewModel)_sectionWidgetViewModel;
            DataContext = _viewModel;
            _viewModel.FillSectionInItemForm(this);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected override void SetFormModeToInsert()
        {
            tbId.IsReadOnly = false;
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
            cbProduct.IsEnabled = false;
            cbStatus.IsEnabled = false;
            cbStorage.IsEnabled = false;
        }

        private void ItemForm_Loaded(object sender, RoutedEventArgs e)
        {
            FillComboboxes();
        }

        private void FillComboboxes()
        {
            cbProduct.ItemsSource = _viewModel.Products;
            cbStatus.ItemsSource = _viewModel.Statuses;
            cbStorage.ItemsSource = _viewModel.Storages;
            Models.Products currentProduct = _viewModel.Products.Find(p => p.Id == _viewModel.CurrentItem.ProductId);
            if (currentProduct != null)
            {
                cbProduct.SelectedItem = currentProduct;
            }

            Models.Storages currentStorage = _viewModel.Storages.Find(s => s.Id == _viewModel.CurrentItem.StorageId);
            if (currentStorage != null)
            {
                cbStorage.SelectedItem = currentStorage;
            }

            Models.OrderStatuses currentStatus = _viewModel.Statuses.Find(s => s.Id == _viewModel.CurrentItem.StatusId);
            if (currentStatus != null)
            {
                cbStatus.SelectedItem = currentStatus;
            }
        }

        private void tbId_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            int id = 0;
            if (int.TryParse(tbId.Text, out id))
            {
                _viewModel.CurrentItem.OrderId = id;
                FillComboboxes();
            }
        }
    }
}
