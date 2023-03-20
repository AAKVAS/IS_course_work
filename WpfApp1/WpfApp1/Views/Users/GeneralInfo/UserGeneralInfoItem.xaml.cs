using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Users.GeneralInfo;

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
            cbCountry.ItemsSource = ((UsersGeneralInfoViewModel)_sectionWidgetViewModel).Countries;
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
