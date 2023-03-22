using System.Windows;
using System.Windows.Controls.Primitives;
using WpfApp1.Services;

namespace WpfApp1.Views.Components
{
    /// <summary>
    /// Логика взаимодействия для SearchWindow.xaml
    /// </summary>
    public partial class StringFilterWindow : FilterWindow
    {

        public StringFilterWindow(FilterService filterService, DataGridColumnHeader columnHeader) : base(filterService, columnHeader)
        {
            InitializeComponent();
            tbParam.Text = _filterService.GetFilterValueByColumn(_columnHeader);
            SelectFilterType();
        }

        protected override void Filter()
        {
            FilterTypes filterTypes = FilterTypes.All;

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
                case 5:
                    filterTypes = FilterTypes.Contains;
                    break;
            }
            _filterService.Filter(filterTypes, _columnHeader, tbParam.Text);
        }

        private void SelectFilterType()
        {
            switch (_filterService.GetFilterTypeByColumn(_columnHeader))
            {
                case FilterTypes.All:
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
                case FilterTypes.Contains:
                    cbFilterType.SelectedIndex = 5;
                    break;
                default:
                    cbFilterType.SelectedIndex = 0;
                    break;
            }
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
