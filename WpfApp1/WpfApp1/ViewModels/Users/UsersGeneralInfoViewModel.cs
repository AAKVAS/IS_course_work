using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Views;
using WpfApp1.Views.Users.GeneralInfo;
using WpfApp1.Models;
using WpfApp1.Services;
using System.Windows;

namespace WpfApp1.ViewModels.Users.GeneralInfo
{
    internal class UsersGeneralInfoViewModel : SectionWidgetViewModel
    {
        private UserGeneralInfoItem _itemForm;
        protected override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as UserGeneralInfoItem;
        }

        private ObservableCollection<dynamic> _sectionData;
        public override ObservableCollection<dynamic> SectionData
        {
            get => _sectionData;
        }

        private dynamic _currentItem;
        public override dynamic? CurrentItem
        {
            get => _currentItem;
            set => _currentItem = value;
        }

        protected override void CreateNewItemForm()
        {
            _itemForm = new UserGeneralInfoItem(this);
        }

        private Dictionary<string, string> _sectionTableHeaders = new Dictionary<string, string>()
        {
            { "Firstname", "Имя" },
            { "Lastname", "Фамилия" },
            { "Patronymic", "Отчество" },
            { "PhoneNumber", "Номер телефона" },
            { "Birthday", "Дата рождения" },
            { "Title", "Страна" },
            { "Sex", "Пол" }

        };

        protected override Dictionary<string, string> SectionTableHeaders {
            get => _sectionTableHeaders;
        }


        private UserService _userService;

        public UsersGeneralInfoViewModel(SectionWidget sectionWidget) : base(sectionWidget) {
            _userService = (UserService)Application.Current.Resources["UserService"];
            _sectionData = _userService.GetUserGeneralInfo();
        }

    }
}
