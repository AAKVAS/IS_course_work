using System;
using System.Windows;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views;

namespace WpfApp1
{
    /// <summary>
    /// Класс, инкапсулирующий приложение WPF.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Контекст БД.
        /// </summary>
        public static ISWildberriesContext Context = new();

        /// <summary>
        /// Свойство, обеспечивающее аутентификацию и авторизацию в приложении. 
        /// </summary>
        public static AccessService AccessService = new();

        /// <summary>
        /// Основное окно приложения.
        /// </summary>
        public static MainWindow MainWindow = new();

        /// <summary>
        /// Объект, генерирующий PDF-документы.
        /// </summary>
        public static PDFGenerator PDFGenerator = new();

        /// <summary>
        /// Обработчик события OnStartup.
        /// Проверяет доступ к БД, если он отсутствиет, то приложение выдаёт предупреждение и завершает работу.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (IsEnableDB())
            {
                Current.MainWindow = new Login();
                Current.MainWindow.Show();
            }
            else
            {
                MessageBox.Show("Не удалось подключиться к базе данных!");
                Current.Shutdown();
            }
        }

        /// <summary>
        /// Метод проверки наличия доступа к БД.
        /// </summary>
        /// <returns>Истина, если база данных доступна.</returns>
        private bool IsEnableDB()
        {
            return Context.Database.CanConnect();
        }

        /// <summary>
        /// Обработчик необработанных исключений. Выводит сообщение о возникшей ошибке.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"Возникла непредвиденная ошибка: {e.Exception.Message}");
            e.Handled = true;
        }
    }
}