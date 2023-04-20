using System;
using System.Collections.ObjectModel;
using WpfApp1.Views;
using WpfApp1.Services;
using WpfApp1.Views.Suppliers.Profit;

namespace WpfApp1.ViewModels.Suppliers
{
    internal class SuppliersProfitViewModel : SectionWidgetViewModel
    {
        private SuppliersProfitItem _itemForm;
        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as SuppliersProfitItem;
        }

        private ObservableCollection<dynamic> _sectionData;
        public override ObservableCollection<dynamic> SectionData
        {
            get => _sectionData;
            set => _sectionData = value;
        }

        private dynamic _currentItem;
        public override dynamic? CurrentItem
        {
            get => _currentItem;
            set => _currentItem = value;
        }

        public SuppliersProfitViewModel(SectionWidget sectionWidget) : base(sectionWidget) {
	        UpdateSectionData();
        }

        protected override void MakeCurrentItemEmpty()
        {
            _currentItem = new Models.DTO.SuppliersProfitDTO();
        }

        protected override void CreateNewItemForm()
        {
            _itemForm = new SuppliersProfitItem(this);
        }

        protected override void AddCurrentItem()
        {
            throw new NotImplementedException();
        }

        protected override void DeleteCurrentItem()
        {
            throw new NotImplementedException();
        }

        public override void UpdateSectionData()
        {
            _sectionData = SupplierService.GetSuppliersProfits();
        }

        protected override string GetErrors()
        {
            throw new NotImplementedException();
        }

    }
}
