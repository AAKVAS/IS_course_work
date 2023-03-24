using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls.Primitives;
using WpfApp1.Services;
using WpfApp1.Views;
using WpfApp1.Views.Components;

namespace WpfApp1.ViewModels
{
    public abstract class SectionWidgetWithImagesViewModel : SectionWidgetViewModel
    {
        protected RelayCommand? _insertImageCommand;
        protected RelayCommand? _deleteImageCommand;

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
        public RelayCommand DeleteImageCommand
        {
            get
            {
                return _deleteImageCommand ??
                        (_deleteImageCommand = new RelayCommand((object obj) => {
                            DeleteImage();
                        },
                        (obj) => _accessService.HasWorkerRightToDelete(SectionWidget.Section.SectionKey)));
            }
        }

        public abstract dynamic CurrentImage { get; set; }
        public ObservableCollection<dynamic> CurrentItemImages { get; set; }

        public SectionWidgetWithImagesViewModel(SectionWidget sectionWidget) : base(sectionWidget) {}

        private void InsertImage()
        {
            if (CurrentItem.Images.Count == 10)
            {
                MessageBox.Show("Нельзя прикрепить больше 10 файлов!", "Предупреждение!");
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
                        MessageBox.Show("Изображение должно быть не больше 10 МБ!", "Предупреждение!");
                    }
                    else
                    {
                        image = File.ReadAllBytes(fileDialog.FileName);
                        CurrentItem.Images.Add(CreateNewImage(image));
                        App.Context.SaveChanges();
                        LoadCurrentItemImages();
                        ((ItemWithImages)ItemForm).ListBox.ItemsSource = CurrentItemImages;
                        ((ItemWithImages)ItemForm).ListBox.Items.Refresh();
                    }
                }
            }
        }

        protected abstract dynamic CreateNewImage(byte[] image);
        public abstract void LoadCurrentItemImages();

        private void DeleteImage()
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить изображение?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (CurrentImage != null)
                {
                    CurrentItem.Images.Remove(CurrentImage);
                    App.Context.SaveChanges();
                    LoadCurrentItemImages();
                    ((ItemWithImages)ItemForm).ListBox.ItemsSource = CurrentItemImages;
                    ((ItemWithImages)ItemForm).ListBox.Items.Refresh();
                    ImageFormService.TryCloseImageForm(CurrentImage);
                }
            }
        }

        public void TryShowImageForm(object selectedItem)
        {
            CurrentImage = selectedItem;
            if (!ImageFormService.IsExistImageForm(CurrentImage))
            {
                ImageForm imageForm = ImageFormService.TryCreateItemForm(this);
                imageForm.Show();
            }
        }

    }
}
