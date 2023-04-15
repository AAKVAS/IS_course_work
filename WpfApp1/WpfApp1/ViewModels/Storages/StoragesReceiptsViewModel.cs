using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WpfApp1.Views;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views.Storages.Receipts;
using System.Windows.Controls;

namespace WpfApp1.ViewModels.Storages
{
    internal class StoragesReceiptsViewModel : SectionWidgetViewModel
    {
        private StoragesReceiptsItem _itemForm;
        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as StoragesReceiptsItem;
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

        private StorageService _storageService;
        public List<Models.Products> Products { get; set; }
        public List<Models.Storages> Storages { get; set; }

        public StoragesReceiptsViewModel(SectionWidget sectionWidget) : base(sectionWidget) {
            _storageService = App.StorageService;
	        UpdateSectionData();
        }

        protected override void MakeCurrentItemEmpty()
        {
            _currentItem = new ReceiptOfProductsToStorages();
        }

        protected override void CreateNewItemForm()
        {
            Products = App.Context.Products.ToList();
            Storages = App.Context.Storages.ToList();
            _itemForm = new StoragesReceiptsItem(this);
        }

        protected override void AddCurrentItem()
        {
            App.Context.ReceiptOfProductsToStorages.Add(CurrentItem);
        }

        protected override void DeleteCurrentItem()
        {
            App.Context.ReceiptOfProductsToStorages.Remove(CurrentItemFromContext);
        }

        public override void UpdateSectionData()
        {
            _sectionData = _storageService.GetStorageReceipts();
        }

        protected override void FillItem() {}

        protected override string GetErrors()
        {
            StringBuilder errorBuilder = new StringBuilder();

            if (CurrentItem.Storage == null)
            {
                errorBuilder.AppendLine("Поле \"Склад\" обязательно для заполнения;");
            }
            if (CurrentItem.Product == null)
            {
                errorBuilder.AppendLine("Свойство \"Товар\" обязательно для заполнения;");
            }
            if (Validation.GetHasError((ItemForm as StoragesReceiptsItem).tbAmount) || CurrentItem.Amount <= 0)
            {
                errorBuilder.AppendLine("Поле \"Количество\" - положительное число;");
            }
            if (CurrentItem.ReceivedAt == null || CurrentItem.ReceivedAt < new DateTime(1900, 1, 1) || CurrentItem.ReceivedAt > new DateTime(3000, 12, 31))
            {
                errorBuilder.AppendLine("Свойство \"Дата поступления\" обязательно для заполнения, допустимые значения от 1900.01.01 до 3000.12.31;");
            }
            
            return errorBuilder.ToString();
        }

    }
}
