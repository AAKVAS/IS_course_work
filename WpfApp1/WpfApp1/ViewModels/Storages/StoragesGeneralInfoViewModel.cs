using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WpfApp1.Views;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views.Storages.GeneralInfo;
using ValidationLib;

namespace WpfApp1.ViewModels.Storages
{
    internal class StoragesGeneralInfoViewModel : SectionWidgetViewModel
    {
        private StoragesGeneralInfoItem _itemForm;
        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as StoragesGeneralInfoItem;
        }

        private ObservableCollection<dynamic> _sectionData;
        public override ObservableCollection<dynamic> SectionData
        {
            get => _sectionData;
            set => _sectionData = value;
        }

        public List<StorageTypes> StorageTypes { get; set; }

        public StoragesGeneralInfoViewModel(SectionWidget sectionWidget) : base(sectionWidget)
        {
            StorageTypes = App.Context.StorageTypes.ToList();
            UpdateSectionData();
        }

        protected override void MakeCurrentItemEmpty()
        {
            CurrentItem = new Models.Storages();
        }

        protected override void CreateNewItemForm()
        {
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
            _sectionData = StorageService.GetStoragesGeneralInfo();
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
