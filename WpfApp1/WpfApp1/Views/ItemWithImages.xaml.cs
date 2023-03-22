using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    /// <summary>
    /// Логика взаимодействия для ItemWithImages.xaml
    /// </summary>
    public abstract partial class ItemWithImages : ItemForm
    {
        public ItemWithImages(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
        }
    }
}
