using System.Windows;
using System.Windows.Controls;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Products;

namespace WpfApp1.Views.Products.Reviews 
{
    /// <summary>
    /// Окно работы с записью раздела "Товары / Отзывы к товару".
    /// </summary>
    public partial class ProductsReviewsItemWithImages : ItemWithImages
    {
        /// <summary>
        /// Ссылка на модель представления раздела.
        /// </summary>
        private ProductsReviewsViewModel _viewModel;

        /// <summary>
        /// Конструктор класса ProductsReviewsItemWithImages, принимающий в качестве параметра ссылку на модель представления раздела.
        /// </summary>
        /// <param name="sectionWidgetViewModel">Модель представления раздела.</param>
        public ProductsReviewsItemWithImages(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            _viewModel = (ProductsReviewsViewModel)_sectionWidgetViewModel;
            DataContext = _viewModel;

            //Подтягиваем изображения к текущей записи и устанавливаем их в ListBox окна.
            _viewModel.LoadCurrentItemImages();
            lbImages.ItemsSource = _viewModel.CurrentItemFromContext.Images;
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
            tbId.IsReadOnly = true;
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
            tbId.IsReadOnly = true;
            tbReview.IsReadOnly = true;
            tbStars.IsReadOnly = true;
            cbProduct.IsEnabled = false;
            cbUser.IsEnabled = false;
            btnAddImage.IsEnabled = false;
            btnAddImage.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Обработчик события PreviewMouseDoubleClick для изображения в списке изображений.
        /// Пытается открыть окно работы с изображением, если его ещё нет.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbImages_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _viewModel.TryShowImageWindow();
        }
    }
}

