using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WpfApp1.Services;
using WpfApp1.Views;
using WpfApp1.Views.Components;

namespace WpfApp1.ViewModels
{
    /// <summary>
    /// Абстрактный класс, являющийся моделью представления для раздела, в котором есть работа с изображениями.
    /// </summary>
    public abstract class SectionWidgetWithImagesViewModel : SectionWidgetViewModel
    {
        /// <summary>
        /// Команда добавления изображения к записи.
        /// </summary>
        protected RelayCommand? _insertImageCommand;
        
        /// <summary>
        /// Команда удаления изображения из записи.
        /// </summary>
        protected RelayCommand? _deleteImageCommand;

        /// <summary>
        /// Команда добавления изображения к записи.
        /// </summary>
        public RelayCommand InsertImageCommand
        {
            get
            {
                return _insertImageCommand ??
                        (_insertImageCommand = new RelayCommand((object obj) => {
                            InsertImage();
                        },
                        (obj) => _accessService.HasWorkerRightToInsert(SectionWidget.Section.SectionKey)));
            }
        }

        /// <summary>
        /// Команда удаления изображения из записи.
        /// </summary>
        public RelayCommand DeleteImageCommand
        {
            get
            {
                return _deleteImageCommand ??
                        (_deleteImageCommand = new RelayCommand((object obj) => {
                            DeleteImage(obj);
                        },
                        (obj) => _accessService.HasWorkerRightToDelete(SectionWidget.Section.SectionKey)));
            }
        }

        /// <summary>
        /// Модель текущего изображения, с которым идёт работа. 
        /// Имеет тип данных dynamic, так как абстрактный класс модели представления не знает, какого типа данных будет модель изорбражения.
        /// </summary>
        public dynamic CurrentImage { get; set; }

        /// <summary>
        /// Коллекция изображений текущей записи.
        /// Имеет тип данных ObservableCollection<dynamic>, потому что абстрактный класс модели представления раздела с изображениям не знает, какого типа данных будут изображения записи в разделе.
        /// </summary>
        public ObservableCollection<dynamic> CurrentItemImages { get; set; }

        /// <summary>
        /// Конструктор для класса SectionWidgetWithImagesViewModel, принимающий в качестве параметра ссылку на представление раздела.
        /// Вызывает конструктор родительского класса SectionWidgetViewModel.
        /// </summary>
        /// <param name="sectionWidget">Представление раздела.</param>
        protected SectionWidgetWithImagesViewModel(SectionWidget sectionWidget) : base(sectionWidget) {}

        /// <summary>
        /// Метод, добавляющий изображение к текущей записи раздела.
        /// Если у записи уже есть 10 изображений, либо выбранное изображение весит не менее 10 МБ, то добавление изображения отменяется.
        /// </summary>
        private void InsertImage()
        {
            if (CurrentItem.Images.Count == 10)
            {
                MessageBox.Show("Нельзя прикрепить больше 10 файлов!", "Предупреждение");
            }
            else
            {
                byte[] image;
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "Image | *.png; *.jpg; *.jpeg; *.gif";
                if (fileDialog.ShowDialog() == true)
                {
                    if (new FileInfo(fileDialog.FileName).Length >= 10485760)
                    {
                        MessageBox.Show("Изображение должно быть не больше 10 МБ!", "Предупреждение");
                    }
                    else
                    {
                        image = File.ReadAllBytes(fileDialog.FileName);
                        CurrentItem.Images.Add(CreateNewImage(image));
                        ((ItemWithImages)ItemForm).ImagesListBox.ItemsSource = CurrentItem.Images;
                    }
                }
            }
        }

        /// <summary>
        /// Метод, удаляющий изображение текущей записи. В качестве параметра принимает модель изображения. которую нужно удалить.
        /// </summary>
        /// <param name="image">Модель изображения, которую нужно удалить.</param>
        private void DeleteImage(dynamic image)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить изображение?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (image != null)
                {
                    CurrentItem.Images.Remove(image);
                    ((ItemWithImages)ItemForm).ImagesListBox.ItemsSource = CurrentItem.Images;
                    ImageWindowService.TryCloseImageWindow(image);
                }
            }
        }

        /// <summary>
        /// Абстрактный метод, создающий модель изображения для текущей записи раздела.
        /// В качетсве параметра принимает само изображение.
        /// </summary>
        /// <param name="image">Изображение, для которого нужно создать модель.</param>
        /// <returns>Модель изображения.</returns>
        protected abstract dynamic CreateNewImage(byte[] image);

        /// <summary>
        /// Абстрактный метод, выполняющий загрузку изображений для текущей записи.
        /// </summary>
        public abstract void LoadCurrentItemImages();

        /// <summary>
        /// Метод, который создаёт и показывает окно работы с изображением, если для текущего изображения оно ещё не создано.
        /// В качестве параметра принимает режим работы с изображением, по умолчанию - просмотр с возможностью удаления.
        /// Если режим работы с текущей записью - просмотр, то и изображение должно быть только для просмотра.
        /// </summary>
        /// <param name="imageFormMode">Режим работы с изображением.</param>
        public void TryShowImageWindow(ImageWindowMode imageFormMode = ImageWindowMode.Delete)
        {
            CurrentImage = ((ItemWithImages)ItemForm).ImagesListBox.SelectedItem;
            if (_itemFormMode == ItemFormMode.Read)
            {
                imageFormMode = ImageWindowMode.Read;
            }
            if (CurrentImage != null && !ImageWindowService.IsExistImageWindow(CurrentImage))
            {
                ImageWindow imageForm = ImageWindowService.TryCreateItemForm(this, imageFormMode);
                imageForm.Show();
            }
        }

        /// <summary>
        /// Метод копирования изображения в буфер обмена.
        /// </summary>
        /// <param name="img"></param>
        public void CopyImage(Image img)
        {
            Clipboard.SetImage(img.Source as BitmapImage);
        }
    }
}
