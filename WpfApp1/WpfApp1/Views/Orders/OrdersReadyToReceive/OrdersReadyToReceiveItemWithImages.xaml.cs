using System.Windows;
using System.Windows.Controls;
using WpfApp1.Services;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Orders;
using WpfApp1.ViewModels.Products;


namespace WpfApp1.Views.Orders.OrdersReadyToReceive {

    public partial class OrdersReadyToReceiveItemWithImages : ItemWithImages
    {

        private OrdersReadyToReceiveViewModel _viewModel;

        public OrdersReadyToReceiveItemWithImages(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            _viewModel = (OrdersReadyToReceiveViewModel)_sectionWidgetViewModel;
            DataContext = _viewModel;
            _viewModel.LoadCurrentItemImages();
            lbImages.ItemsSource = _viewModel.CurrentItemFromContext.Order.Product.Images;
        }

        public override ListBox ListBox 
        { 
            get => lbImages;
            set => lbImages = value; 
        }

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
            cbStatus.IsEnabled = false;
        }

        private void DisableAllInputs() {}

        private void lbImages_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _viewModel.TryShowImageWindow(ImageWindowMode.Read);
        }
    }
}

