using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;
using WpfApp1.Models.DTO;
using WpfApp1.Services;
using WpfApp1.Views;

namespace WpfApp1.ViewModels
{
    /// <summary>
    /// Модель представления для основного окна приложения.
    /// </summary>
    internal class MainWindowViewModel
    {
        /// <summary>
        /// Ссылка на объект класса AccessService для авторизации.
        /// </summary>
        private AccessService _accessService;

        /// <summary>
        /// Список прав на разделы вошедшего в систему сотрудника.
        /// </summary>
        private List<LoginedWorkerRights> _sections = null!;
        
        /// <summary>
        /// 
        /// </summary>
        private MainWindow _mainWindow;

        /// <summary>
        /// Команда создания вкладки с новым разделом.
        /// </summary>
        private RelayCommand? _sectionChoosedCommand;

        /// <summary>
        /// Команда создания вкладки с новым разделом.
        /// </summary>
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

        /// <summary>
        /// Конструктор класса MainWindowViewModel, принимающий в качестве параметра ссылку на представление основного окна MainWindow.
        /// </summary>
        /// <param name="mainWindow">Ссылка на основное окно.</param>
        public MainWindowViewModel(MainWindow mainWindow)
        {
            _accessService = App.AccessService;
            _mainWindow = mainWindow;
            HideMenuItemsWithoutRights();
        }

        /// <summary>
        /// Метод, скрывающий кнопки открытия разделов, если у вошедшего сотрудника нет на них прав.
        /// Вначале метод скрывает абсолютно все кнопки открытия разделов, а затем открывает те, на которые у сотрудника есть права.
        /// </summary>
        private void HideMenuItemsWithoutRights()
        {
            _sections = _accessService.LoginedWorkerSections;

            //Меню, в котором расположены кнопки, открывающие разделы.
            Menu mainMenu = _mainWindow.mainMenu;

            CollapseMenuItems(mainMenu);

            foreach (var section in _sections)
            {
                MenuItem menuItem = mainMenu.FindName(section.SectionKey) as MenuItem;
                if (menuItem != null)
                {
                    menuItem.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// Метод, скрывающий кнопки меню. В качестве параметра принимает ссылку на меню, в котором нужно спрятать кнопки.
        /// </summary>
        /// <param name="menu">Меню, в котором нужно спрятать кнопки.</param>
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

        /// <summary>
        /// Метод, добавляющий вкладку с новым разделом. В качестве параметра принимает английское название раздела, для которого нужно добавить вкладку.
        /// Метод выбирает подходящее представление раздела.
        /// Затем создаётся заголовок раздела, который устанавливается в качестве названия вкладки.
        /// </summary>
        /// <param name="sectionKey">Английское название раздела, для которого нужно добавить вкладку.</param>
        private void AddSectionToMainTabControl(string sectionKey)
        {
            Sections section = SectionService.GetSectionBySectionKey(sectionKey);

            SectionWidget? sectionWidget = SectionCreator.GetSectionWidget(section);

            if (sectionWidget != null)
            {
                TabControl tabControl = _mainWindow.mainTabControl;

                string parentSectionTitle = SectionService.GetSectionParent(section).Title;
                string sectionTitle = parentSectionTitle + " / " + section.Title;
                sectionWidget.ViewModel.SectionTitle = sectionTitle;

                tabControl.Items.Add(new TabItem
                {
                    Header = sectionTitle,
                    Content = sectionWidget
                });
            }
        }
    }
}
