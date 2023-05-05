using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfApp1.Views;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views.Storages.WorkerShifts;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace WpfApp1.ViewModels.Storages
{
    /// <summary>
    /// Модель представления для раздела "Склады / Работа сотрудников на складе".
    /// </summary>
    internal class StoragesWorkerShiftsViewModel : SectionWidgetViewModel
    {
        /// <summary>
        /// Ссылка на окно работы с записью раздела. 
        /// </summary>
        private StoragesWorkerShiftsItem _itemForm;

        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as StoragesWorkerShiftsItem;
        }

        /// <summary>
        /// Коллекция складов, используется для заполнения выпадающего списка в окне работы с записью раздела.
        /// </summary>
        public List<Models.Storages> Storages { get; set; }

        /// <summary>
        /// Коллекция сотрудников, используется для заполнения выпадающего списка в окне работы с записью раздела.
        /// </summary>
        public List<Models.Workers> Workers { get; set; }

        /// <summary>
        /// Конструктор класса StoragesWorkerShiftsViewModel, в качестве параметра принимает ссылку на представление раздела.
        /// </summary>
        /// <param name="sectionWidget">Представление раздела.</param>
        public StoragesWorkerShiftsViewModel(SectionWidget sectionWidget) : base(sectionWidget) {}

        protected override void MakeCurrentItemEmpty()
        {
            CurrentItem = new StorageWorkerShifts();
        }

        protected override void CreateNewItemForm()
        {
            Storages = App.Context.Storages.Include(s => s.StorageTypeNavigation).ToList();
            Workers = App.Context.Workers.Include(w => w.Post).ToList();
            _itemForm = new StoragesWorkerShiftsItem(this);
        }

        protected override void AddCurrentItem()
        {
            App.Context.StorageWorkerShifts.Add(CurrentItem);
        }

        protected override void DeleteCurrentItem()
        {
            App.Context.StorageWorkerShifts.Remove(CurrentItemFromContext);
        }

        public override void UpdateSectionData()
        {
            SectionData = StorageService.GetStorageWorkerShifts();
        }

        protected override string GetErrors()
        {
            StringBuilder errorBuilder = new StringBuilder();

            if (CurrentItem.Storage == null)
            {
                errorBuilder.AppendLine("Свойство \"Склад\" обязательно для заполнения;");
            }
            if (CurrentItem.Worker == null)
            {
                errorBuilder.AppendLine("Свойство \"Сотрудник\" обязательно для заполнения;");
            }
            if (CurrentItem.StartedShiftAt == null || CurrentItem.StartedShiftAt < new DateTime(1900, 1, 1) || CurrentItem.StartedShiftAt > new DateTime(3000, 12, 31))
            {
                errorBuilder.AppendLine("Свойство \"Начало смены\" обязательно для заполнения, допустимые значения от 1900.01.01 до 3000.12.31;");
            }
            if (CurrentItem.FinishedShiftAt == null || CurrentItem.FinishedShiftAt < new DateTime(1900, 1, 1) || CurrentItem.FinishedShiftAt > new DateTime(3000, 12, 31))
            {
                errorBuilder.AppendLine("Свойство \"Окончание смены\" обязательно для заполнения, допустимые значения от 1900.01.01 до 3000.12.31;");
            }
            if (CurrentItem.StartedShiftAt != null && CurrentItem.FinishedShiftAt != null && CurrentItem.StartedShiftAt >= CurrentItem.FinishedShiftAt)
            {
                errorBuilder.AppendLine("Значние свойства \"Начало смены\" должно быть меньше свойства\"Окончание смены\";");
            }

            return errorBuilder.ToString();
        }

        protected override void Insert()
        {
            var entry = App.Context.Entry(CurrentItem);
            try
            {
                StorageService.InsertStorageWorkerShift(CurrentItem);
                ItemForm.Close();
                UpdateItems();
            }
            catch (Exception ex)
            {
                entry.Reload();
                MessageBox.Show("Добавление записи завершилось ошибкой");
                ItemForm.Close();
            }
        }
    }
}
