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
    public partial class OrderHistoryWorkersSectionWidget : SectionWidget
    {
        public override Dictionary<string, string> HeadersProperties {
            get 
            {
                return new Dictionary<string, string> {};
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
        
        public OrderHistoryDTO OrderHistoryDTO { get; set; }
        public OrderHistoryViewModel OrderHistoryViewModel { get; set; }

        public OrderHistoryWorkersSectionWidget(Sections section, OrderHistoryViewModel orderHistoryViewModel) : base(section)
        {
            InitializeComponent();
            OrderHistoryDTO = orderHistoryViewModel.CurrentItem;
            OrderHistoryViewModel = orderHistoryViewModel;
            ViewModel = new OrderHistoryWorkersViewModel(this);
            DataContext = ViewModel;
            DataGrid.ItemsSource = ViewModel.SectionData;
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            DataContext = ViewModel;
        }

    }
}
