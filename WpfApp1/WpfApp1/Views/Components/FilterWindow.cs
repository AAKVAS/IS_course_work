using System.Windows;
using System.Windows.Controls.Primitives;
using WpfApp1.Services;

namespace WpfApp1.Views.Components
{

    public abstract class FilterWindow : Window
    {
        protected DataGridColumnHeader _columnHeader;
        protected FilterService _filterService;

        public FilterWindow(FilterService filterService, DataGridColumnHeader columnHeader)
        {
            _columnHeader = columnHeader;
            _filterService = filterService;
        }

        protected abstract void Filter();
    }
}
