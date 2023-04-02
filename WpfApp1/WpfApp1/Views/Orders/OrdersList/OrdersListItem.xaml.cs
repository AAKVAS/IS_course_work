using System.Windows;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Orders;


namespace WpfApp1.Views.Orders.OrdersList
{
    public partial class OrdersListItem: ItemForm
    {
        public OrdersListItem(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            DataContext = (OrdersListViewModel)_sectionWidgetViewModel;
            cbUser.ItemsSource = ((OrdersListViewModel)_sectionWidgetViewModel).Users;
            cbPickUpPoint.ItemsSource = ((OrdersListViewModel)_sectionWidgetViewModel).PickUpPoints;
            cbProduct.ItemsSource = ((OrdersListViewModel)_sectionWidgetViewModel).Products;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
