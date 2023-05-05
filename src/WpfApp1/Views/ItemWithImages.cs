using System.Windows.Controls;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    /// <summary>
    /// Абстрактный класс окна работы с записью раздела с изображениями.
    /// </summary>
    public abstract class ItemWithImages : ItemForm
    {
        /// <summary>
        /// Список изображений записи раздела.
        /// </summary>
        public abstract ListBox ImagesListBox { get; set; }

        /// <summary>
        /// Конструктор класса ItemWithImages, принимающий в качестве параметра ссылку на модель представления раздела.
        /// Вызывает конструктор родительского класса ItemForm.
        /// </summary>
        /// <param name="sectionWidgetViewModel">Ссылка на модель представления раздела.</param>
        protected ItemWithImages(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel) {}
    }
}
