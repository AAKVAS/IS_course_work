using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using WpfApp1.ViewModels;
using WpfApp1.Views;
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
        private struct FilterCondition
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

                var column = _dataGrid.Columns.Single(c => c.Header.ToString() == columnHeader.Content.ToString());
                if (column != null)
                {
                    string columnName = column.Header.ToString();
                    if (_viewModel.SectionWidget.HeadersProperties.ContainsKey(columnName))
                    {
                        string propertyName = _viewModel.SectionWidget.HeadersProperties[columnName];
                        items = new ObservableCollection<dynamic>(items.Where(item => Condition(item, propertyName, filterCondition.Value)));
                    }
                    else
                    {
                        throw new Exception("Столбец для фильтрации не найден!");
                    }
                }
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
                    return property?.GetValue(item).ToString() == filterCondition.Value.ToString();
                case FilterTypes.NotEquals:
                    return property?.GetValue(item).ToString() != filterCondition.Value.ToString();
                case FilterTypes.LessThan:
                    return String.Compare(property?.GetValue(item).ToString(), filterCondition.Value.ToString()) < 0;
                case FilterTypes.MoreThan:
                    return String.Compare(property?.GetValue(item).ToString(), filterCondition.Value.ToString()) > 0;
                case FilterTypes.Contains:
                    return property?.GetValue(item).ToString().Contains(filterCondition.Value.ToString()) ?? false;
                default:
                    return true;
            }
            
        }
    }
}
