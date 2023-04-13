using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    public partial class StorageProductAmountSectionWidget : SectionWidget
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
                    { "Id товара", "Product.Id" },
                    { "Название", "Product.Title" },
                    { "Количество товара", "ProductAmount" }
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

        public StorageProductAmountSectionWidget(Sections section) : base(section)
        {
            InitializeComponent();
            ViewModel = new ViewModels.Storages.StorageProductAmountViewModel(this);
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
