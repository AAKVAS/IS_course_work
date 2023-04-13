﻿using System.Windows;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Suppliers;

namespace WpfApp1.Views.Suppliers.Profit
{
    public partial class SuppliersProfitItem: ItemForm
    {
        public SuppliersProfitItem(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            DataContext = (SuppliersProfitViewModel)_sectionWidgetViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected override void SetFormModeToInsert() {}

        protected override void SetFormModeToUpdate() {}

        protected override void SetFormModeToRead() {}
    }
}
