using System;
using WpfApp1.Views;
using WpfApp1.Services;
using WpfApp1.Views.Suppliers.Profit;

namespace WpfApp1.ViewModels.Suppliers
{
    /// <summary>
    /// Модель представления для раздела "Поставщики / Прибыль".
    /// </summary>
    internal class SuppliersProfitViewModel : SectionWidgetViewModel
    {
        /// <summary>
        /// Ссылка на окно работы с записью раздела. 
        /// </summary>
        private SuppliersProfitItem _itemForm;

        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as SuppliersProfitItem;
        }

        /// <summary>
        /// Конструктор класса SuppliersProfitViewModel, в качестве параметра принимает ссылку на представление раздела.
        /// </summary>
        /// <param name="sectionWidget">Представление раздела.</param>
        public SuppliersProfitViewModel(SectionWidget sectionWidget) : base(sectionWidget) {}

        protected override void MakeCurrentItemEmpty()
        {
            CurrentItem = new Models.DTO.SuppliersProfitDTO();
        }

        protected override void CreateNewItemForm()
        {
            _itemForm = new SuppliersProfitItem(this);
        }

        protected override void AddCurrentItem()
        {
            //Добавление новых записей невозможно.
            throw new Exception("Добавление данных в этом разделе не предусмотрено");
        }

        protected override void DeleteCurrentItem()
        {
            //Удаление записей невозможно.
            throw new Exception("Удаление данных в этом разделе не предусмотрено");
        }

        public override void UpdateSectionData()
        {
            SectionData = SupplierService.GetSuppliersProfits();
        }

        protected override string GetErrors()
        {
            //Ошибок в записи быть не может.
            throw new NotImplementedException();
        }
    }
}
