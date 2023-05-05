using System.Windows;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Suppliers;

namespace WpfApp1.Views.Suppliers.Profit
{
    /// <summary>
    /// Окно работы с записью раздела "Поставщики / Прибыль".
    /// </summary>
    public partial class SuppliersProfitItem: ItemForm
    {
        /// <summary>
        /// Конструктор класса SuppliersProfitItem, принимающий в качестве параметра ссылку на модель представления раздела.
        /// </summary>
        /// <param name="sectionWidgetViewModel">Модель представления раздела.</param>
        public SuppliersProfitItem(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            DataContext = (SuppliersProfitViewModel)_sectionWidgetViewModel;
        }

        /// <summary>
        /// Обработчик нажатия на кнопку закрытия окна.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            TryCloseForm();
        }

        //Раздел только для просмотра данных.
        protected override void SetFormModeToInsert() {}
        protected override void SetFormModeToUpdate() {}
        protected override void SetFormModeToRead() {}
    }
}
