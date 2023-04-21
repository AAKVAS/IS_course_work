using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;
using WpfApp1.Models.DTO;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Orders;

namespace WpfApp1.Views
{
    /// <summary>
    /// Представление раздела "Доставки / История заказов / Сотрудники в заказах".
    /// </summary>
    public partial class OrderHistoryWorkersSectionWidget : SectionWidget
    {
        /// <summary>
        /// Пустой словарь, так как фильтрация данных в таблице сотрудников, причастных к изменению статуса заказа не предусмотренна.
        /// </summary>
        public override Dictionary<string, string> HeadersProperties 
        {
            get 
            {
                return new Dictionary<string, string> {};
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
        /// Ссылка на модель представления раздела "Доставки / История заказов".
        /// </summary>
        public OrderHistoryViewModel OrderHistoryViewModel { get; set; }

        /// <summary>
        /// Ссылка на текущую запись в разделе "Доставки / История заказов".
        /// </summary>
        public OrderHistoryDTO CurrentOrderHistoryDTO { get; set; }

        /// <summary>
        /// Конструктор класса OrderHistoryWorkersSectionWidget, принимающий в качестве параметров модель раздела и ссылку на модель представления раздела "Доставки / История заказов".
        /// </summary>
        /// <param name="section">Модель раздела.</param>
        /// <param name="orderHistoryViewModel">Модель представления  раздела "Доставки / История заказов".</param>
        public OrderHistoryWorkersSectionWidget(Sections section, OrderHistoryViewModel orderHistoryViewModel) : base(section)
        {
            InitializeComponent();
            //Запоминаем ссылку на модель представления раздела "Доставки / История заказов".
            OrderHistoryViewModel = orderHistoryViewModel;
            ///Запоминаем текущую запись раздела "Доставки / История заказов".
            CurrentOrderHistoryDTO = orderHistoryViewModel.CurrentItem;
            ViewModel = new OrderHistoryWorkersViewModel(this);
            DataContext = ViewModel;
            DataGrid.ItemsSource = ViewModel.SectionData;
        }
    }
}
