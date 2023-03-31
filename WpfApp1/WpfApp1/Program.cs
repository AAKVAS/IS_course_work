using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using WpfApp1.Models;
using WpfApp1.Views;
using WpfApp1.Services;

namespace WpfApp1
{
    internal class Program
    {
        [STAThread]
        public static void Main()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<Login>();
                    services.AddSingleton<MainWindow>();

                    services.AddSingleton<ISWildberriesContext>();
                    services.AddSingleton<AccessService>();
                    services.AddSingleton<PDFGenerateService>();
                    services.AddSingleton<SectionCreator>();
                    services.AddSingleton<SectionService>();
                    services.AddSingleton<UserService>();
                    services.AddSingleton<WorkerService>();
                    services.AddSingleton<OrderService>();
                    services.AddSingleton<ProductService>();
                })
                .Build();

            new App(host).Run();
        }
    }
}
