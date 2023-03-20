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

        public static ISWildberriesContext Context;
        public static AccessService AccessService;
        public static SectionFactory SectionFactory;
        public static UserService UserService;
        public static MainWindow MainWindow;
        public static PDFGenerateService PDFGenerateService;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Context = host.Services.GetService<ISWildberriesContext>();
            AccessService = host.Services.GetService<AccessService>();
            SectionFactory = host.Services.GetService<SectionFactory>();
            UserService = host.Services.GetService<UserService>();
            MainWindow = host.Services.GetService<MainWindow>();
            PDFGenerateService = host.Services.GetService<PDFGenerateService>();

            AddResourceDictionaries();

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
            ResourceDictionary buttonDictionary = new ResourceDictionary() { Source = new Uri("pack://application:,,,/Resources/ButtonTheme.xaml") };
            Resources.MergedDictionaries.Add(buttonDictionary);
            ResourceDictionary textBoxDictionary = new ResourceDictionary() { Source = new Uri("pack://application:,,,/Resources/TextBoxTheme.xaml") };
            Resources.MergedDictionaries.Add(textBoxDictionary);
        }

        private bool IsEnableDB()
        {
            var context = host.Services.GetService<ISWildberriesContext>();
            return context.Database.CanConnect();
        }

    }
}