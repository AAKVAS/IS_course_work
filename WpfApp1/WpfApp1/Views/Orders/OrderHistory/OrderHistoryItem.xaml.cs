using System.Windows;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Orders;

namespace WpfApp1.Views.Orders.OrderHistory
{
    /// <summary>
    /// Окно работы с записью раздела "Доставки / История заказов".
    /// </summary>
    public partial class OrderHistoryItem : ItemForm
    {
        /// <summary>
        /// Ссылка на модель представления раздела.
        /// </summary>
        private OrderHistoryViewModel _viewModel;

        /// <summary>
        /// Конструктор класса OrderHistoryItem, принимающий в качестве параметра ссылку на модель представления раздела.
        /// </summary>
        /// <param name="sectionWidgetViewModel">Модель представления раздела.</param>
        public OrderHistoryItem(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            _viewModel = (OrderHistoryViewModel)_sectionWidgetViewModel;
            DataContext = _viewModel;
            _viewModel.FillSectionInItemForm(this);
        }

        /// <summary>
        /// Обработчик нажатия на кнопку закрытия окна.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            TryCloseForm();
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

        /// <summary>
        /// Метод, делающий все поля окна раздела доступными только для просмотра.
        /// </summary>
        private void DisableAllInputs()
        {
            cbProduct.IsEnabled = false;
            cbStatus.IsEnabled = false;
            cbStorage.IsEnabled = false;
        }

        /// <summary>
        /// Обработчик события Loaded. Заполняет выпадающие списки нужными значениями.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemForm_Loaded(object sender, RoutedEventArgs e)
        {
            FillComboboxes();
        }

        /// <summary>
        /// Метод, заполняющий выпадающие списки необходимыми значениями.
        /// </summary>
        private void FillComboboxes()
        {
            cbProduct.ItemsSource = _viewModel.Products;
            cbStatus.ItemsSource = _viewModel.Statuses;
            cbStorage.ItemsSource = _viewModel.Storages;

            //Находим модель товара, который указан в OrderHistoryDTO.
            Models.Products currentProduct = _viewModel.Products.Find(p => p.Id == _viewModel.CurrentItem.ProductId);
            cbProduct.SelectedItem = currentProduct;

            //Находим модель склада, который указан в OrderHistoryDTO.
            Models.Storages currentStorage = _viewModel.Storages.Find(s => s.Id == _viewModel.CurrentItem.StorageId);
            cbStorage.SelectedItem = currentStorage;

            //Находим модель статуса доставки, который указан в OrderHistoryDTO.
            Models.OrderStatuses currentStatus = _viewModel.Statuses.Find(s => s.Id == _viewModel.CurrentItem.StatusId);
            cbStatus.SelectedItem = currentStatus;
        }

        /// <summary>
        /// Обработчик события TextChanged для поля ввода Id заказа. Обновляет сведения о заказе в окне.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbId_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            int id = 0;
            if (int.TryParse(tbId.Text, out id))
            {
                _viewModel.CurrentItem.OrderId = id;
                Models.Products currentProduct = _viewModel.Products.Find(p => p.Id == _viewModel.CurrentItem.ProductId);
                cbProduct.SelectedItem = currentProduct;
            }
        }

        /// <summary>
        /// Обработчик события Closing, очищает отложенные запросы при закрытии окна работы с записью. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _viewModel.DefferedQueries.ClearQueries();
        }
    }
}
