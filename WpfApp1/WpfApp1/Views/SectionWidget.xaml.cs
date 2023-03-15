using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    /// <summary>
    /// Логика взаимодействия для ChapterForm.xaml
    /// </summary>
    public partial class SectionWidget : UserControl
    {
        public string SectionKey { get; set; }
        private SectionWidgetViewModel _viewModel;

        public SectionWidget(string sectionKey)
        {
            SectionKey = sectionKey;
        }

        public void SetViewModelAndInitialize(SectionWidgetViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            DataContext = _viewModel;
        }

        public void CollapseInsertButton()
        {
            insertButton.Visibility = Visibility.Collapsed;
        }

        public void CollapseUpdateButton()
        {
            updateButton.Visibility = Visibility.Collapsed;
        }

        public void CollapseDeleteButton()
        {
            deleteButton.Visibility = Visibility.Collapsed;
        }

        public void CollapseReadButton()
        {
            readButton.Visibility = Visibility.Collapsed;
        }

        private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string column_name = e.Column.Header.ToString();
            e.Column.Header = _viewModel.GetTableHeaderName(column_name);
        }
    }
}
