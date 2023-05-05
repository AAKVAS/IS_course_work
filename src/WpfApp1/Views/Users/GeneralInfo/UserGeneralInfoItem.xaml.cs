using System.Windows;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Users;

namespace WpfApp1.Views.Users.GeneralInfo
{
    /// <summary>
    /// Окно работы с записью раздела "Пользователи / Общие сведения".
    /// </summary>
    public partial class UserGeneralInfoItem : ItemForm
    {
        /// <summary>
        /// Конструктор класса UserGeneralInfoItem, принимающий в качестве параметра ссылку на модель представления раздела.
        /// </summary>
        /// <param name="sectionWidgetViewModel">Модель представления раздела.</param>
        public UserGeneralInfoItem(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            DataContext = (UsersGeneralInfoViewModel)_sectionWidgetViewModel;
        }

        /// <summary>
        /// Обработчик нажатия на кнопку закрытия окна.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Метод, делающий все поля окна раздела доступными только для просмотра.
        /// </summary>
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
