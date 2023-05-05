using System.Collections.ObjectModel;
using System.Text;
using WpfApp1.Views;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views.Products.Categories;
using ValidationLib;

namespace WpfApp1.ViewModels.Products
{
    /// <summary>
    /// Модель представления для раздела "Товары / Категории товаров".
    /// </summary>
    internal class ProductsCategoriesViewModel : SectionWidgetViewModel
    {
        /// <summary>
        /// Ссылка на окно работы с записью раздела. 
        /// </summary>
        private ProductsCategoriesItem _itemForm;

        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as ProductsCategoriesItem;
        }

        /// <summary>
        /// Коллекция категорий товаров, используется для заполнения выпадающего списка в окне работы с записью раздела.
        /// </summary>
        public ObservableCollection<Categories> Categories { get; set; }

        /// <summary>
        /// Конструктор класса ProductsCategoriesViewModel, в качестве параметра принимает ссылку на представление раздела.
        /// </summary>
        /// <param name="sectionWidget">Представление раздела.</param>
        public ProductsCategoriesViewModel(SectionWidget sectionWidget) : base(sectionWidget) {}

        protected override void MakeCurrentItemEmpty()
        {
            CurrentItem = new Categories();
        }

        protected override void CreateNewItemForm()
        {
            Categories = new ObservableCollection<Categories>(ProductService.GetCategories());
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
            SectionData = new ObservableCollection<dynamic>(ProductService.GetCategories());
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
