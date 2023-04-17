using System.Windows;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Workers;

namespace WpfApp1.Views.Workers.WorkersList
{
    public partial class WorkersListItem: ItemForm
    {
        public WorkersListItem(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            DataContext = (WorkersListViewModel)_sectionWidgetViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
            btnChangePassword.Visibility = Visibility.Collapsed;
        }

        private void DisableAllInputs()
        {
            tbLastname.IsReadOnly = true;
            tbFirstname.IsReadOnly = true;
            tbPatronymic.IsReadOnly = true;
            tbPhoneNumber.IsReadOnly = true;
            calendarBirthday.IsEnabled = false;
            rbMale.IsEnabled = false;
            rbFemale.IsEnabled = false;
            cbPost.IsEnabled = false;
        }

    }
}
