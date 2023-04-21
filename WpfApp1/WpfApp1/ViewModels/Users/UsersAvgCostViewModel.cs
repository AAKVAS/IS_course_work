using WpfApp1.Views;
using WpfApp1.Views.Users.GeneralInfo;
using WpfApp1.Services;
using System;

namespace WpfApp1.ViewModels.Users
{
    /// <summary>
    /// Модель представления для раздела "Пользователи / Средние затраты пользователей".
    /// </summary>
    internal class UsersAvgCostViewModel : SectionWidgetViewModel
    {
        /// <summary>
        /// Ссылка на окно работы с записью раздела. 
        /// </summary>
        private UserAvgCostItem _itemForm;

        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as UserAvgCostItem;
        }

        /// <summary>
        /// Конструктор класса UsersAvgCostViewModel, в качестве параметра принимает ссылку на представление раздела.
        /// </summary>
        /// <param name="sectionWidget">Представление раздела.</param>
        public UsersAvgCostViewModel(SectionWidget sectionWidget) : base(sectionWidget) {}

        protected override void MakeCurrentItemEmpty()
        {
            CurrentItem = new Models.DTO.UserAverageCostDTO();
        }

        protected override void CreateNewItemForm()
        {
            _itemForm = new UserAvgCostItem(this);
        }

        protected override void AddCurrentItem()
        {
            //Добавление новых записей невозможно.
            throw new Exception("Добавление данных в этом разделе не предусмотрено");
        }

        protected override void DeleteCurrentItem()
        {
            //Удаление записей невозможно.
            throw new Exception("Удаление данных в этом разделе не предусмотрено");
        }

        public override void UpdateSectionData()
        {
            SectionData = UserService.GetUserAvgCost();
        }

        protected override string GetErrors()
        {
            //Ошибок в записи быть не может.
            throw new NotImplementedException();
        }
    }
}
