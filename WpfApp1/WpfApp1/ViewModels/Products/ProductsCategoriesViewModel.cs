using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WpfApp1.Views;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views.Products.Categories;
using ValidationLib;

namespace WpfApp1.ViewModels.Products
{
    internal class ProductsCategoriesViewModel : SectionWidgetViewModel
    {
        private ProductsCategoriesItem _itemForm;
        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as ProductsCategoriesItem;
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
        public ObservableCollection<Categories> Categories { get; set; }

        public ProductsCategoriesViewModel(SectionWidget sectionWidget) : base(sectionWidget) {
            _productService = App.ProductService;
            UpdateSectionData();
        }

        protected override void MakeCurrentItemEmpty()
        {
            _currentItem = new Categories();
        }

        protected override void CreateNewItemForm()
        {
            Categories = new ObservableCollection<Categories>(_productService.GetCategories());
            _itemForm = new ProductsCategoriesItem(this);
        }

        protected override void AddCurrentItem()
        {
            App.Context.Categories.Add(CurrentItem);
        }

	    protected override void DeleteCurrentItem()
        {
            App.Context.Categories.Remove(CurrentItemFromContext);
        }

        public override void UpdateSectionData()
        {
            _sectionData = new ObservableCollection<dynamic>(_productService.GetCategories());
        }

        protected override string GetErrors()
        {
            StringBuilder errorBuilder = new StringBuilder();

            if (!StringValidator.IsValid(CurrentItem.Title))
            {
                errorBuilder.AppendLine("Поле \"Название\" обязательно для заполнения, максимальная длина - 255 символов;");
            }
            if (CurrentItem.ParentCategory != null && CurrentItem.ParentCategory.Id == CurrentItem.Id)
            {
                errorBuilder.AppendLine("Свойство \"Родительская категория\" не может быть текущей категорией;");
            }
            return errorBuilder.ToString();
        }

    }
}
