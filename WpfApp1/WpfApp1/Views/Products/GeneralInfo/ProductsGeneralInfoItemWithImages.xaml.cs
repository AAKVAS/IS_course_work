using System.Windows;
using System.Windows.Controls;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Products;

namespace WpfApp1.Views.Products.GeneralInfo
{
    public partial class ProductsGeneralInfoItemWithImages : ItemWithImages
    {
        public ProductsGeneralInfoItemWithImages(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            ProductsGeneralInfoViewModel viewModel = (ProductsGeneralInfoViewModel)_sectionWidgetViewModel;
            DataContext = viewModel;
            cbSupplier.ItemsSource = viewModel.Suppliers;
            cbCategory.ItemsSource = viewModel.Categories;
            viewModel.LoadCurrentItemImages();
            lbImages.ItemsSource = viewModel.CurrentItemImages;
        }

        public override ListBox ListBox 
        { 
            get => lbImages;
            set => lbImages = value; 
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
            tbId.IsReadOnly = true;
            tbTitle.IsReadOnly = true;
            tbDescription.IsReadOnly = true;
            tbPrice.IsReadOnly = true;
            tbPercent.IsReadOnly = true;
            cbCategory.IsEnabled = false;
            cbSupplier.IsEnabled = false;
        }

        private void lbImages_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ((SectionWidgetWithImagesViewModel)_sectionWidgetViewModel).TryShowImageForm(lbImages.SelectedItem);
        }
    }
}
