using System.Windows;
using WpfApp1.ViewModels.Workers;

namespace WpfApp1.Views.Workers.WorkersList
{
    /// <summary>
    /// Окно изменения пароля.
    /// </summary>
    public partial class ChangePasswordForm : Window
    {
        /// <summary>
        /// Модель представления раздела "Сотрудники / Список сотрудников".
        /// </summary>
        private WorkersListViewModel _workersListSectionWidget;

        /// <summary>
        /// Конструктор класса ChangePasswordForm, принимающий в качестве параметра ссылку на модель представления раздела "Сотрудники / Список сотрудников".
        /// </summary>
        /// <param name="sectionWidgetViewModel">Модель представления раздела "Сотрудники / Список сотрудников".</param>
        public ChangePasswordForm(WorkersListViewModel workersListSectionWidget)
        {
            InitializeComponent();
            _workersListSectionWidget = workersListSectionWidget;
            DataContext = _workersListSectionWidget;
        }

        /// <summary>
        /// Обработчик нажатия на кнопку закрытия окна.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Обработчик события PasswordChanged у поля пароля. Меняет оценку пароля.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            tbPasswordComplexity.Text = _workersListSectionWidget.RusPasswordComplexity;
        }
    }
}
