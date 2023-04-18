using System.Windows;
using System.Windows.Controls;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Products;

namespace WpfApp1.Views.Products.GeneralInfo
{
    public partial class ProductsGeneralInfoItemWithImages : ItemWithImages
    {
        private ProductsGeneralInfoViewModel _viewModel;

        public ProductsGeneralInfoItemWithImages(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            _viewModel = (ProductsGeneralInfoViewModel)_sectionWidgetViewModel;
            DataContext = _viewModel;
            _viewModel.LoadCurrentItemImages();
            lbImages.ItemsSource = _viewModel.CurrentItemFromContext.Images;
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
        }

        private void DisableAllInputs()
        {
            tbId.IsReadOnly = true;
            tbTitle.IsReadOnly = true;
            tbDescription.IsReadOnly = true;
            tbPrice.IsReadOnly = true;
            tbPercent.IsReadOnly = true;
            cbCategory.IsEnabled = false;
            cbSupplier.IsEnabled = false;
            btnAddImage.IsEnabled = false;
            btnAddImage.Visibility = Visibility.Collapsed;
        }

        private void lbImages_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _viewModel.TryShowImageForm();
        }
    }
}
