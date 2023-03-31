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
        readonly IHost host;

        public App(IHost host)
        {
            this.host = host;
        }

        public static ISWildberriesContext Context { get; set; }
        public static AccessService AccessService { get; set; }
        public static MainWindow MainWindow { get; set; }
        public static PDFGenerateService PDFGenerateService { get; set; }
        public static ProductService ProductService { get; set; }
        public static SectionCreator SectionCreator { get; set; }
        public static SectionService SectionService { get; set; }
        public static UserService UserService { get; set; }
        public static WorkerService WorkerService { get; set; }
        public static OrderService OrderService { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Context = host.Services.GetService<ISWildberriesContext>();
            AccessService = host.Services.GetService<AccessService>();
            MainWindow = host.Services.GetService<MainWindow>();
            PDFGenerateService = host.Services.GetService<PDFGenerateService>();
            ProductService = host.Services.GetService<ProductService>();
            SectionCreator = host.Services.GetService<SectionCreator>();
            SectionService = host.Services.GetService<SectionService>();
            UserService = host.Services.GetService<UserService>();
            WorkerService = host.Services.GetService<WorkerService>();
            OrderService = host.Services.GetService<OrderService>();

            if (IsEnableDB())
            {
                var startWindow = host.Services.GetService<Login>();
                startWindow?.Show();
            }
            else {
                MessageBox.Show("Не удалось подключиться к базе данных!");
            }
        }

        private bool IsEnableDB()
        {
            var context = host.Services.GetService<ISWildberriesContext>();
            return context.Database.CanConnect();
        }

    }
}