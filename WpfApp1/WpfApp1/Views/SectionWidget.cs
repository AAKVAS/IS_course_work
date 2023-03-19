using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{

    public abstract class SectionWidget : UserControl
    {
        public Sections Section;

        protected abstract SectionWidgetViewModel ViewModel { get; set; }

        protected abstract Button InsertButton { get; }
        protected abstract Button DeleteButton { get; }
        protected abstract Button UpdateButton { get; }
        protected abstract Button ReadButton { get; }
        protected abstract Button PDFButton { get; }

        public abstract DataGrid DataGrid { get; set; }

        public SectionWidget(Sections section)
        {
            Section = section;
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

        public void CollapsePDFButton()
        {
            PDFButton.Visibility = Visibility.Collapsed;
        }

    }
}
