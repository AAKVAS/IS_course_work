using System.Windows;
using System.Windows.Controls;
using WpfApp1.Services;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Orders;

namespace WpfApp1.Views.Orders.OrdersReadyToReceive
{
    /// <summary>
    /// Окно работы с записью раздела "Доставки / Доставки, готовые к получению".
    /// </summary>
    public partial class OrdersReadyToReceiveItemWithImages : ItemWithImages
    {
        /// <summary>
        /// Ссылка на модель представления раздела.
        /// </summary>
        private OrdersReadyToReceiveViewModel _viewModel;

        /// <summary>
        /// Конструктор класса OrdersReadyToReceiveItemWithImages, принимающий в качестве параметра ссылку на модель представления раздела.
        /// </summary>
        /// <param name="sectionWidgetViewModel">Модель представления раздела.</param>
        public OrdersReadyToReceiveItemWithImages(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            _viewModel = (OrdersReadyToReceiveViewModel)_sectionWidgetViewModel;
            DataContext = _viewModel;

            //Подтягиваем изображения к текущей записи и устанавливаем их в ListBox окна.
            _viewModel.LoadCurrentItemImages();
            lbImages.ItemsSource = _viewModel.CurrentItemFromContext.Order.Product.Images;
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
            cbStatus.IsEnabled = false;
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
    }
}

