using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using WpfApp1.ViewModels;
using WpfApp1.Views.Components;

namespace WpfApp1.Services
{
    /// <summary>
    /// Перечисление, описыващее вид фильтрации.
    /// Виды фильтрации:
    /// Без фильтра;
    /// Равно;
    /// Не равно;
    /// Меньше чем;
    /// Больше чем;
    /// Содержит.
    /// </summary>
    public enum FilterTypes
    {
        WithoutFilter,
        Equals,
        NotEquals,
        LessThan,
        MoreThan,
        Contains
    }

    /// <summary>
    /// Класс, предоставляющий функционал для фильтрации данных в таблице представления SectionWidget.
    /// Основной принцип фильтрации основан на рефлексии.
    /// </summary>
    public class FilterService
    {
        /// <summary>
        /// Класс, представляющий собой структуру данных, которая хранит вид фильтрации и фильтруемое значение.
        /// </summary>
        private class FilterCondition
        {
            /// <summary>
            /// Вид фильтрации.
            /// </summary>
            public FilterTypes Type { get; set; }

            /// <summary>
            /// Фильтруемое значение.
            /// </summary>
            public string Value { get; set; }

            /// <summary>
            /// Конструктор класса FilterCondition, принимает в качестве параметров вид фильтрации и значение для фильтрации.
            /// </summary>
            /// <param name="type">Вид фильтрации.</param>
            /// <param name="value">Фильтруемое значение.</param>
            public FilterCondition(FilterTypes type, string value)
            {
                Type = type;
                Value = value;
            }
        }

        /// <summary>
        /// Ссылка на таблицу представления SectionWidget
        /// </summary>
        private DataGrid _dataGrid;

        /// <summary>
        /// Словарь, хранящий в качестве ключа заголовок столбца таблицы, в котором будут фильтроваться значения, в качестве значения по ключу в словаре выступает объект класса FilterCondition, хранящий вид фильтрации и фильтруемое значение.
        /// </summary>
        private Dictionary<DataGridColumnHeader, FilterCondition> _filterConditions;

        /// <summary>
        /// Ссылка на модель представления раздела.
        /// </summary>
        private SectionWidgetViewModel _viewModel;

        /// <summary>
        /// Конструктор класса FilterService. В качестве параметра принимает в себя модель представления раздела.
        /// </summary>
        /// <param name="viewModel">Модель представления раздела.</param>
        public FilterService(SectionWidgetViewModel viewModel)
        {
            _viewModel = viewModel;
            _dataGrid = _viewModel.SectionWidget.DataGrid;
            _filterConditions = new Dictionary<DataGridColumnHeader, FilterCondition>();
        }

        /// <summary>
        /// Метод, устанавливающий фильтр для столбца в таблице раздела. В качестве параметров принимает в себя вид фильтрации, заголовок столбца таблицы, значение для фильтрации.
        /// Если в качестве вида фильтрации выбрано "WithoutFilter" (без фильтра), то фильтры по данному столбцу удаляются.
        /// </summary>
        /// <param name="filterType">Вид фильтрации.</param>
        /// <param name="columnHeader">Заголовок столбца таблицы.</param>
        /// <param name="value">Фильтруемое значение.</param>
        public void SetFilter(FilterTypes filterType, DataGridColumnHeader columnHeader, string value)
        {
            if (filterType == FilterTypes.WithoutFilter)
            {
                if (_filterConditions.ContainsKey(columnHeader))
                {
                    _filterConditions.Remove(columnHeader);
                }
            }
            else
            {
                _filterConditions[columnHeader] = new FilterCondition(filterType, value);
            }
            UseFilters();
        }


        /// <summary>
        /// Метод, фильтрующий значения в таблице раздела. 
        /// Проходит по всем фильтрам, берёт заголовок столбца и определяет свойство модели в таблице раздела. 
        /// Метод проверяет, если свойство является составным, например, у объекта класса Worker - Worker.Post.Title, то выполняется алгоритм фильтрации составного свойства.
        /// Иначе выполняется алгоритм фильтрации для простого свойства.
        /// </summary>
        public void UseFilters()
        {
            _viewModel.UpdateSectionData();
            ObservableCollection<dynamic> items = _viewModel.SectionData;

            foreach (var filterCondition in _filterConditions)
            {
                DataGridColumnHeader columnHeader = filterCondition.Key;
                string propertyName = GetPropertyNameByHeader(columnHeader);
                if (IsPropertyComposite(propertyName))
                {
                    //определение конечного свойства записи раздела
                    string finalPropertyName = propertyName.Split('.').Last();
                    items = new ObservableCollection<dynamic>(items
                        .Where(item => Condition(GetPenultimateProperty(item, propertyName), finalPropertyName, filterCondition.Value)));
                }
                else
                {
                    items = new ObservableCollection<dynamic>(items.Where(item => Condition(item, propertyName, filterCondition.Value)));
                }
            }
            _viewModel.SectionData = items;
            _dataGrid.ItemsSource = _viewModel.SectionData;
        }

        /// <summary>
        /// Метод, который проверяет, удовлетворяет ли условию фильтрации значение свойства объекта в таблице раздела.
        /// В качетсве параметра принимет запись таблицы раздела, имя свойства для фильтрации, условие фильтрации.
        /// Принцип основан на рефлексии.
        /// Определяется тип объекта, после извлекается информация о свойстве.
        /// Определяется тип фильтрации, на основе чего выбирается алгоритм фильтрации.
        /// </summary>
        /// <param name="item">Объект из таблицы раздела, чьё свойство будет фильтроваться.</param>
        /// <param name="propertyName">Название фильтруемого свойства.</param>
        /// <param name="filterCondition">Условие фильтрацию.</param>
        /// <returns>Истина, если условие фильтрации пройден.</returns>
        private bool Condition(object item, string propertyName, FilterCondition filterCondition)
        {
            Type itemType = item.GetType();
            var property = itemType.GetProperty(propertyName);
            switch (filterCondition.Type)
            {
                case FilterTypes.Equals:
                    return Equals(item, property, filterCondition.Value);
                case FilterTypes.NotEquals:
                    return NotEquals(item, property, filterCondition.Value);
                case FilterTypes.LessThan:
                    return LessThan(item, property, filterCondition.Value);
                case FilterTypes.MoreThan:
                    return MoreThan(item, property, filterCondition.Value);
                case FilterTypes.Contains:
                    return property?.GetValue(item)?.ToString()?.Contains(filterCondition.Value.ToString()) ?? (filterCondition.Value.ToString() == "");
                default:
                    return true;
            }
        }

        /// <summary>
        /// Метод определяющий, является ли свойство составным.
        /// Например, свойство объекта Worker Id - простое, Post.Tittle - составное.
        /// </summary>
        /// <param name="propertyName">Название свойства.</param>
        /// <returns>Истина, если свойство составное.</returns>
        private bool IsPropertyComposite(string propertyName)
        {
            return propertyName.Contains('.');
        }

        /// <summary>
        /// Метод, получающий название свойства модели по заголовку таблицы раздела. 
        /// </summary>
        /// <param name="columnHeader">Заголовок таблицы раздела.</param>
        /// <returns>Название свойства модели.</returns>
        /// <exception cref="Exception"></exception>
        private string GetPropertyNameByHeader(DataGridColumnHeader columnHeader)
        {
            var column = _dataGrid.Columns.Single(c => c.Header.ToString() == columnHeader.Content.ToString());

            if (column != null)
            {
                string columnName = column.Header?.ToString() ?? "";
                if (_viewModel.SectionWidget.HeadersProperties.ContainsKey(columnName))
                {
                    return _viewModel.SectionWidget.HeadersProperties[columnName];
                }
            }
            throw new Exception("Столбец для фильтрации не найден!");
        }


        /// <summary>
        /// Метод, получающий сведения о данных в столбце по его заголовку.
        /// В качестве параметра принимает заголовок столбца.
        /// </summary>
        /// <param name="columnHeader">Заголовок столбца.</param>
        /// <returns>Сведения о данных в столбце.</returns>
        public PropertyInfo GetPropertyInfo(DataGridColumnHeader columnHeader)
        {
            string propertyName = GetPropertyNameByHeader(columnHeader);
            if (IsPropertyComposite(propertyName))
            {
                return GetPropertyInfoForCompositeProperty(propertyName);
            }
            else
            {
                Type itemType = _viewModel.CurrentItem.GetType();
                return itemType.GetProperty(propertyName);
            }
        }

        /// <summary>
        /// Метод, получающий сведения о составном свойстве записи таблицы раздела.
        /// В качестве параметра принимает название составного свойства.
        /// Принцип действия основан на рефлексии.
        /// Название составного свойства разбивается на массив строк.
        /// Метод определяет тип объекта, хранимого в таблице раздела и извлекает информацию о свойстве по его имени.
        /// Операция проводится циклично, пока не будет достигнуто последнее свойство объекта.
        /// </summary>
        /// <param name="propertyName">Название составного свойства.</param>
        /// <returns>Сведения о свойстве записи таблицы раздела.</returns>
        private PropertyInfo GetPropertyInfoForCompositeProperty(string propertyName)
        {
            string[] properties = propertyName.Split('.');
            Type itemType = _viewModel.CurrentItem.GetType();
            PropertyInfo? property = itemType.GetProperty(properties[0]);

            for (int i = 1; i < properties.Length; i++)
            {
                itemType = property?.PropertyType;
                property = itemType.GetProperty(properties[i]);
            }
            return property;
        }

        /// <summary>
        /// Метод, извлекающий значение предпоследнего свойства объекта из составного свойства.
        /// Принимает в качестве параметров объект и имя составного свойства.
        /// Принцип действия основан на рефлексии.
        /// Название составного свойства разбивается на массив строк.
        /// Метод определяет тип объекта, хранимого в таблице раздела, извлекает информацию о свойстве по его имени и берёт значение этого свойства.
        /// Операция проводится циклично, пока не будет достигнуто предпоследнее свойство объекта.
        /// </summary>
        /// <param name="item">Объект, у которого нужно извлечь значение препдпоследнего свойства.</param>
        /// <param name="propertyName">Название составного свойства.</param>
        /// <returns></returns>
        private object GetPenultimateProperty(object item, string propertyName)
        {
            string[] properties = propertyName.Split('.');
            Type itemType = _viewModel.CurrentItem.GetType();
            PropertyInfo? property = itemType.GetProperty(properties[0]);
            object obj = item;

            for (int i = 1; i < properties.Length; i++)
            {
                obj = property?.GetValue(obj);
                itemType = property?.PropertyType;
                property = itemType.GetProperty(properties[i]);
            }
            return obj;
        }

        /// <summary>
        /// Метод, определяющий, равно ли свойство объекта фильтруемому значению.
        /// Принимает в качестве параметров объект из таблицы раздела, сведения о свойстве, фильтруемое значение.
        /// Если свойство хранит дату, то оно и фильтруемое значение конвертируются к дате, иначе сравниваются строки.
        /// </summary>
        /// <param name="item">Запись таблицы раздела.</param>
        /// <param name="property">Сведения о свойстве объекта.</param>
        /// <param name="value">Фильтруемое значение.</param>
        /// <returns>Истина, если свойство объекта равно фильтруемому значению.</returns>
        private bool Equals(object item, PropertyInfo property, object value)
        {
            if (IsDate(property))
            {
                DateTime temp = Convert.ToDateTime(value);
                return Convert.ToDateTime(property?.GetValue(item)) == temp;
            }
            return (property?.GetValue(item)?.ToString() ?? "") == value.ToString();
        }

        /// <summary>
        /// Метод, определяющий, не равно ли свойство объекта фильтруемому значению.
        /// Принимает в качестве параметров объект из таблицы раздела, сведения о свойстве, фильтруемое значение.
        /// Если свойство хранит дату, то оно и фильтруемое значение конвертируются к дате, иначе сравниваются строки.
        /// </summary>
        /// <param name="item">Запись таблицы раздела.</param>
        /// <param name="property">Сведения о свойстве объекта.</param>
        /// <param name="value">Фильтруемое значение.</param>
        /// <returns>Истина, если свойство объекта не равно фильтруемому значению.</returns>
        private bool NotEquals(object item, PropertyInfo property, object value)
        {
            if (IsDate(property))
            {
                DateTime temp = Convert.ToDateTime(value);
                return Convert.ToDateTime(property?.GetValue(item)) != temp;
            }
            return (property?.GetValue(item)?.ToString() ?? "") != value.ToString();
        }

        /// <summary>
        /// Метод, определяющий, меньше ли свойство объекта фильтруемого значения.
        /// Принимает в качестве параметров объект из таблицы раздела, сведения о свойстве, фильтруемое значение.
        /// Если свойство хранит дату, то оно и фильтруемое значение конвертируются к дате.
        /// Если свойство хранит число, и пользователь ввёл число, то значения конвертируются к числу.
        /// Иначе сравниваются строки.
        /// </summary>
        /// <param name="item">Запись таблицы раздела.</param>
        /// <param name="property">Сведения о свойстве объекта.</param>
        /// <param name="value">Фильтруемое значение.</param>
        /// <returns>Истина, если свойство объекта меньше фильтруемого значения.</returns>
        private bool LessThan(object item, PropertyInfo property, object value)
        {
            if (IsDate(property))
            {
                return Convert.ToDateTime(property?.GetValue(item)) < Convert.ToDateTime(value);
            }
            else if (IsNumeric(property))
            {
                double temp; 
                if (Double.TryParse(value.ToString(), out temp))
                {
                    return Convert.ToDouble(property?.GetValue(item)) < Convert.ToDouble(value);
                }
            }
            return String.Compare((property?.GetValue(item)?.ToString() ?? ""), value.ToString()) < 0;
        }

        /// <summary>
        /// Метод, определяющий, больше ли свойство объекта фильтруемого значения.
        /// Принимает в качестве параметров объект из таблицы раздела, сведения о свойстве, фильтруемое значение.
        /// Если свойство хранит дату, то оно и фильтруемое значение конвертируются к дате.
        /// Если свойство хранит число, и пользователь ввёл число, то значения конвертируются к числу.
        /// Иначе сравниваются строки.
        /// </summary>
        /// <param name="item">Запись таблицы раздела.</param>
        /// <param name="property">Сведения о свойстве объекта.</param>
        /// <param name="value">Фильтруемое значение.</param>
        /// <returns>Истина, если свойство объекта больше фильтруемого значения.</returns>
        private bool MoreThan(object item, PropertyInfo property, object value)
        {
            if (IsDate(property))
            {
                return Convert.ToDateTime(property?.GetValue(item)) > Convert.ToDateTime(value);
            }
            else if (IsNumeric(property))
            {
                double temp;
                if (Double.TryParse(value.ToString(), out temp))
                {
                    return Convert.ToDouble(property?.GetValue(item)) > Convert.ToDouble(value);
                }
            }
            return String.Compare((property?.GetValue(item)?.ToString() ?? ""), value.ToString()) > 0;
        }

        /// <summary>
        /// Метод, проверяющий имеет ли объект тип данных DateTime. В качестве параметра принимает сведения об объекте.
        /// </summary>
        /// <param name="property">Сведения об объекте.</param>
        /// <returns>Истина, если тип данных объекта - DateTime.</returns>
        public static bool IsDate(PropertyInfo? property)
        {
            TypeCode typeCode = Type.GetTypeCode(property.PropertyType);
            return typeCode == TypeCode.DateTime;
        }

        /// <summary>
        /// Метод, проверяющий имеет ли объект числовой тип данных. В качестве параметра принимает сведения об объекте.
        /// </summary>
        /// <param name="property">Сведения об объекте.</param>
        /// <returns>Истина, если тип данных объекта - числовой.</returns>
        public static bool IsNumeric(PropertyInfo? property)
        {
            TypeCode typeCode = Type.GetTypeCode(property.PropertyType);
            switch (typeCode)
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Метод, создающий и вызывающий окно фильтрации. Принимает заголовок столбца таблицы раздела.
        /// Если столбец содержит дату, то создаётся окно фильтрации для даты.
        /// Иначе создаётся столбец фильтрации с текстовым полем ввода.
        /// </summary>
        /// <param name="columnHeader">Заголовок столбца.</param>
        public void ShowFilterWindow(DataGridColumnHeader columnHeader)
        {
            if (IsDate(GetPropertyInfo(columnHeader)))
            {
                FilterWindow filterWindow = new DateFilterWindow(this, columnHeader);
                filterWindow.ShowDialog();
            }
            else
            {
                FilterWindow filterWindow = new StringFilterWindow(this, columnHeader);
                filterWindow.ShowDialog();
            }
        }

        public string GetFilterValueByColumn(DataGridColumnHeader columnHeader)
        {
            FilterCondition filterCondition = GetFilterConditionByColumn(columnHeader);
            return filterCondition != null ? filterCondition.Value : "";
        }

        public FilterTypes GetFilterTypeByColumn(DataGridColumnHeader columnHeader)
        {
            FilterCondition filterCondition = GetFilterConditionByColumn(columnHeader);
            return filterCondition != null ? filterCondition.Type : FilterTypes.WithoutFilter;
        }

        private FilterCondition GetFilterConditionByColumn(DataGridColumnHeader columnHeader)
        {
            FilterCondition filterCondition;
            _filterConditions.TryGetValue(columnHeader, out filterCondition);
            return filterCondition;
        }
    }
}
