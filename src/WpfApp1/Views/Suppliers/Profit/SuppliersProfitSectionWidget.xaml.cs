using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;

namespace WpfApp1.Views
{
    /// <summary>
    /// Представление раздела "Поставщики / Прибыль".
    /// </summary>
    public partial class SuppliersProfitSectionWidget : SectionWidget
    {
        public override Dictionary<string, string> HeadersProperties {
            get 
            {
                return new Dictionary<string, string> {
                    { "Id поставщика", "Id" },
                    { "Поставщик", "Title" },
                    { "Прибыль", "Profit" }
                };
            }
        }

        protected override Button InsertButton 
        { 
            get => btnInsert;
        }
        protected override Button UpdateButton
        {
            get => btnUpdate;
        }
        protected override Button ReadButton
        {
            get => btnRead;
        }
        protected override Button DeleteButton
        {
            get => btnDelete;
        }
        protected override Button PDFButton
        {
            get => btnPDF;
        }

        public override DataGrid DataGrid
        {
            get => dataGrid;
            set
            {
                dataGrid = value;
            }
        }

        /// <summary>
        /// Конструктор класса SuppliersProfitSectionWidget, принимающий в качестве параметра ссылку на модель раздела.
        /// </summary>
        /// <param name="section">Модель раздела.</param>
        public SuppliersProfitSectionWidget(Sections section) : base(section)
        {
            InitializeComponent();
            ViewModel = new ViewModels.Suppliers.SuppliersProfitViewModel(this);
            DataContext = ViewModel;
            DataGrid.ItemsSource = ViewModel.SectionData;
        }

        /// <summary>
        /// Обработчик события двойного клика на заголовок столбца таблицы раздела.
        /// Вызывает окно фильтрации для столбца таблицы раздела.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridColumnHeader_DoubleClick(object sender, RoutedEventArgs e)
        {
            ViewModel.ShowFilterWindow(sender, e);
        }
    }
}
