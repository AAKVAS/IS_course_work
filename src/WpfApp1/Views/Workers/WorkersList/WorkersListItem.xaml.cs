using System.Windows;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Workers;

namespace WpfApp1.Views.Workers.WorkersList
{
    /// <summary>
    /// Окно работы с записью раздела "Сотрудники / Список сотрудников".
    /// </summary>
    public partial class WorkersListItem: ItemForm
    {
        /// <summary>
        /// Конструктор класса WorkersListItem, принимающий в качестве параметра ссылку на модель представления раздела.
        /// </summary>
        /// <param name="sectionWidgetViewModel">Модель представления раздела.</param>
        public WorkersListItem(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            DataContext = (WorkersListViewModel)_sectionWidgetViewModel;
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
            btnChangePassword.Visibility = Visibility.Collapsed;
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
            cbPost.IsEnabled = false;
        }
    }
}
