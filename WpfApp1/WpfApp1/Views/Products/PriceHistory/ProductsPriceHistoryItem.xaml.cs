using System.Windows;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Products;

namespace WpfApp1.Views.Products.PriceHistory
{
    public partial class ProductsPriceHistoryItem: ItemForm
    {
        private ProductsPriceHistoryViewModel _viewModel;

        public ProductsPriceHistoryItem(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            _viewModel = (ProductsPriceHistoryViewModel)_sectionWidgetViewModel;
            DataContext = _viewModel;
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
        }

        private void DisableAllInputs()
        {
            cbProduct.IsEnabled = false;
            tbPrice.IsReadOnly = true;
            calendarPriceDate.IsEnabled = false;
        }
    }
}
