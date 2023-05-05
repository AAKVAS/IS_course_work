using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfApp1.Views;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views.Storages.GeneralInfo;
using ValidationLib;

namespace WpfApp1.ViewModels.Storages
{
    /// <summary>
    /// Модель представления для раздела "Склады / Общие сведения".
    /// </summary>
    internal class StoragesGeneralInfoViewModel : SectionWidgetViewModel
    {
        /// <summary>
        /// Ссылка на окно работы с записью раздела. 
        /// </summary>
        private StoragesGeneralInfoItem _itemForm;

        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as StoragesGeneralInfoItem;
        }

        /// <summary>
        /// Коллекция видов складов, используется для заполнения выпадающего списка в окне работы с записью раздела.
        /// </summary>
        public List<StorageTypes> StorageTypes { get; set; }

        /// <summary>
        /// Конструктор класса StoragesGeneralInfoViewModel, в качестве параметра принимает ссылку на представление раздела.
        /// </summary>
        /// <param name="sectionWidget">Представление раздела.</param>
        public StoragesGeneralInfoViewModel(SectionWidget sectionWidget) : base(sectionWidget) {}

        protected override void MakeCurrentItemEmpty()
        {
            CurrentItem = new Models.Storages();
        }

        protected override void CreateNewItemForm()
        {
            StorageTypes = App.Context.StorageTypes.ToList();
            _itemForm = new StoragesGeneralInfoItem(this);
        }

        protected override void AddCurrentItem()
        {
            App.Context.Storages.Add(CurrentItem);
        }

        protected override void DeleteCurrentItem()
        {
            App.Context.Storages.Remove(CurrentItemFromContext);
        }

        public override void UpdateSectionData()
        {
            SectionData = StorageService.GetStoragesGeneralInfo();
        }

        protected override string GetErrors()
        {
            StringBuilder errorBuilder = new StringBuilder();

            if (!StringValidator.IsValid(CurrentItem.Country))
            {
                errorBuilder.AppendLine("Поле \"Страна\" обязательно для заполнения, максимальная длина - 255 символов;");
            }
            if (!StringValidator.IsValid(CurrentItem.FederalSubject))
            {
                errorBuilder.AppendLine("Поле \"Субъект\" обязательно для заполнения, максимальная длина - 255 символов;");
            }
            if (!StringValidator.IsValid(CurrentItem.Locality))
            {
                errorBuilder.AppendLine("Поле \"Город\" обязательно для заполнения, максимальная длина - 255 символов;");
            }
            if (!StringValidator.IsValid(CurrentItem.Street))
            {
                errorBuilder.AppendLine("Поле \"Улица\" обязательно для заполнения, максимальная длина - 255 символов;");
            }
            if (!StringValidator.IsValid(CurrentItem.HouseNumber))
            {
                errorBuilder.AppendLine("Поле \"Номер дома\" обязательно для заполнения, максимальная длина - 255 символов;");
            }
            if (CurrentItem.StorageTypeNavigation == null)
            {
                errorBuilder.AppendLine("Свойство \"Тип склада\" обязательно для заполнения;");
            }    

            return errorBuilder.ToString();
        }
    }
}
