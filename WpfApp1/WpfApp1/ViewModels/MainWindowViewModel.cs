using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views;

namespace WpfApp1.ViewModels
{
    internal class MainWindowViewModel
    {
        private AccessService _accessService;
        private SectionCreator _sectionCreator;
        private SectionService _sectionService;
        private List<Sections> _sections;

        private RelayCommand? _sectionChoosedCommand;
        private MainWindow _mainWindow;

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
            _accessService = App.AccessService;
            _sectionCreator = App.SectionCreator;
            _sectionService = App.SectionService;
            _mainWindow = mainWindow;
            HideMenuItemsWithoutRights();
        }

        private void HideMenuItemsWithoutRights()
        {
            _sections = _accessService.LoginedWorkerSections;

            Menu mainMenu = _mainWindow.mainMenu;

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
            Sections section = _sectionService.GetSectionBySectionKey(sectionKey);

            SectionWidget sectionWidget = _sectionCreator.GetSectionWidget(section);

            if (sectionWidget != null)
            {
                TabControl tabControl = _mainWindow.mainTabControl;

                string parentSectionTitle = _sectionService.GetSectionParent(section).Title;
                sectionWidget.ViewModel.SectionTitle = parentSectionTitle + " / " + section.Title;

                tabControl.Items.Add(new TabItem
                {
                    Header = sectionWidget.ViewModel.SectionTitle,
                    Content = sectionWidget
                });
            }
        }
    }
}
