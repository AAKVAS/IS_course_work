using System.Windows;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    /// <summary>
    /// Окно входа в систему.
    /// </summary>
    public partial class Login : Window
    {
        /// <summary>
        /// Конструктор окна входа Login.
        /// </summary>
        public Login()
        {
            InitializeComponent();
            DataContext = new LoginViewModel(this);
        }
    }
}
