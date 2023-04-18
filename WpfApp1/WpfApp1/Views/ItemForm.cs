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
        protected ItemFormMode _mode = ItemFormMode.Read;
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

        protected void TryCloseForm()
        {
            if (_mode == ItemFormMode.Read)
            {
                Close();
            }
            else if (MessageBox.Show("Возможно есть не сохранённые данные.\nВы уверены, что хотите закрыть окно?",
                                "Предупреждение",
                                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Close();
            }
        }

    }
}
