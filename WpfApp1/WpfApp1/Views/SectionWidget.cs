using System.Windows;
using System.Windows.Controls;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{

    public abstract class SectionWidget : UserControl
    {
        public string SectionKey { get; set; }
        protected abstract SectionWidgetViewModel ViewModel { get; set; }

        protected abstract Button InsertButton { get; }
        protected abstract Button DeleteButton { get; }
        protected abstract Button UpdateButton { get; }
        protected abstract Button ReadButton { get; }

        public abstract DataGrid DataGrid { get; set; }

        public SectionWidget(string sectionKey)
        {
            SectionKey = sectionKey;
        }

        public void CollapseInsertButton()
        {
            InsertButton.Visibility = Visibility.Collapsed;
        }

        public void CollapseUpdateButton()
        {
            UpdateButton.Visibility = Visibility.Collapsed;
        }

        public void CollapseDeleteButton()
        {
            DeleteButton.Visibility = Visibility.Collapsed;
        }

        public void CollapseReadButton()
        {
            ReadButton.Visibility = Visibility.Collapsed;
        }

    }
}
