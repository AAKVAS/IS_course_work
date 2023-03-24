using System.Windows.Controls;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{

    public abstract class ItemWithImages : ItemForm
    {
        public abstract ListBox ListBox { get; set; }

        public ItemWithImages(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel) {}
    }
}
