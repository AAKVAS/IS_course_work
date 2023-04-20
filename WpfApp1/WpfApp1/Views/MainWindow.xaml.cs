using System.Windows;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    /// <summary>
    /// Представление основного окна приложения.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Конструктор основного окна MainWindow.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик события Loaded. Устанаваливает к качестве контекста данных новый экземпляр модели представления MainWindowViewModel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new MainWindowViewModel(this);
        }

        /// <summary>
        /// Метод, закрывающий вкладку открытого в данный момент раздела.
        /// </summary>
        public void CloseCurrentSection()
        {
            if (!mainTabControl.HasItems || mainTabControl.SelectedItem == null) return;

            mainTabControl.Items.RemoveAt(mainTabControl.SelectedIndex);
        }

        /// <summary>
        /// Обработчик события Closing. Закрывает все окна в приложении.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window != this)
                {
                    window.Close();
                }
            }
        }
    }
}
