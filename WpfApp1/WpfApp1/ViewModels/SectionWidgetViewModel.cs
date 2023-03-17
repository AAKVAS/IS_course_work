using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Services;
using WpfApp1.Views;

namespace WpfApp1.ViewModels
{
    public abstract class SectionWidgetViewModel
    {
        protected SectionWidget _sectionWidget;
        protected abstract ItemForm ItemForm { get; set; }
        public abstract ObservableCollection<dynamic> SectionData { get; }
        public abstract dynamic? CurrentItem { get; set; }
        protected abstract Dictionary<string, string> SectionTableHeaders { get; }

        protected ItemFormMode _itemFormMode = ItemFormMode.Read;

        protected RelayCommand? _insertCommand;
        protected RelayCommand? _deleteCommand;
        protected RelayCommand? _updateCommand;
        protected RelayCommand? _readCommand;
        protected RelayCommand? _closeCommand;
        protected RelayCommand? _saveCommand;

        public RelayCommand InsertCommand
        {
            get
            {
                return _insertCommand ??
                        (_insertCommand = new RelayCommand((object obj) => {
                            TryInsert();
                        },
                        (obj) => _accessService.HasWorkerRightToInsert(_sectionWidget.SectionKey)));
            }
        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return _deleteCommand ??
                        (_deleteCommand = new RelayCommand((object obj) => {
                            Delete();
                        },
                        (obj) => _accessService.HasWorkerRightToDelete(_sectionWidget.SectionKey)));
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
                        (obj) => _accessService.HasWorkerRightToUpdate(_sectionWidget.SectionKey)));
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
                        (obj) => _accessService.HasWorkerRightToRead(_sectionWidget.SectionKey)));
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


        protected AccessService _accessService;

        public SectionWidgetViewModel(SectionWidget sectionWidget)
        {
            _sectionWidget = sectionWidget;
            _accessService = (AccessService)Application.Current.Resources["AccessService"];
            CollapseButtonsWithoutRights();
        }

        protected void CollapseButtonsWithoutRights() {
            if (!_accessService.HasWorkerRightToInsert(_sectionWidget.SectionKey))
            {
                _sectionWidget.CollapseInsertButton();
            }
            if (!_accessService.HasWorkerRightToUpdate(_sectionWidget.SectionKey))
            {
                _sectionWidget.CollapseUpdateButton();
            }
            if (!_accessService.HasWorkerRightToDelete(_sectionWidget.SectionKey))
            {
                _sectionWidget.CollapseDeleteButton();
            }
            if (!_accessService.HasWorkerRightToRead(_sectionWidget.SectionKey))
            {
                _sectionWidget.CollapseReadButton();
            }
        }

        public string GetTableHeaderName(string columnName)
        {
            string header;
            if (SectionTableHeaders.TryGetValue(columnName, out header))
            {
                return header;
            };
            return columnName;
        }

        protected abstract void CreateNewItemForm();

        protected void TryInsert()
        {
            MakeCurrentItemEmpty();
            CreateNewItemForm();
            _itemFormMode = ItemFormMode.Insert;
            ItemForm.Mode = _itemFormMode;
            ItemForm.Show();      
        }

        protected void TryUpdate()
        {
            if (_sectionWidget.dataGrid.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите запись!");
            }
            else
            {
                CurrentItem = _sectionWidget.dataGrid.SelectedItem;

                CreateNewItemForm();
                _itemFormMode = ItemFormMode.Update;
                ItemForm.Mode = _itemFormMode;
                ItemForm.Show(); 
            }
        }

        protected void Delete()
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить запись?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                CurrentItem = _sectionWidget.dataGrid.SelectedItem;
                App.Context.Users.Remove(CurrentItem);
                App.Context.SaveChanges();
                UpdateItems();
            }
        }

        protected void Read()
        {
            CreateNewItemForm();
            _itemFormMode = ItemFormMode.Read;
            ItemForm.Mode = _itemFormMode;
            ItemForm.Show();
        }

        public void Save()
        {
            if (_itemFormMode == ItemFormMode.Insert)
            {
                AddCurrentItem();
            }
            App.Context.SaveChanges();
            UpdateItems();
        }

        protected abstract void AddCurrentItem();
        protected abstract void MakeCurrentItemEmpty();

        private void UpdateItems()
        {
            _sectionWidget.dataGrid.ItemsSource = SectionData;
        }

        protected void ToPDF()
        {

        }

        protected void Close()
        {
            MainWindow mainWindow = (MainWindow)Application.Current.Resources["MainWindow"];
            mainWindow.CloseCurrentSection();
        }
    }
}
