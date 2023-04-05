using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls.Primitives;
using WpfApp1.Services;
using WpfApp1.Views;

namespace WpfApp1.ViewModels
{
    public abstract class SectionWidgetViewModel
    {
        public SectionWidget SectionWidget;
        public abstract ItemForm ItemForm { get; set; }
        private PDFGenerateService _pdfGenerateService;
        public abstract ObservableCollection<dynamic> SectionData { get; set; }
        public dynamic? CurrentItemFromContext { get; set; }
        public abstract dynamic? CurrentItem { get; set; }

        protected ItemFormMode _itemFormMode = ItemFormMode.Read;
        public string SectionTitle { get; set; }

        public FilterService FilterService { get; set; }

        protected RelayCommand? _insertCommand;
        protected RelayCommand? _deleteCommand;
        protected RelayCommand? _updateCommand;
        protected RelayCommand? _readCommand;
        protected RelayCommand? _closeCommand;
        protected RelayCommand? _saveCommand;
        protected RelayCommand? _pdfCommand;

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


        protected AccessService _accessService;

        public SectionWidgetViewModel(SectionWidget sectionWidget)
        {
            SectionWidget = sectionWidget;
            _accessService = App.AccessService;
            _pdfGenerateService = App.PDFGenerateService;
            CollapseButtonsWithoutRights();
            FilterService = new FilterService(this);
            MakeCurrentItemEmpty();
        }

        protected void CollapseButtonsWithoutRights() {
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
        }

        protected abstract void CreateNewItemForm();

        protected void TryInsert()
        {
            MakeCurrentItemEmpty();

            CreateNewItemForm();
            ItemForm.Title = SectionTitle;
            _itemFormMode = ItemFormMode.Insert;
            ItemForm.Mode = _itemFormMode;
            ItemForm.Show();      
        }

        protected void TryUpdate()
        {
            if (SectionWidget.DataGrid.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите запись!");
            }
            else {
                CurrentItemFromContext = SectionWidget.DataGrid.SelectedItem;
                CurrentItem = CurrentItemFromContext.Clone();

                CreateNewItemForm();
                ItemForm.Title = SectionTitle;
                _itemFormMode = ItemFormMode.Update;
                ItemForm.Mode = _itemFormMode;
                ItemForm.Show();
            }
        }

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

        protected virtual void Delete()
        {
            var entry = App.Context.Entry(CurrentItemFromContext);
            try
            {
                DeleteCurrentItem();
                App.Context.SaveChanges();
                UpdateItems();
            }
            catch (DbUpdateException ex)
            {
                entry.Reload();
                MakeCurrentItemEmpty();
                MessageBox.Show("Вы не можете удалить эту запись, так как она используется в другом разделе");
            }
        }

        protected void Read()
        {
            if (SectionWidget.DataGrid.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите запись!");
            }
            else
            {
                CurrentItem = SectionWidget.DataGrid.SelectedItem;

                CreateNewItemForm();
                ItemForm.Title = SectionTitle;
                _itemFormMode = ItemFormMode.Read;
                ItemForm.Mode = _itemFormMode;
                ItemForm.Show();
            }
        }

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
                CurrentItemFromContext.Copy(CurrentItem);
                App.Context.SaveChanges();
                ItemForm.Close();
                UpdateItems();
            }
        }

        protected virtual void Insert()
        {
            var entry = App.Context.Entry(CurrentItem);
            try
            {
                AddCurrentItem();
                App.Context.SaveChanges();
                ItemForm.Close();
                UpdateItems();
            }
            catch (Exception ex)
            {
                entry.Reload();
                MessageBox.Show("Вы не можете вставить запись, так как её ключ уже используется");
                ItemForm.Close();
            }
        }

        protected abstract void AddCurrentItem();
        protected abstract void DeleteCurrentItem();
        protected abstract void MakeCurrentItemEmpty();
        protected abstract void FillItem();
        protected abstract string GetErrors();

        protected void UpdateItems()
        {
            FilterService.UseFilters();
        }

        public abstract void UpdateSectionData();

        protected void ToPDF()
        {
            _pdfGenerateService.TryCreatePDF(SectionWidget.Section.Title, SectionWidget.DataGrid);
        }

        public void ShowFilterWindow(object sender, RoutedEventArgs e)
        {
            var columnHeader = sender as DataGridColumnHeader;
            if (columnHeader != null)
            {
                FilterService.ShowFilterWindow(columnHeader);
            }
        }

        protected void Close()
        {
            MainWindow mainWindow = App.MainWindow;
            mainWindow.CloseCurrentSection();
        }
    }
}
