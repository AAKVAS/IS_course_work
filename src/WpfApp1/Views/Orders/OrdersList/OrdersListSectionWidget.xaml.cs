using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;

namespace WpfApp1.Views
{
    /// <summary>
    /// Представление раздела "Доставки / Список доставок".
    /// </summary>
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

        /// <summary>
        /// Конструктор класса OrdersListSectionWidget, принимающий в качестве параметра ссылку на модель раздела.
        /// </summary>
        /// <param name="section">Модель раздела.</param>
        public OrdersListSectionWidget(Sections section) : base(section)
        {
            InitializeComponent();
            ViewModel = new ViewModels.Orders.OrdersListViewModel(this);
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
