using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.ViewModels;
using WpfApp1.Views.Components;

namespace WpfApp1.Services
{
    /// <summary>
    /// Перечисление, описывающее режимы работы с окном изображения.
    /// Режимы работы: просмотр, просмотр с возможностью удаления изображения.
    /// </summary>
    public enum ImageWindowMode
    {
        Read,
        Delete
    }

    /// <summary>
    /// Класс, ответственный за создание окон работы с изображениями.
    /// Необходим для реализации возможности открытия одновременно нескольких окон работы с изображениями. 
    /// Но для каждого изображения может быть только одно окно.
    /// </summary>
    public class ImageWindowService
    {
        /// <summary>
        /// Словарь изображений и привязанных к ним окон.
        /// Используется для отслеживания существующих окон работы с изображениями.
        /// </summary>
        private static Dictionary<dynamic, ImageWindow> _imageForms = new Dictionary<dynamic, ImageWindow>();

        /// <summary>
        /// Метод, который создаёт окно работы с изображением, если для данного изображения ещё не существует окна.
        /// В качестве параметров принимает модель представления, режим работы с изображением.
        /// </summary>
        /// <param name="viewModel">Модель представления раздела.</param>
        /// <param name="imageFormMode">Режим работы с изображением.</param>
        /// <returns>Окно работы с изображением.</returns>
        /// <exception cref="Exception"></exception>
        public static ImageWindow TryCreateItemForm(SectionWidgetWithImagesViewModel viewModel, ImageWindowMode imageFormMode)
        {
            dynamic image = viewModel.CurrentImage;
            if (!IsExistImageWindow(image))
            {
                ImageWindow imageForm = new ImageWindow(viewModel, imageFormMode);
                imageForm.image.Source = ImageConverter.ByteArrayToImage(image.Image);
                _imageForms[image] = imageForm;
                return imageForm;
            }
            else
            {
                throw new Exception("Может быть открыто только одно окно для работы с изображением");
            }
        }

        /// <summary>
        /// Метод, проверяющий, существует ли окно для текущего изображения.
        /// </summary>
        /// <param name="image">Модель изображения.</param>
        /// <returns>Истина, если окно для текущего изображения существует.</returns>
        public static bool IsExistImageWindow(dynamic image)
        {
            return _imageForms.ContainsKey(image);
        }

        /// <summary>
        /// Метод, который пытается закрыть окно работы с изображением. Принимает в качестве параметра модель изображения.
        /// Если для такого изображения найдено окно, то оно закрывается.
        /// Параметр имеет тип dynamic в связи с тем, что метод не знает, какого типа данных будет модель изображения.
        /// </summary>
        /// <param name="image">Модель изображения.</param>
        public static void TryCloseImageWindow(dynamic image)
        {
            ImageWindow imageForm;
            if (_imageForms.TryGetValue(image, out imageForm))
            {
                imageForm.Close();
                RemoveImageWindow(image);
            }
        }

        /// <summary>
        /// Метод, который удаляет окно из словаря окон работы с изображениями.
        /// </summary>
        /// <param name="image">Модель изображения, чьё окно должно быть удалено из словаря.</param>
        public static void RemoveImageWindow(dynamic image)
        {
            _imageForms.Remove(image);
        }
    }
}
