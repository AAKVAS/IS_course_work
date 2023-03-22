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
    public enum FilterTypes
    {
        All,
        Equals,
        NotEquals,
        LessThan,
        MoreThan,
        Contains
    }

    public class FilterService
    {
        private class FilterCondition
        {
            public FilterCondition(FilterTypes type, string value)
            {
                Type = type;
                Value = value;
            }

            public FilterTypes Type { get; set; }
            public string Value { get; set; }
        }

        private DataGrid _dataGrid;
        private Dictionary<DataGridColumnHeader, FilterCondition> _filterConditions;
        private SectionWidgetViewModel _viewModel;

        public FilterService(SectionWidgetViewModel viewModel)
        {
            _viewModel = viewModel;
            _dataGrid = _viewModel.SectionWidget.DataGrid;
            _filterConditions = new Dictionary<DataGridColumnHeader, FilterCondition>();
        }

        public void Filter(FilterTypes filterType, DataGridColumnHeader columnHeader, string value)
        {
            if (filterType == FilterTypes.All)
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

        public void UseFilters()
        {
            _viewModel.UpdateSectionData();
            ObservableCollection<dynamic> items = _viewModel.SectionData;

            foreach (var filterCondition in _filterConditions)
            {
                DataGridColumnHeader columnHeader = filterCondition.Key;
                string propertyName = GetPropertyNameByHeader(columnHeader);
                items = new ObservableCollection<dynamic>(items.Where(item => Condition(item, propertyName, filterCondition.Value)));
            }
            _viewModel.SectionData = items;
            _dataGrid.ItemsSource = _viewModel.SectionData;
        }

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
                    return property?.GetValue(item).ToString().Contains(filterCondition.Value.ToString()) ?? false;
                default:
                    return true;
            }
        }

        public PropertyInfo GetPropertyInfo(DataGridColumnHeader columnHeader)
        {
            string propertyName = GetPropertyNameByHeader(columnHeader);
            Type itemType = _viewModel.CurrentItem.GetType();
            return itemType.GetProperty(propertyName);      
        }

        private string GetPropertyNameByHeader(DataGridColumnHeader columnHeader)
        {
            var column = _dataGrid.Columns.Single(c => c.Header.ToString() == columnHeader.Content.ToString());

            if (column != null)
            {
                string columnName = column.Header.ToString();
                if (_viewModel.SectionWidget.HeadersProperties.ContainsKey(columnName))
                {
                    return _viewModel.SectionWidget.HeadersProperties[columnName];
                }
            }
            throw new Exception("Столбец для фильтрации не найден!");
        }

        private bool Equals(object item, PropertyInfo property, object value)
        {
            if (IsDate(property))
            {
                DateTime temp = Convert.ToDateTime(value);
                return Convert.ToDateTime(property?.GetValue(item)) == temp;
            }
            return property?.GetValue(item).ToString() == value.ToString();
        }
        private bool NotEquals(object item, PropertyInfo property, object value)
        {
            if (IsDate(property))
            {
                DateTime temp = Convert.ToDateTime(value);
                return Convert.ToDateTime(property?.GetValue(item)) != temp;
            }
            return property?.GetValue(item).ToString() != value.ToString();
        }
        private bool LessThan(object item, PropertyInfo property, object value)
        {
            if (IsNumeric(property))
            {
                return Convert.ToDouble(property?.GetValue(item)) < Convert.ToDouble(value);
            }
            if (IsDate(property))
            {
                return Convert.ToDateTime(property?.GetValue(item)) < Convert.ToDateTime(value);
            }
            return String.Compare(property?.GetValue(item).ToString(), value.ToString()) < 0;
        }
        private bool MoreThan(object item, PropertyInfo property, object value)
        {
            if (IsNumeric(property))
            {
                return Convert.ToDouble(property?.GetValue(item)) > Convert.ToDouble(value);
            }
            if (IsDate(property))
            {
                return Convert.ToDateTime(property?.GetValue(item)) > Convert.ToDateTime(value);
            }
            return String.Compare(property?.GetValue(item).ToString(), value.ToString()) > 0;
        }

        public void ShowFilterWindow(DataGridColumnHeader columnHeader)
        {
            if (IsDate(GetPropertyInfo(columnHeader)))
            {
                FilterWindow filterWindow = new DataFilterWindow(this, columnHeader);
                filterWindow.Show();
            }
            else
            {
                FilterWindow filterWindow = new StringFilterWindow(this, columnHeader);
                filterWindow.Show();
            }
        }

        public bool IsNumeric(PropertyInfo? property)
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
        public bool IsDate(PropertyInfo? property)
        {
            TypeCode typeCode = Type.GetTypeCode(property.PropertyType);
            return typeCode == TypeCode.DateTime;
        }

        public string GetFilterValueByColumn(DataGridColumnHeader columnHeader)
        {
            FilterCondition filterCondition = GetFilterConditionByColumn(columnHeader);
            return filterCondition != null ? filterCondition.Value : "";
        }

        public FilterTypes GetFilterTypeByColumn(DataGridColumnHeader columnHeader)
        {
            FilterCondition filterCondition = GetFilterConditionByColumn(columnHeader);
            return filterCondition != null ? filterCondition.Type : FilterTypes.All;
        }

        private FilterCondition GetFilterConditionByColumn(DataGridColumnHeader columnHeader)
        {
            FilterCondition filterCondition;
            _filterConditions.TryGetValue(columnHeader, out filterCondition);
            return filterCondition;
        }
    }
}
