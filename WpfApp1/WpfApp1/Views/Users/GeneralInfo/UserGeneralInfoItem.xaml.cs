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
using WpfApp1.ViewModels;
using WpfApp1.Views;

namespace WpfApp1.Views.Users.GeneralInfo
{
    /// <summary>
    /// Логика взаимодействия для UserGeneralInfoItem.xaml
    /// </summary>
    public partial class UserGeneralInfoItem : ItemForm
    {
        public UserGeneralInfoItem(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            DataContext = _sectionWidgetViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected override void SetFormModeToInsert()
        {
            dataActionButton.Visibility = Visibility.Visible;
            dataActionButton.Content = "Сохранить";
        }

        protected override void SetFormModeToUpdate()
        {
            dataActionButton.Visibility = Visibility.Visible;
            dataActionButton.Content = "Изменить";
        }

        protected override void SetFormModeToRead()
        {
            dataActionButton.Visibility = Visibility.Collapsed;
            DisableAllInputs();
        }

        private void DisableAllInputs()
        {
            tb_lastname.IsReadOnly = true;
            tb_firstname.IsReadOnly = true;
            tb_patronymic.IsReadOnly = true;
            tb_phone_number.IsReadOnly = true;
            tb_birthday.IsReadOnly = true;
            tb_sex.IsReadOnly = true;
            tb_email.IsReadOnly = true;
            tb_country.IsReadOnly = true;
        }

    }
}
