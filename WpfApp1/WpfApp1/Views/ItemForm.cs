
using System.Windows;

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

        public ItemForm() {}

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
