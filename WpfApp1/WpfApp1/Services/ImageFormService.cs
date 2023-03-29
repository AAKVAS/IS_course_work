using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.ViewModels;
using WpfApp1.Views.Components;

namespace WpfApp1.Services
{
    public enum ImageFormMode
    {
        Read,
        Delete
    }

    public class ImageFormService
    {
        private static Dictionary<dynamic, ImageForm> _imageForms = new Dictionary<dynamic, ImageForm>();

        public static ImageForm TryCreateItemForm(SectionWidgetWithImagesViewModel viewModel, ImageFormMode imageFormMode)
        {
            dynamic image = viewModel.CurrentImage;
            if (!IsExistImageForm(image))
            {
                ImageForm imageForm = new ImageForm(viewModel, imageFormMode);
                imageForm.image.Source = ImageConverter.ByteArrayToImage(image.Image);
                _imageForms[image] = imageForm;
                return imageForm;
            }
            else
            {
                throw new Exception("Может быть открыто только одно окно для работы с изображением");
            }
        }

        public static bool IsExistImageForm(dynamic image)
        {
            return _imageForms.ContainsKey(image);
        }

        public static void TryCloseImageForm(dynamic image)
        {
            ImageForm imageForm;
            if (_imageForms.TryGetValue(image, out imageForm))
            {
                imageForm.Close();
                _imageForms.Remove(image);
            }
        }

        public static void RemoveImageForm(dynamic image)
        {
            ImageForm imageForm;
            if (_imageForms.TryGetValue(image, out imageForm))
            {
                _imageForms.Remove(image);
            }
        }
    }
}
