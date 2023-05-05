using System.Windows;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Storages;

namespace WpfApp1.Views.Storages.ProductAmount
{
    /// <summary>
    /// Окно работы с записью раздела "Склады / Товары на складах".
    /// </summary>
    public partial class StorageProductAmountItem: ItemForm
    {
        /// <summary>
        /// Конструктор класса StorageProductAmountItem, принимающий в качестве параметра ссылку на модель представления раздела.
        /// </summary>
        /// <param name="sectionWidgetViewModel">Модель представления раздела.</param>
        public StorageProductAmountItem(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            DataContext = (StorageProductAmountViewModel)_sectionWidgetViewModel;
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
            tbAmount.IsReadOnly = true;
            cbProduct.IsEnabled = false;
            cbStorage.IsEnabled = false;
        }
    }
}
