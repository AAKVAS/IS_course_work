using System.Windows;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    /// <summary>
    /// Перечисление, описывающее режимы окна записи.
    /// Режимы: вставка, изменение, просмотр
    /// </summary>
    public enum ItemFormMode
    {
        Insert,
        Update,
        Read
    }

    /// <summary>
    /// Абстрактный класс окна работы с записью раздела.
    /// </summary>
    public abstract class ItemForm : Window
    {
        /// <summary>
        /// Ссылка на модель представления раздела.
        /// </summary>
        protected SectionWidgetViewModel _sectionWidgetViewModel;

        /// <summary>
        /// Режим работы с записью раздела.
        /// </summary>
        protected ItemFormMode _mode = ItemFormMode.Read;

        /// <summary>
        /// Режим работы с записью раздела.
        /// </summary>
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

        /// <summary>
        /// Конструктор класса ItemForm, принимающий в качестве параметра ссылку на модель представления раздела.
        /// </summary>
        /// <param name="sectionWidgetViewModel">Модель представления раздела.</param>
        public ItemForm(SectionWidgetViewModel sectionWidgetViewModel) {
            _sectionWidgetViewModel = sectionWidgetViewModel;
        }

        /// <summary>
        /// Метод, меняющий внешний вид окна, в зависимости от режима работы с записью.
        /// </summary>
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

        /// <summary>
        /// Абстрактный метод, меняющий внешний вид окна для вставки записи в БД.
        /// </summary>
        protected abstract void SetFormModeToInsert();

        /// <summary>
        /// Абстрактный метод, меняющий внешний вид окна для изменения записи в БД.
        /// </summary>
        protected abstract void SetFormModeToUpdate();

        /// <summary>
        /// Абстрактный метод, меняющий внешний вид окна для просмотра записи.
        /// </summary>
        protected abstract void SetFormModeToRead();

        /// <summary>
        /// Метод, закрывающий окно работы с записью, если окно открыто для просмотра или пользователь уверен, что окно должно быть закрыто.
        /// </summary>
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
