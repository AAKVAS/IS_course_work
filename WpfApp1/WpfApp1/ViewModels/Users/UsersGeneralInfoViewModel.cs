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
            set => _sectionData = value;
        }

        private dynamic _currentItem;
        public override dynamic? CurrentItem
        {
            get => _currentItem;
            set => _currentItem = value;
        }


        private UserService _userService;

        public List<Countries> Countries;

        public UsersGeneralInfoViewModel(SectionWidget sectionWidget) : base(sectionWidget) {
            _userService = App.UserService;
            _sectionData = _userService.GetUserGeneralInfo();
            Countries = App.Context.Countries.ToList();
        }

        protected override void MakeCurrentItemEmpty()
        {
            _currentItem = new Models.Users();
        }

        protected override void CreateNewItemForm()
        {
            _itemForm = new UserGeneralInfoItem(this);
        }

        protected override void AddCurrentItem()
        {
            App.Context.Users.Add(CurrentItem);
        }

        public override void UpdateSectionData()
        {
            _sectionData = _userService.GetUserGeneralInfo();
        }

        protected override void FillItem()
        {
            CurrentItem.IsMale = _itemForm.rbMale.IsChecked ?? false;
        }

    }
}
