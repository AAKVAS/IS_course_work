using System.Windows;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Storages;

namespace WpfApp1.Views.Storages.Receipts
{
    public partial class StoragesReceiptsItem: ItemForm
    {
        public StoragesReceiptsItem(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            DataContext = (StoragesReceiptsViewModel)_sectionWidgetViewModel;
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
            calendarReceivedAt.IsEnabled = false;
            cbProduct.IsEnabled = false;
            cbStorage.IsEnabled = false;
        }
    }
}
