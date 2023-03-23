using System.Windows;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Products;

namespace WpfApp1.Views.Products.GeneralInfo
{
    public partial class ProductsGeneralInfoItemWithImages : ItemWithImages
    {
        public ProductsGeneralInfoItemWithImages(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            DataContext = (ProductsGeneralInfoViewModel)_sectionWidgetViewModel;
            cbSupplier.ItemsSource = ((ProductsGeneralInfoViewModel)_sectionWidgetViewModel).Suppliers;
            cbCategory.ItemsSource = ((ProductsGeneralInfoViewModel)_sectionWidgetViewModel).Categories;
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

    }
}
