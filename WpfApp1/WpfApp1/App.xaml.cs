using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views;

namespace WpfApp1
{
    public partial class App : Application
    {

        public static ISWildberriesContext Context = new ISWildberriesContext();
        public static AccessService AccessService = new AccessService();
        public static MainWindow MainWindow = new MainWindow();
        public static PDFGenerateService PDFGenerateService = new PDFGenerateService();
        public static ProductService ProductService = new ProductService();
        public static SectionCreator SectionCreator = new SectionCreator();
        public static SectionService SectionService = new SectionService();
        public static StorageService StorageService = new StorageService();
        public static UserService UserService = new UserService();
        public static WorkerService WorkerService = new WorkerService();
        public static OrderService OrderService = new OrderService();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (IsEnableDB())
            {
                var startWindow = new Login();
                startWindow?.Show();
            }
            else
            {
                MessageBox.Show("Не удалось подключиться к базе данных!");
            }
        }

        private bool IsEnableDB()
        {
            return Context.Database.CanConnect();
        }

    }
}