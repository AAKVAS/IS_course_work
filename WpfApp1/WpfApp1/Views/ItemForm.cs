
using System.Windows;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    public enum ItemFormMode
    {
        Insert,
        Update,
        Read
    }


    public abstract class ItemForm : Window
    {
        protected SectionWidgetViewModel _sectionWidgetViewModel;
        private ItemFormMode _mode = ItemFormMode.Read;
        public ItemFormMode Mode { 
            get
            {
                return _mode;
            }
            set
            {
                _mode = value;
                UpdateForm();
            }
        }

        public ItemForm(SectionWidgetViewModel sectionWidgetViewModel) {
            _sectionWidgetViewModel = sectionWidgetViewModel;
        }

        private void UpdateForm()
        {
            switch (_mode)
            {
                case ItemFormMode.Insert:
                    SetFormModeToInsert();
                    break;
                case ItemFormMode.Update:
                    SetFormModeToUpdate();
                    break;
                case ItemFormMode.Read:
                    SetFormModeToRead();
                    break;
            }
        }

        protected abstract void SetFormModeToInsert();
        protected abstract void SetFormModeToUpdate();
        protected abstract void SetFormModeToRead();

    }
}
