using System.Windows;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Orders;

namespace WpfApp1.Views.Orders.OrdersList
{
    /// <summary>
    /// Окно работы с записью раздела "Доставки / Список доставок".
    /// </summary>
    public partial class OrdersListItem: ItemForm
    {
        /// <summary>
        /// Ссылка на модель представления раздела.
        /// </summary>
        private OrdersListViewModel _ordersListViewModel;

        /// <summary>
        /// Конструктор класса OrdersListItem, принимающий в качестве параметра ссылку на модель представления раздела.
        /// </summary>
        /// <param name="sectionWidgetViewModel">Модель представления раздела.</param>
        public OrdersListItem(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            _ordersListViewModel = (OrdersListViewModel)_sectionWidgetViewModel;
            DataContext = _ordersListViewModel;
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
        /// Метод, делающий все поля окна раздела доступными только для просмотра.
        /// </summary>
        private void DisableAllInputs()
        {
            tbProductCount.IsReadOnly = true;
            tbPrice.IsReadOnly = true;
            cbUser.IsEnabled = false;
            cbPickUpPoint.IsEnabled = false;
            cbProduct.IsEnabled = false;
            calendarCreatedAt.IsEnabled = false;
            calendarEstimatedDeliveryAt.IsEnabled = false;
        }
    }
}
