using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
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

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AddResourceDictionaries();
            AddServicesToResources();
            AddMainWindowToResources();

            if (IsEnableDB())
            {
                var startWindow = host.Services.GetService<Login>();
                startWindow?.Show();
            }
            else {
                MessageBox.Show("Не удалось подключиться к базе данных!");
            }
        }

        private void AddResourceDictionaries()
        {
            ResourceDictionary dict = new ResourceDictionary() { Source = new Uri("pack://application:,,,/Resources/ButtonTheme.xaml") };
            Resources.MergedDictionaries.Add(dict);
        }

        private void AddServicesToResources()
        {
            AccessService accessService = host.Services.GetService<AccessService>();
            Resources.Add("AccessService", accessService);
            SectionFactory sectionFactory = host.Services.GetService<SectionFactory>();
            Resources.Add("SectionFactory", sectionFactory);
            UserService userService = host.Services.GetService<UserService>();
            Resources.Add("UserService", userService);
        }

        private void AddMainWindowToResources()
        {
            MainWindow mainWindow = host.Services.GetService<MainWindow>();
            Resources.Add("MainWindow", mainWindow);
        }

        private bool IsEnableDB()
        {
            var context = host.Services.GetService<ISWildberriesContext>();
            return context.Database.CanConnect();
        }

    }
}