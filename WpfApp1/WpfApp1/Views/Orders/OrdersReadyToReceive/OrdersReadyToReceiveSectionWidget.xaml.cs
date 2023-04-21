using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;

namespace WpfApp1.Views
{
    /// <summary>
    /// Представление раздела "Доставки / Доставки, готовые к получению".
    /// </summary>
    public partial class OrdersReadyToReceiveSectionWidget : SectionWidget
    {
        public override Dictionary<string, string> HeadersProperties {
            get 
            {
                return new Dictionary<string, string> {
                    { "Id пункта выдачи", "Order.PickUpPoint.Id" },
                    { "Страна", "Order.PickUpPoint.Country" },
                    { "Субъект", "Order.PickUpPoint.FederalSubject" },
                    { "Город", "Order.PickUpPoint.Locality" },
                    { "Улица", "Order.PickUpPoint.Street" },
                    { "Дом", "Order.PickUpPoint.HouseNumber" },
                    { "Id заказа", "Order.Id" },
                    { "Id пользователя", "Order.User.Id" },
                    { "Фамилия", "Order.User.Lastname" },
                    { "Имя", "Order.User.Firstname" },
                    { "Отчество", "Order.User.Patronymic" },
                    { "Код для получения заказа", "Order.User.OrderCode" },
                    { "Id товара", "Order.Product.Id" },
                    { "Товар", "Order.Product.Title" },
                    { "Количество товара", "Order.ProductCount" },
                    { "Стоимость заказа", "Order.Price" }
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
        /// Конструктор класса OrdersReadyToReceiveSectionWidget, принимающий в качестве параметра ссылку на модель раздела.
        /// </summary>
        /// <param name="section">Модель раздела.</param>
        public OrdersReadyToReceiveSectionWidget(Sections section) : base(section)
        {
            InitializeComponent();
            ViewModel = new ViewModels.Orders.OrdersReadyToReceiveViewModel(this);
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
