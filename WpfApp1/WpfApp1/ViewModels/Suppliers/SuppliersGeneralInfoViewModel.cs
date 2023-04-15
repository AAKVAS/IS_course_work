using System.Collections.ObjectModel;
using System.Text;
using WpfApp1.Views;
using WpfApp1.Services;
using WpfApp1.Views.Suppliers.GeneralInfo;

namespace WpfApp1.ViewModels.Suppliers
{
    internal class SuppliersGeneralInfoViewModel : SectionWidgetViewModel
    {
        private SuppliersGeneralInfoItem _itemForm;
        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as SuppliersGeneralInfoItem;
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


        private SupplierService _supplierService;

        public SuppliersGeneralInfoViewModel(SectionWidget sectionWidget) : base(sectionWidget) {
            _supplierService = App.SupplierService;
	        UpdateSectionData();
        }

        protected override void MakeCurrentItemEmpty()
        {
            _currentItem = new Models.Suppliers();
        }

        protected override void CreateNewItemForm()
        {
            _itemForm = new SuppliersGeneralInfoItem(this);
        }

        protected override void AddCurrentItem()
        {
            App.Context.Suppliers.Add(CurrentItem);
        }

        protected override void DeleteCurrentItem()
        {
            App.Context.Suppliers.Remove(CurrentItemFromContext);
        }

        public override void UpdateSectionData()
        {
            _sectionData = _supplierService.GetSuppliersGeneralInfo();
        }

        protected override string GetErrors()
        {
            StringBuilder errorBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(CurrentItem.Title))
            {
                errorBuilder.AppendLine("Поле \"Имя\" обязательно для заполнения;");
            }

            return errorBuilder.ToString();
        }

    }
}
