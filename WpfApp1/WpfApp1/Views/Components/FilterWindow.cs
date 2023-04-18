using System.Windows;
using System.Windows.Controls.Primitives;
using WpfApp1.Services;

namespace WpfApp1.Views.Components
{
    /// <summary>
    /// Абстрактный класс, представляющий собой окно фильтрации данных.
    /// </summary>
    public abstract class FilterWindow : Window
    {
        /// <summary>
        /// Заголовок столбца, по которому будет происходить фильтрация.
        /// </summary>
        protected DataGridColumnHeader _columnHeader;

        /// <summary>
        /// Ссылка на сервис фильтрации.
        /// </summary>
        protected FilterService _filterService;

        /// <summary>
        /// Конструктор класса FilterWindow, принимающий в качестве параметров сервис фильтрации и заголовок столбца.
        /// </summary>
        /// <param name="filterService">Сервис фильтрации.</param>
        /// <param name="columnHeader">Заголовок столбца.</param>
        public FilterWindow(FilterService filterService, DataGridColumnHeader columnHeader)
        {
            _columnHeader = columnHeader;
            _filterService = filterService;
        }

        /// <summary>
        /// Абстрактный метод, который устанавливает в списке видов фильтров текущий выбранный фильтр.
        /// </summary>
        protected abstract void SelectFilterType();

        /// <summary>
        /// Абстрактный метод применения фильтров.
        /// </summary>
        protected abstract void Filter();
    }
}
