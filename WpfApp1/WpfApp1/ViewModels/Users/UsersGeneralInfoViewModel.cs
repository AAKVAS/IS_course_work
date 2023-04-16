using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WpfApp1.Views;
using WpfApp1.Views.Users.GeneralInfo;
using WpfApp1.Models;
using WpfApp1.Services;
using ValidationLib;

namespace WpfApp1.ViewModels.Users
{
    internal class UsersGeneralInfoViewModel : SectionWidgetViewModel
    {
        private UserGeneralInfoItem _itemForm;
        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as UserGeneralInfoItem;
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

        private UserService _userService;

        public List<Countries> Countries { get; set; }

        public UsersGeneralInfoViewModel(SectionWidget sectionWidget) : base(sectionWidget) {
            _userService = App.UserService;
            UpdateSectionData();
        }

        protected override void MakeCurrentItemEmpty()
        {
            _currentItem = new Models.Users();
        }

        protected override void CreateNewItemForm()
        {
            Countries = App.Context.Countries.ToList();
            _itemForm = new UserGeneralInfoItem(this);
        }

        protected override void AddCurrentItem()
        {
            App.Context.Users.Add(CurrentItem);
        }

        protected override void DeleteCurrentItem()
        {
            App.Context.Users.Remove(CurrentItemFromContext);
        }

        public override void UpdateSectionData()
        {
            _sectionData = _userService.GetUserGeneralInfo();
        }

        protected override void FillItem()
        {
            CurrentItem.IsMale = _itemForm.rbMale.IsChecked ?? false;
        }

        protected override string GetErrors()
        {
            StringBuilder errorBuilder = new StringBuilder();

            if (!StringValidator.IsValid(CurrentItem.Lastname))
            {
                errorBuilder.AppendLine("Поле \"Фамилия\" обязательно для заполнения, максимальная длина - 255 символов;");
            }
            if (!StringValidator.IsValid(CurrentItem.Firstname))
            {
                errorBuilder.AppendLine("Поле \"Имя\" обязательно для заполнения, максимальная длина - 255 символов;");
            }
            if (!StringValidator.IsValid(CurrentItem.Patronymic))
            {
                errorBuilder.AppendLine("Поле \"Отчество\" обязательно для заполнения, максимальная длина - 255 символов;");
            }
            if (!PhoneNumberValidator.IsValid(CurrentItem.PhoneNumber))
            {
                errorBuilder.AppendLine("Неверное значение поля \"Номер телефона\";");
            }
            if (!EmailValidatior.IsValid(CurrentItem.Email))
            {
                errorBuilder.AppendLine("Неверное значение поля \"Email\";");
            }
            if (CurrentItem.Country == null)
            {
                errorBuilder.AppendLine("Свойство \"Страна\" обязательно для заполнения;");
            }
            if (CurrentItem.Birthday == null || CurrentItem.Birthday < new DateTime(1900, 1, 1) || CurrentItem.Birthday > new DateTime(3000, 12, 31))
            {
                errorBuilder.AppendLine("Свойство \"Дата рождения\" обязательно для заполнения, допустимые значения от 1900.01.01 до 3000.12.31;");
            }

            return errorBuilder.ToString();
        }

    }
}
