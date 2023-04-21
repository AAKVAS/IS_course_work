using System.Windows;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Storages;

namespace WpfApp1.Views.Storages.GeneralInfo
{
    /// <summary>
    /// Окно работы с записью раздела "Склады / Общие сведениях".
    /// </summary>
    public partial class StoragesGeneralInfoItem: ItemForm
    {
        /// <summary>
        /// Ссылка на модель представления раздела.
        /// </summary>
        StoragesGeneralInfoViewModel _viewModel;

        /// <summary>
        /// Конструктор класса StoragesGeneralInfoItem, принимающий в качестве параметра ссылку на модель представления раздела.
        /// </summary>
        /// <param name="sectionWidgetViewModel">Модель представления раздела.</param>
        public StoragesGeneralInfoItem(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            _viewModel = (StoragesGeneralInfoViewModel)_sectionWidgetViewModel;
            DataContext = _viewModel;
        }

        /// <summary>
        /// Обработчик нажатия на кнопку закрытия окна.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            TryCloseForm();
        }

        protected override void SetFormModeToInsert()
        {
            btnDataAction.Visibility = Visibility.Visible;
            btnDataAction.Content = "Сохранить";
        }

        protected override void SetFormModeToUpdate()
        {
            btnDataAction.Visibility = Visibility.Visible;
            btnDataAction.Content = "Изменить";
        }

        protected override void SetFormModeToRead()
        {
            btnDataAction.Visibility = Visibility.Collapsed;
            DisableAllInputs();
        }

        /// <summary>
        /// Метод, делающий все поля окна раздела доступными только для просмотра.
        /// </summary>
        private void DisableAllInputs()
        {
            tbCountry.IsReadOnly = true;
            tbHouseNumber.IsReadOnly = true;
            tbStreet.IsReadOnly = true;
            tbFederalSubject.IsReadOnly = true;
            tbLocality.IsReadOnly = true;
            cbStorages.IsEnabled = false;
        }
    }
}
