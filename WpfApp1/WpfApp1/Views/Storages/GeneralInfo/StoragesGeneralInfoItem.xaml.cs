using System.Windows;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Storages;

namespace WpfApp1.Views.Storages.GeneralInfo
{
    public partial class StoragesGeneralInfoItem: ItemForm
    {
        StoragesGeneralInfoViewModel _viewModel;

        public StoragesGeneralInfoItem(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            _viewModel = (StoragesGeneralInfoViewModel)_sectionWidgetViewModel;
            DataContext = _viewModel;
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
            tbCountry.IsReadOnly = true;
            tbHouseNumber.IsReadOnly = true;
            tbStreet.IsReadOnly = true;
            tbFederalSubject.IsReadOnly = true;
            tbLocality.IsReadOnly = true;
            cbStorages.IsEnabled = false;
        }

    }
}
