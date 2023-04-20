using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Services
{
    /// <summary>
    /// Класс, обеспечивающий генерацию PDF-документа на основе данных таблицы раздела.
    /// </summary>
    public class PDFGenerator
    {
        /// <summary>
        /// Путь к PDF-файлу.
        /// </summary>
        private string _filepath;

        /// <summary>
        /// Заголовок таблицы в PDF-документе.
        /// </summary>
        private string _title;

        /// <summary>
        /// Количество столбцов в таблице.
        /// </summary>
        private int _columnCount;   
        
        /// <summary>
        /// Ссылка на таблицу раздела.
        /// </summary>
        private DataGrid _grid;

        /// <summary>
        /// Создаваемый PDF-документ.
        /// </summary>
        private Document _document;

        /// <summary>
        /// Шрифт в PDF-документе.
        /// </summary>
        private Font _font;

        /// <summary>
        /// Таблица в PDF-документе.
        /// </summary>
        private PdfPTable _table;

        /// <summary>
        /// Метод, пытающийся создать PDF-документ.
        /// В качестве параметров принимает заголовок таблицы в создаваемом PDF-документе, а также ссылку на таблицу раздела.
        /// </summary>
        /// <param name="title">Заголовок таблицы.</param>
        /// <param name="dataGrid">Таблица раздела.</param>
        public void TryCreatePDF(string title, DataGrid dataGrid)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF |*.pdf";
            saveFileDialog.FileName = "Отчёт";

            if (saveFileDialog.ShowDialog() ?? false)
            {
                _filepath = saveFileDialog.FileName;
                _grid = dataGrid;
                _title = title;
                try
                {
                    _document = new Document(new Rectangle(1440, 900));
                    SetupDocumentProperties();
                    _document.Open();
                    DrawTable();
                    _document.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось загрузить данные в PDF-файл");
                }
            }
        }

        /// <summary>
        /// Метод, настраивающий PDF-документ, перед записью данных.
        /// </summary>
        private void SetupDocumentProperties()
        {
            //Добавление объекта записи данных в PDF-файл.
            PdfWriter.GetInstance(_document, new FileStream(_filepath, FileMode.Create));

            //Установка в документе шрифта Arial, в целях использования русских символов.
            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");
            BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            _font = new Font(baseFont, 8, Font.NORMAL);
        }

        /// <summary>
        /// Метод, создающий таблицу в PDF-документе.
        /// </summary>
        private void DrawTable()
        {
            _columnCount = _grid.Columns.Count;
            _table = new PdfPTable(_columnCount);
            DrawTitle();
            DrawTableHeader();
            DrawTableItems();
            _document.Add(_table);
        }

        /// <summary>
        /// Метод, создающий заголовок таблицы в PDF-документе.
        /// </summary>
        private void DrawTitle()
        {
            PdfPCell title = new PdfPCell(new Phrase(_title, _font));
            title.HorizontalAlignment = (int)HorizontalAlignment.Center;
            title.Colspan = _columnCount;
            title.Border = 0;
            _table.AddCell(title);
        }

        /// <summary>
        /// Метод, создающий заголовки столбцов таблицы в PDF-документе.
        /// </summary>
        private void DrawTableHeader()
        {
            for (int i = 0; i < _columnCount; i++)
            {
                PdfPCell cell = new PdfPCell(new Phrase(_grid.Columns[i].Header.ToString(), _font));
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                _table.AddCell(cell);
            }
        }

        /// <summary>
        /// Метод, создающий таблицу с данными из раздела.
        /// </summary>
        private void DrawTableItems()
        {
            for (int i = 0; i < _grid.Items.Count; i++)
            {
                for (int j = 0; j < _columnCount; j++)
                {
                    _table.AddCell(new Phrase((_grid.Columns[j].GetCellContent(_grid.Items[i]) as TextBlock)?.Text ?? "", _font));
                }
            }
        }
    }
}
