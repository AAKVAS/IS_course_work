using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    public partial class OrderHistorySectionWidget : SectionWidget
    {
        public override Dictionary<string, string> HeadersProperties {
            get 
            {
                return new Dictionary<string, string> {
                    { "Id заказ", "OrderId" },
                    { "Id товара", "ProductId" },
                    { "Товар", "ProductTitle" },
                    { "Статус заказа", "StatusDescription" },
                    { "Id склада", "StorageId" },
                    { "Тип склада", "StorageTitle" },
                    { "Страна", "Country" },
                    { "Субъект", "FederalSubject" },
                    { "Город", "Locality" },
                    { "Улица", "Street" },
                    { "Дом", "HouseNumber" },
                    { "Дата изменения статуса", "StatusChangedAt" },
                    { "Id сотрудника", "WorkerId" },
                    { "Фамилия", "WorkerLastname" },
                    { "Имя", "WorkerFirstname" },
                    { "Отчество", "WorkerPatronymic" },
                    { "Должность", "WorkerPost" }
                };
            }
        }

        protected override Button InsertButton { 
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

        public override SectionWidgetViewModel ViewModel { get; set; }

        public OrderHistorySectionWidget(Sections section) : base(section)
        {
            InitializeComponent();
            ViewModel = new ViewModels.Orders.OrderHistoryViewModel(this);
            DataContext = ViewModel;
            DataGrid.ItemsSource = ViewModel.SectionData;
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            DataContext = ViewModel;
        }

        private void DataGridColumnHeader_DoubleClick(object sender, RoutedEventArgs e)
        {
            ViewModel.ShowFilterWindow(sender, e);
        }
    }
}
