using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    public partial class OrdersListSectionWidget : SectionWidget
    {
        public override Dictionary<string, string> HeadersProperties {
            get 
            {
                return new Dictionary<string, string> {
                    { "Id", "Id" },
                    { "Id пользователя", "UserId" },
                    { "Фамилия", "User.Lastname" },
                    { "Имя", "User.Firstname" },
                    { "Отчество", "User.Patronymic" },
                    { "Id товара", "ProductId" },
                    { "Товар", "Product.Title" },
                    { "Стоимость заказа", "Price" },
                    { "Количество товара", "ProductCount" },
                    { "Id пункта выдачи", "PickUpPointId" },
                    { "Страна", "PickUpPoint.Country" },
                    { "Субъект", "PickUpPoint.FederalSubject" },
                    { "Город", "PickUpPoint.Locality" },
                    { "Улица", "PickUpPoint.Street" },
                    { "Дом", "PickUpPoint.HouseNumber" },
                    { "Дата формирования заказа", "CreatedAt" },
                    { "Ориентировочная дата выдачи", "EstimatedDeliveryAt" }
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

        public OrdersListSectionWidget(Sections section) : base(section)
        {
            InitializeComponent();
            ViewModel = new ViewModels.Orders.OrdersListViewModel(this);
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
