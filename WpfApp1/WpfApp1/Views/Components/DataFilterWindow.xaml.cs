﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Services;

namespace WpfApp1.Views.Components
{
    /// <summary>
    /// Логика взаимодействия для DataFilterWindow.xaml
    /// </summary>
    public partial class DataFilterWindow : FilterWindow
    {

        public DataFilterWindow(FilterService filterService, DataGridColumnHeader columnHeader) : base(filterService, columnHeader)
        {
            InitializeComponent();
            dpParam.Text = _filterService.GetFilterValueByColumn(_columnHeader);
            SelectFilterType();
        }

        private void SelectFilterType()
        {
            switch (_filterService.GetFilterTypeByColumn(_columnHeader))
            {
                case FilterTypes.All:
                    cbFilterType.SelectedIndex = 0;
                    break;
                case FilterTypes.Equals:
                    cbFilterType.SelectedIndex = 1;
                    break;
                case FilterTypes.NotEquals:
                    cbFilterType.SelectedIndex = 2;
                    break;
                case FilterTypes.MoreThan:
                    cbFilterType.SelectedIndex = 3;
                    break;
                case FilterTypes.LessThan:
                    cbFilterType.SelectedIndex = 4;
                    break;
                default:
                    cbFilterType.SelectedIndex = 0;
                    break;
            }
        }

        protected override void Filter()
        {
            FilterTypes filterTypes = FilterTypes.All;

            switch (cbFilterType.SelectedIndex)
            {
                case 1:
                    filterTypes = FilterTypes.Equals;
                    break;
                case 2:
                    filterTypes = FilterTypes.NotEquals;
                    break;
                case 3:
                    filterTypes = FilterTypes.MoreThan;
                    break;
                case 4:
                    filterTypes = FilterTypes.LessThan;
                    break;
            }
            _filterService.Filter(filterTypes, _columnHeader, dpParam.Text);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            Filter();
        }
    }
}