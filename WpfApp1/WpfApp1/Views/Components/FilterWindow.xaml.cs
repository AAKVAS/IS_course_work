using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Services;

namespace WpfApp1.Views.Components
{
    /// <summary>
    /// Логика взаимодействия для SearchWindow.xaml
    /// </summary>
    public partial class FilterWindow : Window
    {
        private DataGridColumnHeader _columnHeader;
        private FilterService _filterService;

        public FilterWindow(FilterService filterService, DataGridColumnHeader columnHeader)
        {
            InitializeComponent();
            _columnHeader = columnHeader;
            _filterService = filterService;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            FilterTypes filterTypes = FilterTypes.All;

            switch(cbFilterType.SelectedIndex)
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
    }
}
