using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views;

namespace WpfApp1.ViewModels
{
    internal class MainWindowViewModel
    {
        AccessService _accessService;
        SectionFactory _sectionFactory;
        List<Sections> _sections;

        RelayCommand? _sectionChoosedCommand;
        MainWindow _mainWindow;

        public RelayCommand SectionChoosedCommand
        {
            get
            {
                return _sectionChoosedCommand ??
                        (_sectionChoosedCommand = new RelayCommand((object obj) => {
                            AddSectionToMainTabControl(obj as string);
                        },
                        (obj) => _accessService.HasWorkerRightToSection(obj as string)));
            }

        }

        public MainWindowViewModel(MainWindow mainWindow)
        {
            _accessService = (AccessService)Application.Current.Resources["AccessService"];
            _sectionFactory = (SectionFactory)Application.Current.Resources["SectionFactory"];
            _mainWindow = mainWindow;
            HideMenuItemsWithoutRights();
        }

        private void HideMenuItemsWithoutRights()
        {
            _sections = _accessService.LoginedWorkerSections;

            Menu mainMenu = _mainWindow.GetMainMenu();

            CollapseMenuItems(mainMenu);

            foreach (var section in _sections)
            {
                MenuItem menuItem = mainMenu.FindName(section.SectionKey) as MenuItem;
                menuItem.Visibility = Visibility.Visible;
            }

        }

        private void CollapseMenuItems(Menu menu)
        {
            foreach (MenuItem item in menu.Items)
            {
                item.Visibility = Visibility.Collapsed;

                foreach (MenuItem childItem in item.Items)
                {
                    childItem.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void AddSectionToMainTabControl(string sectionKey)
        {
            string sectionName = _accessService.GetSectionBySectionKey(sectionKey).Title;

            SectionWidget sectionWidget = _sectionFactory.GetSectionWidget(sectionKey);

            TabControl tabControl = _mainWindow.GetMainTabControl();

            tabControl.Items.Add(new TabItem
            {
                Header = sectionName,
                Content = sectionWidget
            });
        }
    }
}
