using System.Windows;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Orders;

namespace WpfApp1.Views.Orders.OrderHistory
{
    /// <summary>
    /// Окно работы с записью подраздела "Доставки / История заказов / Сотрудники в заказах".
    /// </summary>
    public partial class OrderHistoryWorkersItem: ItemForm
    {
        /// <summary>
        /// Ссылка на модель представления подраздела.
        /// </summary>
        private OrderHistoryWorkersViewModel _viewModel;

        /// <summary>
        /// Конструктор, принимающий в качестве параметра ссылку на модель представления подраздела.
        /// </summary>
        /// <param name="sectionWidgetViewModel"></param>
        public OrderHistoryWorkersItem(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            _viewModel = (OrderHistoryWorkersViewModel)_sectionWidgetViewModel;
            DataContext = _viewModel;
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
        /// Метод, делающий все поля записи доступными только для просмотра.
        /// </summary>
        private void DisableAllInputs()
        {
            cbWorker.IsEnabled = false;
        }

        /// <summary>
        /// Обработчик события Loaded. Заполняет значениями выпадающий список сотрудников.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemForm_Loaded(object sender, RoutedEventArgs e)
        {
            cbWorker.ItemsSource = _viewModel.Workers;
            Models.Workers currentWorker = _viewModel.Workers.Find(p => p.Id == _viewModel.CurrentItem.WorkerId);
            cbWorker.SelectedItem = currentWorker;
        }
    }
}
