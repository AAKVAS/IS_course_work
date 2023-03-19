using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Services
{
    public class PDFGenerateService
    {
        private static string _filepath;
        private static string _title;
        private static int _columnCount;        
        private static DataGrid _grid;
        private static Document _document;
        private static Font _font;
        private static PdfPTable _table;

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

        private void SetupDocumentProperties()
        {
            _document = new Document();
            PdfWriter.GetInstance(_document, new FileStream(_filepath, FileMode.Create));

            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");
            BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            _font = new Font(baseFont, 8, Font.NORMAL);
        }

        private void DrawTable()
        {
            _columnCount = _grid.Columns.Count;
            _table = new PdfPTable(_columnCount);
            DrawTitle();
            DrawTableHeader();
            DrawTableItems();
            _document.Add(_table);
        }

        private void DrawTitle()
        {
            PdfPCell title = new PdfPCell(new Phrase(_title, _font));
            title.HorizontalAlignment = (int)HorizontalAlignment.Center;
            title.Colspan = _columnCount;
            title.Border = 0;
            _table.AddCell(title);
        }

        private void DrawTableHeader()
        {
            for (int i = 0; i < _columnCount; i++)
            {
                PdfPCell cell = new PdfPCell(new Phrase(_grid.Columns[i].Header.ToString(), _font));
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                _table.AddCell(cell);
            }
        }

        private void DrawTableItems()
        {
            for (int i = 0; i < _grid.Items.Count; i++)
            {
                for (int j = 0; j < _columnCount; j++)
                {
                    _table.AddCell(new Phrase((_grid.Columns[j].GetCellContent(_grid.Items[i]) as TextBlock).Text, _font));
                }
            }
        }
    }
}
