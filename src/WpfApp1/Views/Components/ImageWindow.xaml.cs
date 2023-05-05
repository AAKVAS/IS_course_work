using System;
using System.Windows;
using WpfApp1.Services;
using WpfApp1.ViewModels;

namespace WpfApp1.Views.Components
{
    /// <summary>
    /// Окно работы с изображением.
    /// </summary>
    public partial class ImageWindow : Window
    {
        /// <summary>
        /// Режим работы с изображением.
        /// </summary>
        private ImageWindowMode _imageFormMode;

        /// <summary>
        /// Модель текущего изображения в окне.
        /// </summary>
        public dynamic Image { get; set; }

        /// <summary>
        /// Модель представления раздела.
        /// </summary>
        public SectionWidgetWithImagesViewModel ViewModel { get; set; }

        /// <summary>
        /// Конструктор класса ImageWindow, принимающий в качестве параметров модель представления раздела и режим работы с изображением.
        /// </summary>
        /// <param name="viewModel">Модель представления раздела.</param>
        /// <param name="imageFormMode">Режим работы с изображением.</param>
        public ImageWindow(SectionWidgetWithImagesViewModel viewModel, ImageWindowMode imageFormMode)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = this;
            Image = ViewModel.CurrentImage;
            _imageFormMode = imageFormMode;
            DisableDeleteButtonIfNeeded();
            Owner = ViewModel.ItemForm;
        }

        /// <summary>
        /// Метод, скрывающий кнопку удаления, если режим работы с изображением - просмотр.
        /// </summary>
        private void DisableDeleteButtonIfNeeded()
        {
            if (_imageFormMode == ImageWindowMode.Read)
            {
                btnDelete.IsEnabled = false;
                btnDelete.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Обработчик нажатия на кнопку закрытия окна.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Обработчик собития Window_Closed. Во время закрытия окна, удаляет его из словаря окон работы с изображениями.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            ImageWindowService.RemoveImageWindow(Image);
        }

        /// <summary>
        /// Обработчик события нажатия на элемент "Копировать" контекстного меню.
        /// Копирует изображение в буфер обмена.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemCopy_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.CopyImage(image);
        }
    }
}
