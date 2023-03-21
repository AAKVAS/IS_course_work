using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.ViewModels;
using WpfApp1.Views.Components;

namespace WpfApp1.Views
{

    public abstract class SectionWidget : UserControl
    {
        public Sections Section;

        public abstract SectionWidgetViewModel ViewModel { get; set; }

        protected abstract Button InsertButton { get; }
        protected abstract Button DeleteButton { get; }
        protected abstract Button UpdateButton { get; }
        protected abstract Button ReadButton { get; }
        protected abstract Button PDFButton { get; }
        public abstract Dictionary<string, string> HeadersProperties { get; }

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
