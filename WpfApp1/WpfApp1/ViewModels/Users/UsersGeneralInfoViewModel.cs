using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Views;
using WpfApp1.Views.Users.GeneralInfo;

namespace WpfApp1.ViewModels.Users.GeneralInfo
{
    internal class UsersGeneralInfoViewModel : SectionWidgetViewModel
    {
        private UserGeneralInfoItem _itemForm;
        protected override ItemForm ItemForm
        {
            get => (_itemForm ?? new UserGeneralInfoItem()) as object as ItemForm;
        }

        public UsersGeneralInfoViewModel(SectionWidget sectionWidget) : base(sectionWidget) { }

    }
}
