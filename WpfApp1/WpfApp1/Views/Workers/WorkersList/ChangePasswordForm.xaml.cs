using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.ViewModels.Workers;

namespace WpfApp1.Views.Workers.WorkersList
{
    /// <summary>
    /// Логика взаимодействия для ChangePasswordForm.xaml
    /// </summary>
    public partial class ChangePasswordForm : Window
    {
        public ChangePasswordForm(WorkersListViewModel workersListSectionWidget)
        {
            InitializeComponent();
            DataContext = workersListSectionWidget;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
