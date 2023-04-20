using System.Windows;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views;

namespace WpfApp1
{
    public partial class App : Application
    {
        public static ISWildberriesContext Context = new();
        public readonly static AccessService AccessService = new();
        public readonly static MainWindow MainWindow = new();
        public readonly static PDFGenerator PDFGenerator = new();

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

        private bool IsEnableDB()
        {
            return Context.Database.CanConnect();
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"Возникла непредвиденная ошибка: {e.Exception.Message}");
            e.Handled = true;
        }
    }
}