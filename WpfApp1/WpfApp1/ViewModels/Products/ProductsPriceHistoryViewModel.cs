using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WpfApp1.Views;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views.Products.PriceHistory;
using System.Windows.Controls;

namespace WpfApp1.ViewModels.Products
{
    internal class ProductsPriceHistoryViewModel : SectionWidgetViewModel
    {
        private ProductsPriceHistoryItem _itemForm;
        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as ProductsPriceHistoryItem;
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

        private ProductService _productService;
        public List<Models.Products> Products { get; set; }

        public ProductsPriceHistoryViewModel(SectionWidget sectionWidget) : base(sectionWidget) {
            _productService = App.ProductService;
            UpdateSectionData();
        }

        protected override void MakeCurrentItemEmpty()
        {
            _currentItem = new PriceHistory();
        }

        protected override void CreateNewItemForm()
        {
            Products = App.Context.Products.ToList();
            _itemForm = new ProductsPriceHistoryItem(this);
        }

        protected override void AddCurrentItem()
        {
            App.Context.PriceHistory.Add(CurrentItem);
        }

        protected override void DeleteCurrentItem()
        {
            App.Context.PriceHistory.Remove(CurrentItemFromContext);
        }

        public override void UpdateSectionData()
        {
            _sectionData = _productService.GetPriceHistory();
        }

        protected override string GetErrors()
        {
            StringBuilder errorBuilder = new StringBuilder();

            if (CurrentItem.Product == null)
            {
                errorBuilder.AppendLine("Свойство \"Товар\" обязательно для заполнения;");
            }
            if (Validation.GetHasError((ItemForm as ProductsPriceHistoryItem).tbPrice) || CurrentItem.Price <= 0)
            {
                errorBuilder.AppendLine("Поле \"Цена\" - положительное число;");
            }
            if (CurrentItem.PriceDate == null || CurrentItem.PriceDate < new DateTime(1900, 1, 1) || CurrentItem.PriceDate > new DateTime(3000, 12, 31))
            {
                errorBuilder.AppendLine("Свойство \"Дата изменения цены\" обязательно для заполнения, допустимые значения от 1900.01.01 до 3000.12.31;");
            }

            return errorBuilder.ToString();
        }

    }
}
