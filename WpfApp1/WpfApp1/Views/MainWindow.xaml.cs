﻿using System.Windows;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new MainWindowViewModel(this);
        }

        public void CloseCurrentSection()
        {
            if (!mainTabControl.HasItems || mainTabControl.SelectedItem == null) return;

            mainTabControl.Items.RemoveAt(mainTabControl.SelectedIndex);
        }

    }
}
