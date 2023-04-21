using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    /// <summary>
    /// Абстрактный класс, являющийся представлением раздела.
    /// </summary>
    public abstract class SectionWidget : UserControl
    {
        /// <summary>
        /// Кнопка, вызывающая окно вставки записи.
        /// </summary>
        protected abstract Button InsertButton { get; }

        /// <summary>
        ///  Кнопка удаления записи.
        /// </summary>
        protected abstract Button DeleteButton { get; }

        /// <summary>
        /// Кнопка, вызывающая окно изменения записи.
        /// </summary>
        protected abstract Button UpdateButton { get; }

        /// <summary>
        /// Кнопка, вызывающая окно просмотра записи.
        /// </summary>
        protected abstract Button ReadButton { get; }

        /// <summary>
        /// Кнопка создания PDF-документа.
        /// </summary>
        protected abstract Button PDFButton { get; }

        /// <summary>
        /// Модель раздела, для которого существует данного представление.
        /// </summary>
        public Sections Section;

        /// <summary>
        /// Ссылка на модель представления раздела.
        /// </summary>
        public SectionWidgetViewModel ViewModel { get; set; }

        /// <summary>
        /// Словарь хранящий названия столбцов таблицы раздела и названия свойств модели, которые привязаны к этим столбцам.
        /// Используется в FilterService для фильтрации.
        /// </summary>
        public abstract Dictionary<string, string> HeadersProperties { get; }

        /// <summary>
        /// Таблица раздела.
        /// </summary>
        public abstract DataGrid DataGrid { get; set; }

        /// <summary>
        /// Конструктор для класса SectionWidget. Принимает в качестве параметра модель раздела.
        /// </summary>
        /// <param name="section">Модель раздела.</param>
        public SectionWidget(Sections section)
        {
            Section = section;
        }

        /// <summary>
        /// Метод, скрывающий кнопку, вызывающую окно вставки записи.
        /// </summary>
        public void CollapseInsertButton()
        {
            InsertButton.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Метод, скрывающий кнопку, вызывающую окно обновления записи.
        /// </summary>
        public void CollapseUpdateButton()
        {
            UpdateButton.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Метод, скрывающий кнопку удаления записи.
        /// </summary>
        public void CollapseDeleteButton()
        {
            DeleteButton.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Метод, скрывающий кнопку, вызывающую окно просмотра записи.
        /// </summary>
        public void CollapseReadButton()
        {
            ReadButton.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Метод, скрывающий кнопку создания PDF-документа.
        /// </summary>
        public void CollapsePDFButton()
        {
            PDFButton.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Метод, скрывающий все кнопки, кроме кнопки просмотра записи.
        /// </summary>
        public void CollapseAllButtonsExceptReadButton()
        {
            CollapseInsertButton();
            CollapseUpdateButton();
            CollapseDeleteButton();
            CollapsePDFButton();
        }
    }
}
