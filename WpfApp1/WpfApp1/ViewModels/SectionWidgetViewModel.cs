using System;
using System.Collections.Generic;
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
        protected abstract ItemForm ItemForm { get; }

        protected RelayCommand? _insertCommand;
        protected RelayCommand? _deleteCommand;
        protected RelayCommand? _updateCommand;
        protected RelayCommand? _readCommand;
        protected RelayCommand? _closeCommand;

        public RelayCommand InsertCommand
        {
            get
            {
                return _insertCommand ??
                        (_insertCommand = new RelayCommand((object obj) => {
                            Insert();
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
                            Update();
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

        protected void Insert()
        {
            ItemForm.Show();
        }

        protected void Update()
        {
            ItemForm.Show();
        }

        protected void Delete()
        {
            MessageBox.Show("Delete...");
        }

        protected void Read()
        {
            ItemForm.Show();
        }

        protected void ToPDF()
        {

        }

        protected void Close()
        {

        }
    }
}
