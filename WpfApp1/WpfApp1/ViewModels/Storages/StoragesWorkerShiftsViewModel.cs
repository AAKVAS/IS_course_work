using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    internal class StoragesWorkerShiftsViewModel : SectionWidgetViewModel
    {
        private StoragesWorkerShiftsItem _itemForm;
        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as StoragesWorkerShiftsItem;
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
        public List<Models.Storages> Storages { get; set; }
        public List<Models.Workers> Workers { get; set; }

        public StoragesWorkerShiftsViewModel(SectionWidget sectionWidget) : base(sectionWidget) {
            _storageService = App.StorageService;
            Storages = App.Context.Storages.Include(s => s.StorageTypeNavigation).ToList();
            Workers = App.Context.Workers.Include(w => w.Post).ToList();
	        UpdateSectionData();
        }

        protected override void MakeCurrentItemEmpty()
        {
            _currentItem = new Models.StorageWorkerShifts();
        }

        protected override void CreateNewItemForm()
        {
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
            _sectionData = _storageService.GetStorageWorkerShifts();
        }

        protected override void FillItem() {}

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
            if (CurrentItem.StartedShiftAt >= CurrentItem.FinishedShiftAt)
            {
                errorBuilder.AppendLine("Значние свойства \"Начало смены\" должно быть меньше свойства\"Окончание смены\"");
            }

            return errorBuilder.ToString();
        }

        protected override void Insert()
        {
            var entry = App.Context.Entry(CurrentItem);
            try
            {
                _storageService.InsertStorageWorkerShift(CurrentItem);
                ItemForm.Close();
                UpdateItems();
            }
            catch (Exception ex)
            {
                entry.Reload();
                MessageBox.Show("Вы не можете вставить запись, так как её ключ уже используется");
                ItemForm.Close();
            }

        }

    }
}
