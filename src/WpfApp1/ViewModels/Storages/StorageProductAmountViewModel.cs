using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfApp1.Views;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views.Storages.ProductAmount;
using Microsoft.EntityFrameworkCore;
using System.Windows.Controls;

namespace WpfApp1.ViewModels.Storages
{
    /// <summary>
    /// Модель представления для раздела "Склады / Товары на складах".
    /// </summary>
    internal class StorageProductAmountViewModel : SectionWidgetViewModel
    {
        /// <summary>
        /// Ссылка на окно работы с записью раздела. 
        /// </summary>
        private StorageProductAmountItem _itemForm;

        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as StorageProductAmountItem;
        }

        /// <summary>
        /// Коллекция товаров, используется для заполнения выпадающего списка в окне работы с записью раздела.
        /// </summary>
        public List<Models.Products> Products { get; set; }

        /// <summary>
        /// Коллекция складов, используется для заполнения выпадающего списка в окне работы с записью раздела.
        /// </summary>
        public List<Models.Storages> Storages { get; set; }

        /// <summary>
        /// Конструктор класса StorageProductAmountViewModel, в качестве параметра принимает ссылку на представление раздела.
        /// </summary>
        /// <param name="sectionWidget">Представление раздела.</param>
        public StorageProductAmountViewModel(SectionWidget sectionWidget) : base(sectionWidget) {}

        protected override void MakeCurrentItemEmpty()
        {
            CurrentItem = new ProductsOnStorages();
        }

        protected override void CreateNewItemForm()
        {
            Products = App.Context.Products.ToList();
            Storages = App.Context.Storages.Include(s => s.StorageTypeNavigation).ToList();
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
            SectionData = StorageService.GetStoragesProductAmount();
        }

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
            if (Validation.GetHasError((ItemForm as StorageProductAmountItem).tbAmount) || CurrentItem.ProductAmount <= 0)
            {
                errorBuilder.AppendLine("Поле \"Количество\" - положительное число;");
            }

            return errorBuilder.ToString();
        }
    }
}
