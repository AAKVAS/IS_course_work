using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;

namespace WpfApp1.Views
{
    /// <summary>
    /// Представление раздела "Склады / Работа сотрудников на складе".
    /// </summary>
    public partial class StoragesWorkerShiftsSectionWidget : SectionWidget
    {
        public override Dictionary<string, string> HeadersProperties {
            get 
            {
                return new Dictionary<string, string> {
                    { "Id", "Id" },
                    { "Id склада", "Storage.Id" },
                    { "Тип склада", "Storage.StorageTypeNavigation.Title" },
                    { "Страна", "Storage.Country" },
                    { "Субъект", "Storage.FederalSubject" },
                    { "Город", "Storage.Locality" },
                    { "Улица", "Storage.Street" },
                    { "Дом", "Storage.HouseNumber" },
                    { "Id сотрудника", "Worker.Id" },
                    { "Фамилия", "Worker.Lastname" },
                    { "Имя", "Worker.Firstname" },
                    { "Отчество", "Worker.Patronymic" },
                    { "Должность", "Worker.Post.Title" },
                    { "Начало смены", "StartedShiftAt" },
                    { "Конец смены", "FinishedShiftAt" }
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
        /// Конструктор класса StoragesWorkerShiftsSectionWidget, принимающий в качестве параметра ссылку на модель представления раздела.
        /// </summary>
        /// <param name="sectionWidgetViewModel">Модель представления раздела.</param>
        public StoragesWorkerShiftsSectionWidget(Sections section) : base(section)
        {
            InitializeComponent();
            ViewModel = new ViewModels.Storages.StoragesWorkerShiftsViewModel(this);
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
