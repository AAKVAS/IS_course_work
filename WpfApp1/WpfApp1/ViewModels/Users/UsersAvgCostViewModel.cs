using System.Collections.ObjectModel;
using System.Text;
using WpfApp1.Views;
using WpfApp1.Views.Users.GeneralInfo;
using WpfApp1.Services;
using System;

namespace WpfApp1.ViewModels.Users
{
    internal class UsersAvgCostViewModel : SectionWidgetViewModel
    {
        private UserAvgCostItem _itemForm;
        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as UserAvgCostItem;
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

        public UsersAvgCostViewModel(SectionWidget sectionWidget) : base(sectionWidget) {
            UpdateSectionData();
        }

        protected override void MakeCurrentItemEmpty()
        {
            _currentItem = new Models.DTO.UserAverageCostDTO();
        }

        protected override void CreateNewItemForm()
        {
            _itemForm = new UserAvgCostItem(this);
        }

        protected override void AddCurrentItem()
        {
            throw new Exception("Добавление данных в этом разделе не предусмотрено");
        }

        protected override void DeleteCurrentItem()
        {
            throw new Exception("Удаление данных в этом разделе не предусмотрено");
        }

        public override void UpdateSectionData()
        {
            _sectionData = UserService.GetUserAvgCost();
        }

        protected override string GetErrors()
        {
            return new StringBuilder().ToString();
        }
    }
}
