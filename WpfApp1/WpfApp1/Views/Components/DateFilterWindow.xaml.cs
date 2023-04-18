using System.Windows;
using System.Windows.Controls.Primitives;
using WpfApp1.Services;

namespace WpfApp1.Views.Components
{
    /// <summary>
    /// Логика взаимодействия для DateFilterWindow.xaml
    /// </summary>
    public partial class DateFilterWindow : FilterWindow
    {

        public DateFilterWindow(FilterService filterService, DataGridColumnHeader columnHeader) : base(filterService, columnHeader)
        {
            InitializeComponent();
            dpParam.Text = _filterService.GetFilterValueByColumn(_columnHeader);
            SelectFilterType();
        }

        private void SelectFilterType()
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
            _filterService.Filter(filterTypes, _columnHeader, dpParam.Text);
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
