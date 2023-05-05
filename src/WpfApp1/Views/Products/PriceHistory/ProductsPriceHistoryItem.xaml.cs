using System.Windows;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Products;

namespace WpfApp1.Views.Products.PriceHistory
{
    /// <summary>
    /// Окно работы с записью раздела "Товары / История цен".
    /// </summary>
    public partial class ProductsPriceHistoryItem: ItemForm
    {
        /// <summary>
        /// Ссылка на модель представления раздела.
        /// </summary>
        private ProductsPriceHistoryViewModel _viewModel;

        /// <summary>
        /// Конструктор класса ProductsPriceHistoryItem, принимающий в качестве параметра ссылку на модель представления раздела.
        /// </summary>
        /// <param name="sectionWidgetViewModel">Модель представления раздела.</param>
        public ProductsPriceHistoryItem(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            _viewModel = (ProductsPriceHistoryViewModel)_sectionWidgetViewModel;
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
            cbProduct.IsEnabled = false;
            tbPrice.IsReadOnly = true;
            calendarPriceDate.IsEnabled = false;
        }
    }
}
