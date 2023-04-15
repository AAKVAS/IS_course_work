using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Services;
using WpfApp1.ViewModels;

namespace WpfApp1.Views.Components
{
    /// <summary>
    /// Логика взаимодействия для ImageForm.xaml
    /// </summary>
    public partial class ImageForm : Window
    {
        private dynamic _image;
        private ImageFormMode _imageFormMode;
        private SectionWidgetWithImagesViewModel _viewModel;

        public ImageForm(SectionWidgetWithImagesViewModel viewModel, ImageFormMode imageFormMode)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            _image = _viewModel.CurrentImage;
            image.Source = ImageConverter.ByteArrayToImage(_image.Image);
            _imageFormMode = imageFormMode;
            DisableDeleteButtonIfNeeded();
            Owner = _viewModel.ItemForm;
        }

        private void DisableDeleteButtonIfNeeded()
        {
            if (_imageFormMode == ImageFormMode.Read)
            {
                btnDelete.IsEnabled = false;
                btnDelete.Visibility = Visibility.Collapsed;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            ImageFormService.TryCloseImageForm(_image);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ImageFormService.RemoveImageForm(_image);
        }

        private void miCopy_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.CopyImage(image);
        }
    }
}
