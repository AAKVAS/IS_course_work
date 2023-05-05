using System.Windows;
using System.Windows.Controls.Primitives;
using WpfApp1.Services;

namespace WpfApp1.Views.Components
{
    /// <summary>
    /// Класс, представляющий окно фильтрации дат.
    /// </summary>
    public partial class DateFilterWindow : FilterWindow
    {
        /// <summary>
        /// Конструктор класса DateFilterWindow, принимающий в качестве параметров сервис фильтрации и заголовок столбца.
        /// </summary>
        /// <param name="filterService">Сервис фильтрации.</param>
        /// <param name="columnHeader">Заголовок столбца.</param>
        public DateFilterWindow(FilterService filterService, DataGridColumnHeader columnHeader) : base(filterService, columnHeader)
        {
            InitializeComponent();
            dpParam.Text = _filterService.GetFilterValueByColumn(_columnHeader);
            SelectFilterType();
        }

        /// <summary>
        /// Метод, который устанавливает в списке видов фильтров текущий выбранный фильтр.
        /// </summary>
        protected override void SelectFilterType()
        {
            switch (_filterService.GetFilterTypeByColumn(_columnHeader))
            {
                case FilterTypes.WithoutFilter:
                    cbFilterType.SelectedIndex = 0;
                    break;
                case FilterTypes.Equals:
                    cbFilterType.SelectedIndex = 1;
                    break;
                case FilterTypes.NotEquals:
                    cbFilterType.SelectedIndex = 2;
                    break;
                case FilterTypes.MoreThan:
                    cbFilterType.SelectedIndex = 3;
                    break;
                case FilterTypes.LessThan:
                    cbFilterType.SelectedIndex = 4;
                    break;
                default:
                    cbFilterType.SelectedIndex = 0;
                    break;
            }
        }

        /// <summary>
        /// Метод применения фильтров.
        /// </summary>
        protected override void Filter()
        {
            FilterTypes filterTypes = FilterTypes.WithoutFilter;

            switch (cbFilterType.SelectedIndex)
            {
                case 1:
                    filterTypes = FilterTypes.Equals;
                    break;
                case 2:
                    filterTypes = FilterTypes.NotEquals;
                    break;
                case 3:
                    filterTypes = FilterTypes.MoreThan;
                    break;
                case 4:
                    filterTypes = FilterTypes.LessThan;
                    break;
            }
            _filterService.SetFilter(filterTypes, _columnHeader, dpParam.Text);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            Filter();
        }
    }
}
