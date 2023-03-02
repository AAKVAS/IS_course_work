using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Models;
using WpfApp1.ViewModels;
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
                    services.AddSingleton<SectionFactory>();
                })
                .Build();

            new App(host).Run();
        }
    }
}
