using System.Windows;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Users;

namespace WpfApp1.Views.Users.GeneralInfo
{
    /// <summary>
    /// Логика взаимодействия для UserGeneralInfoItem.xaml
    /// </summary>
    public partial class UserGeneralInfoItem : ItemForm
    {
        public UserGeneralInfoItem(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            DataContext = (UsersGeneralInfoViewModel)_sectionWidgetViewModel;
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
            tbLastname.IsReadOnly = true;
            tbFirstname.IsReadOnly = true;
            tbPatronymic.IsReadOnly = true;
            tbPhoneNumber.IsReadOnly = true;
            calendarBirthday.IsEnabled = false;
            rbMale.IsEnabled = false;
            rbFemale.IsEnabled = false;
            tbEmail.IsReadOnly = true;
            cbCountry.IsEnabled = false;
        }
    }
}
