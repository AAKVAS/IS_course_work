using System.Windows;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Suppliers;

namespace WpfApp1.Views.Suppliers.GeneralInfo
{
    public partial class SuppliersGeneralInfoItem: ItemForm
    {
        public SuppliersGeneralInfoItem(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            DataContext = (SuppliersGeneralInfoViewModel)_sectionWidgetViewModel;
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
            tbTitle.IsReadOnly = true;
        }

    }
}
