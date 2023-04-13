using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WpfApp1.Views;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views.Storages.ProductAmount;
using Microsoft.EntityFrameworkCore;

namespace WpfApp1.ViewModels.Storages
{
    internal class StorageProductAmountViewModel : SectionWidgetViewModel
    {
        private StorageProductAmountItem _itemForm;
        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as StorageProductAmountItem;
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

        public StorageProductAmountViewModel(SectionWidget sectionWidget) : base(sectionWidget) {
            _storageService = App.StorageService;
            Products = App.Context.Products.ToList();
            Storages = App.Context.Storages.Include(s => s.StorageTypeNavigation).ToList();
	        UpdateSectionData();
        }

        protected override void MakeCurrentItemEmpty()
        {
            _currentItem = new ProductsOnStorages();
        }

        protected override void CreateNewItemForm()
        {
            _itemForm = new StorageProductAmountItem(this);
        }

        protected override void AddCurrentItem()
        {
            App.Context.ProductsOnStorages.Add(CurrentItem);
        }

        protected override void DeleteCurrentItem()
        {
            App.Context.ProductsOnStorages.Remove(CurrentItemFromContext);
        }

        public override void UpdateSectionData()
        {
            _sectionData = _storageService.GetStoragesProductAmount();
        }

        protected override void FillItem() {}

        protected override string GetErrors()
        {
            StringBuilder errorBuilder = new StringBuilder();
            
            if (CurrentItem.Storage == null)
            {
                errorBuilder.AppendLine("Свойство \"Склад\" обязательно для заполнения;");
            }
            if (CurrentItem.Product == null)
            {
                errorBuilder.AppendLine("Свойство \"Товар\" обязательно для заполнения;");
            }
            if (CurrentItem.ProductAmount <= 0)
            {
                errorBuilder.AppendLine("Поле \"Количество\" - положительное число;");
            }

            return errorBuilder.ToString();
        }

    }
}
