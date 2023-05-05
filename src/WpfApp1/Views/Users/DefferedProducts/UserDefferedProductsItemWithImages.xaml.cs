using System.Windows;
using System.Windows.Controls;
using WpfApp1.Services;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Users;

namespace WpfApp1.Views.Users.DefferedProducts
{
    /// <summary>
    /// Окно работы с записью раздела "Пользователи / Список товаров в корзине".
    /// </summary>
    public partial class UserDefferedProductsItemWithImages : ItemWithImages
    {
        /// <summary>
        /// Ссылка на модель представления раздела.
        /// </summary>
        private UserDefferedProductsViewModel _viewModel;

        /// <summary>
        /// Конструктор класса UserDefferedProductsItemWithImages, принимающий в качестве параметра ссылку на модель представления раздела.
        /// </summary>
        /// <param name="sectionWidgetViewModel">Модель представления раздела.</param>
        public UserDefferedProductsItemWithImages(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            _viewModel = (UserDefferedProductsViewModel)_sectionWidgetViewModel;
            DataContext = _viewModel;
            _viewModel.LoadCurrentItemImages();
            if (_viewModel.CurrentItem.Product != null)
            {
                lbImages.ItemsSource = _viewModel.CurrentItem.Product.Images;
            }
        }

        public override ListBox ImagesListBox
        { 
            get => lbImages;
            set => lbImages = value; 
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
            cbUser.IsEnabled = false;
            cbProduct.IsEnabled = false;
        }

        /// <summary>
        /// Обработчик события PreviewMouseDoubleClick для изображения в списке изображений.
        /// Пытается открыть окно работы с изображением, если его ещё нет.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbImages_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _viewModel.TryShowImageWindow(ImageWindowMode.Read);
        }

        /// <summary>
        /// Обработчик события SelectionChanged для выпадающего списка товаров.
        /// При изменении товара список изображений обновляется.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.LoadDefferedProductImages();
            if (_viewModel.CurrentItem.Product != null)
            {
                lbImages.ItemsSource = _viewModel.CurrentItem.Product.Images;
            }
        }
    }
}