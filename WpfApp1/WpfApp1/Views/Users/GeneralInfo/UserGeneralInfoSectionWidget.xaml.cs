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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Models;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    public partial class UserGeneralInfoSectionWidget : SectionWidget
    {
        protected override Button InsertButton { 
            get => insertButton;
        }
        protected override Button UpdateButton
        {
            get => updateButton;
        }
        protected override Button ReadButton
        {
            get => readButton;
        }
        protected override Button DeleteButton
        {
            get => deleteButton;
        }
        protected override Button PDFButton
        {
            get => toPDFButton;
        }


        public override DataGrid DataGrid
        {
            get => dataGrid;
            set
            {
                dataGrid = value;
            }
        }

        protected override SectionWidgetViewModel ViewModel { get; set; }

        public UserGeneralInfoSectionWidget(Sections section) : base(section)
        {
            InitializeComponent();
            ViewModel = new ViewModels.Users.GeneralInfo.UsersGeneralInfoViewModel(this);
            DataContext = ViewModel;
            DataGrid.ItemsSource = ViewModel.SectionData;            
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            DataContext = ViewModel;
        }
    }
}
