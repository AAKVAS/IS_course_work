using System.Collections.ObjectModel;
using System.Text;
using WpfApp1.Views;
using WpfApp1.Services;
using WpfApp1.Views.Suppliers.GeneralInfo;
using ValidationLib;

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

        public SuppliersGeneralInfoViewModel(SectionWidget sectionWidget) : base(sectionWidget) {
	        UpdateSectionData();
        }

        protected override void MakeCurrentItemEmpty()
        {
            CurrentItem = new Models.Suppliers();
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
            SectionData = SupplierService.GetSuppliersGeneralInfo();
        }

        protected override string GetErrors()
        {
            StringBuilder errorBuilder = new StringBuilder();

            if (!StringValidator.IsValid(CurrentItem.Title))
            {
                errorBuilder.AppendLine("Поле \"Имя\" обязательно для заполнения, максимальная длина - 255 символов;");
            }

            return errorBuilder.ToString();
        }

    }
}
