using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls.Primitives;
using WpfApp1.Services;
using WpfApp1.Views;

namespace WpfApp1.ViewModels
{
    /// <summary>
    /// Абстрактный класс, являющийся моделью представления для раздела информационной системы.
    /// </summary>
    public abstract class SectionWidgetViewModel
    {
        /// <summary>
        /// Ссылка на объект класса AccessService для авторизации.
        /// </summary>
        protected AccessService _accessService;

        /// <summary>
        /// Ссылка на объект класса PDFGenerator для генерации PDF-документов.
        /// </summary>
        protected PDFGenerator _pdfGenarator;

        /// <summary>
        /// Текущий режим окна работы с данными.
        /// </summary>
        protected ItemFormMode _itemFormMode = ItemFormMode.Read;

        /// <summary>
        /// Команда вызова окна для вставки записи в базу данных.
        /// </summary>
        protected RelayCommand? _insertCommand;

        /// <summary>
        /// Команда вызова окна для удаления записи из базы данных.
        /// </summary>
        protected RelayCommand? _deleteCommand;

        /// <summary>
        /// Команда вызова окна для изменения записи в базе данных.
        /// </summary>
        protected RelayCommand? _updateCommand;

        /// <summary>
        /// Команда вызова окна для просмотра записи.
        /// </summary>
        protected RelayCommand? _readCommand;

        /// <summary>
        /// Команда закрытия вкладки SectionWidget в главном окне.
        /// </summary>
        protected RelayCommand? _closeCommand;

        /// <summary>
        /// Команда сохранения изменений из окна работы с записью в базу данных.
        /// </summary>
        protected RelayCommand? _saveCommand;

        /// <summary>
        /// Команда создания PDF-документа на основе данных в разделе.
        /// </summary>
        protected RelayCommand? _pdfCommand;

        /// <summary>
        /// Заголовок раздела.
        /// </summary>
        public string SectionTitle { get; set; }

        /// <summary>
        /// Ссылка на представление SectionWidget.
        /// </summary>
        public SectionWidget SectionWidget;

        /// <summary>
        /// Сервис для фильтрации данных в разделе SectionWidget.
        /// </summary>
        public FilterService FilterService { get; set; }

        /// <summary>
        /// Запись раздела, с которой происходит работа в текущий момент.
        /// Имеет тип данных dynamic в связи с тем, что базовый класс модели представления не знает, какого типа данных будет запись.
        /// </summary>
        public dynamic? CurrentItemFromContext { get; set; }

        /// <summary>
        /// Копия текущей записи раздела, с которой происходит работа в текущий момент.
        /// Имеет тип данных dynamic в связи с тем, что базовый класс модели представления не знает, какого типа данных будет запись.
        /// </summary>
        public dynamic? CurrentItem { get; set; }

        /// <summary>
        /// Окно работы с текущей записью раздела.
        /// </summary>
        public abstract ItemForm ItemForm { get; set; }

        /// <summary>
        /// Коллекеция данных раздела.
        /// Имеет тип данных ObservableCollection<dynamic>, потому что базовый класс модели представления не знает, какого типа данных будут записи в разделе.
        /// </summary>
        public ObservableCollection<dynamic> SectionData { get; set; }

        /// <summary>
        /// Команда вызова окна для вставки записи в базу данных.
        /// </summary>
        public RelayCommand InsertCommand
        {
            get
            {
                return _insertCommand ??
                        (_insertCommand = new RelayCommand((object obj) => {
                            TryInsert();
                        },
                        (obj) => _accessService.HasWorkerRightToInsert(SectionWidget.Section.SectionKey)));
            }
        }

        /// <summary>
        /// Команда вызова окна для удаления записи из базы данных.
        /// </summary>
        public RelayCommand DeleteCommand
        {
            get
            {
                return _deleteCommand ??
                        (_deleteCommand = new RelayCommand((object obj) => {
                            TryDelete();
                        },
                        (obj) => _accessService.HasWorkerRightToDelete(SectionWidget.Section.SectionKey)));
            }
        }

        /// <summary>
        /// Команда вызова окна для изменения записи в базе данных.
        /// </summary>
        public RelayCommand UpdateCommand
        {
            get
            {
                return _updateCommand ??
                        (_updateCommand = new RelayCommand((object obj) => {
                            TryUpdate();
                        },
                        (obj) => _accessService.HasWorkerRightToUpdate(SectionWidget.Section.SectionKey)));
            }
        }

        /// <summary>
        /// Команда вызова окна для просмотра записи.
        /// </summary>
        public RelayCommand ReadCommand
        {
            get
            {
                return _readCommand ??
                        (_readCommand = new RelayCommand((object obj) => {
                            Read();
                        },
                        (obj) => _accessService.HasWorkerRightToRead(SectionWidget.Section.SectionKey)));
            }
        }

        /// <summary>
        /// Команда закрытия вкладки SectionWidget в главном окне.
        /// </summary>
        public RelayCommand CloseCommand
        {
            get
            {
                return _closeCommand ??
                        (_closeCommand = new RelayCommand((object obj) => {
                            Close();
                        }));
            }
        }

        /// <summary>
        /// Команда сохранения изменений из окна работы с записью в базу данных.
        /// </summary>
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ??
                    (_saveCommand = new RelayCommand((object obj) =>
                    {
                        Save();
                    },
                    (obj) => _itemFormMode == ItemFormMode.Insert || _itemFormMode == ItemFormMode.Update));
            }
        }

        /// <summary>
        /// Команда создания PDF-документа на основе данных в разделе.
        /// </summary>
        public RelayCommand PDFCommand
        {
            get
            {
                return _pdfCommand ??
                    (_pdfCommand = new RelayCommand((object obj) => {
                        ToPDF();
                    },
                    (obj) => _accessService.HasWorkerRightToPDF(SectionWidget.Section.SectionKey)));
            }
        }

        /// <summary>
        /// Конструктор класса SectionWidgetViewModel, принимает в качестве параметра ссылку на представление раздела.
        /// </summary>
        /// <param name="sectionWidget">Ссылка на представление раздела.</param>
        protected SectionWidgetViewModel(SectionWidget sectionWidget)
        {
            SectionWidget = sectionWidget;
            _accessService = App.AccessService;
            _pdfGenarator = App.PDFGenerator;
            CollapseButtonsWithoutRights();
            FilterService = new FilterService(this);
            MakeCurrentItemEmpty();
        }

        /// <summary>
        /// Метод вызова окна для вставки записи в БД.
        /// </summary>
        protected virtual void TryInsert()
        {
            MakeCurrentItemEmpty();

            _itemFormMode = ItemFormMode.Insert;
            CreateNewItemForm();
            ItemForm.Title = SectionTitle;
            ItemForm.Mode = _itemFormMode;
            ItemForm.ShowDialog();
        }

        /// <summary>
        /// Метод вызова окна для изменения записи в БД. Если пользователь не выбрал запись, то выводит сообщение об этом.
        /// </summary>
        protected void TryUpdate()
        {
            if (SectionWidget.DataGrid.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите запись!");
            }
            else
            {
                CurrentItemFromContext = SectionWidget.DataGrid.SelectedItem;
                CurrentItem = CurrentItemFromContext.Clone();

                _itemFormMode = ItemFormMode.Update;
                CreateNewItemForm();
                ItemForm.Title = SectionTitle;
                ItemForm.Mode = _itemFormMode;
                ItemForm.ShowDialog();
            }
        }

        /// <summary>
        /// Метод удаления записи. Если пользователь не выбрал запись, то выводит сообщение об этом. Перед удалением, спрашивает пользователя, точно ли тот хочет удалить запись.
        /// </summary>
        protected void TryDelete()
        {
            if (SectionWidget.DataGrid.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите запись!");
            }
            else if (MessageBox.Show("Вы уверены, что хотите удалить запись?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                CurrentItemFromContext = SectionWidget.DataGrid.SelectedItem;
                Delete();
            }
        }

        /// <summary>
        /// Метод вызова окна для просмотра записи. Если пользователь не выбрал запись, то выводит сообщение об этом.
        /// </summary>
        protected void Read()
        {
            if (SectionWidget.DataGrid.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите запись!");
            }
            else
            {
                CurrentItem = SectionWidget.DataGrid.SelectedItem;

                _itemFormMode = ItemFormMode.Read;
                CreateNewItemForm();
                ItemForm.Title = SectionTitle;
                ItemForm.Mode = _itemFormMode;
                ItemForm.ShowDialog();
            }
        }

        /// <summary>
        /// Метод, пытающийся создать PDF-файл на основе данных в разделе.
        /// </summary>
        protected void ToPDF()
        {
            _pdfGenarator.TryCreatePDF(SectionWidget.Section.Title, SectionWidget.DataGrid);
        }

        /// <summary>
        /// Скрывает кнопки работы с данными в разделе, если вошедший пользователь не имеет права на операцию
        /// </summary>
        protected void CollapseButtonsWithoutRights()
        {
            if (!_accessService.HasWorkerRightToInsert(SectionWidget.Section.SectionKey))
            {
                SectionWidget.CollapseInsertButton();
            }
            if (!_accessService.HasWorkerRightToUpdate(SectionWidget.Section.SectionKey))
            {
                SectionWidget.CollapseUpdateButton();
            }
            if (!_accessService.HasWorkerRightToDelete(SectionWidget.Section.SectionKey))
            {
                SectionWidget.CollapseDeleteButton();
            }
            if (!_accessService.HasWorkerRightToRead(SectionWidget.Section.SectionKey))
            {
                SectionWidget.CollapseReadButton();
            }
            if (!_accessService.HasWorkerRightToPDF(SectionWidget.Section.SectionKey))
            {
                SectionWidget.CollapsePDFButton();
            }
        }

        /// <summary>
        /// Метод удаления записи из базы данных. Пытается удалить данные, если сталкивается с ошибкой, то возвращает контекст к исходному состоянию и выводит сообщение о невозможности удаления.
        /// </summary>
        protected virtual void Delete()
        {
            var entry = App.Context.Entry(CurrentItemFromContext);
            try
            {
                DeleteCurrentItem();
                App.Context.SaveChanges();
                UpdateItems();
            }
            catch (Exception ex)
            {
                entry.Reload();
                MessageBox.Show("Вы не можете удалить эту запись, так как она используется в другом разделе");
            }
            finally
            {
                MakeCurrentItemEmpty();
            }
        }

        /// <summary>
        /// Метод сохранения данных из окна работы с записью. Проверяет корректность данных, затем определяет тип операции - вставка или изменение записи.
        /// </summary>
        public void Save()
        {
            FillItem();
            string errors = GetErrors();
            if (errors.Length > 0)
            {
                MessageBox.Show(errors, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (_itemFormMode == ItemFormMode.Insert)
            {
                Insert();
            }
            else if (MessageBox.Show("Вы уверены, что хотите изменить запись?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Update();
            }
        }

        /// <summary>
        /// Метод вставки записи в базу данных. Пытается добавить запись, если возникает ошибка - возвращает контекст к прежнему состоянию.
        /// Закрывает окно работы с записью элемента.
        /// </summary>
        protected virtual void Insert()
        {
            var entry = App.Context.Entry(CurrentItem);
            try
            {
                AddCurrentItem();
                App.Context.SaveChanges();
                UpdateItems();
            }
            catch (Exception ex)
            {
                entry.Reload();
                MessageBox.Show("Добавление записи завершилось ошибкой");
            }
            ItemForm.Close();
        }

        /// <summary>
        /// Метод изменения записи в базе данных. Пытается изменить запись, если возникает ошибка - возвращает контекст к прежнему состоянию.
        /// Закрывает окно работы с записью элемента.
        /// </summary>
        protected virtual void Update()
        {
            try
            {
                CurrentItemFromContext.Copy(CurrentItem);
                App.Context.SaveChanges();
                UpdateItems();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Изменение записи завершилось ошибкой");
            }
            ItemForm.Close();
        }

        /// <summary>
        /// Метод, создающий новый экземпляр формы работы с записью.
        /// </summary>
        protected abstract void CreateNewItemForm();

        /// <summary>
        /// Метод, добавляющий в контекст новую запись.
        /// </summary>
        protected abstract void AddCurrentItem();

        /// <summary>
        /// Метод, удаляющий запись из контекста.
        /// </summary>
        protected abstract void DeleteCurrentItem();

        /// <summary>
        /// Метод, устанавливающий текущей записи пустое значение.
        /// </summary>
        protected abstract void MakeCurrentItemEmpty();

        /// <summary>
        /// Метод проверки корректности данных записи. Возвращает список ошибок, если данные не корректны.
        /// </summary>
        /// <returns>Перечень некорректных атрибутов записи.</returns>
        protected abstract string GetErrors();

        /// <summary>
        /// Метод, который используется в тех случаях, когда нужно заполнить атрибуты записи значениями из элементов управления окна работы с записью.
        /// </summary>
        protected virtual void FillItem() {}

        /// <summary>
        /// Метод, обновляющий записи в таблице раздела.
        /// </summary>
        protected void UpdateItems()
        {
            UpdateSectionData();
            FilterItems();
        }

        /// <summary>
        /// Метод заполнения таблицы раздела коллекцией записей.
        /// </summary>
        public abstract void UpdateSectionData();

        /// <summary>
        /// Метод фильтрации записей в таблице раздела.
        /// </summary>
        protected void FilterItems()
        {
            FilterService.UseFilters();
        }

        /// <summary>
        /// Метод, который вызывает окно фильтрации для столбца в таблице раздела.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ShowFilterWindow(object sender, RoutedEventArgs e)
        {
            var columnHeader = sender as DataGridColumnHeader;
            if (columnHeader != null)
            {
                FilterService.ShowFilterWindow(columnHeader);
            }
        }

        /// <summary>
        /// Метод, закрывающий вкладку раздела в главном окне. 
        /// </summary>
        protected void Close()
        {
            MainWindow mainWindow = App.MainWindow;
            mainWindow.CloseCurrentSection();
        }
    }
}
