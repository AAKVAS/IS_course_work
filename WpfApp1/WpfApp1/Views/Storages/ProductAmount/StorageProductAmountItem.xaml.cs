using System.Windows;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Storages;
using WpfApp1.ViewModels.Users;

namespace WpfApp1.Views.Storages.ProductAmount
{
    public partial class StorageProductAmountItem: ItemForm
    {
        public StorageProductAmountItem(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            DataContext = (StorageProductAmountViewModel)_sectionWidgetViewModel;
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
            tbAmount.IsReadOnly = true;
            cbProduct.IsEnabled = false;
            cbStorage.IsEnabled = false;
        }
    }
}
