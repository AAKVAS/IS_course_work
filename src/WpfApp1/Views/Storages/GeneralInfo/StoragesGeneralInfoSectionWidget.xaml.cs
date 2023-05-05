using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;

namespace WpfApp1.Views
{
    /// <summary>
    /// Представление раздела "Склады / Общие сведения".
    /// </summary>
    public partial class StoragesGeneralInfoSectionWidget : SectionWidget
    {
        public override Dictionary<string, string> HeadersProperties {
            get 
            {
                return new Dictionary<string, string> {
                    { "Id", "Id" },
                    { "Тип склада", "StorageTypeNavigation.Title" },
                    { "Страна", "Country" },
                    { "Субъект", "FederalSubject" },
                    { "Город", "Locality" },
                    { "Улица", "Street" },
                    { "Дом", "HouseNumber" }
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
        /// Конструктор класса StoragesGeneralInfoSectionWidget, принимающий в качестве параметра ссылку на модель представления раздела.
        /// </summary>
        /// <param name="sectionWidgetViewModel">Модель представления раздела.</param>
        public StoragesGeneralInfoSectionWidget(Sections section) : base(section)
        {
            InitializeComponent();
            ViewModel = new ViewModels.Storages.StoragesGeneralInfoViewModel(this);
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
