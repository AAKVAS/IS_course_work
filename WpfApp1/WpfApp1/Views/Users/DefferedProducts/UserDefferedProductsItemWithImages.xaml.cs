using System.Windows;
using System.Windows.Controls;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Users;

namespace WpfApp1.Views.Users.DefferedProducts
{
    public partial class UserDefferedProductsItemWithImages : ItemWithImages
    {
        private UserDefferedProductsViewModel _viewModel;

        public UserDefferedProductsItemWithImages(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            _viewModel = (UserDefferedProductsViewModel)_sectionWidgetViewModel;
            DataContext = _viewModel;
            cbUser.ItemsSource = _viewModel.Users;
            cbProduct.ItemsSource = _viewModel.Products;
            _viewModel.LoadCurrentItemImages();
            lbImages.ItemsSource = _viewModel.CurrentItemImages;
        }

        public override ListBox ListBox 
        { 
            get => lbImages;
            set => lbImages = value; 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected override void SetFormModeToInsert()
        {
            btnDataAction.Visibility = Visibility.Visible;
            btnDataAction.Content = "Сохранить";
        }

        protected override void SetFormModeToUpdate()
        {
            btnDataAction.Visibility = Visibility.Visible;
            btnDataAction.Content = "Изменить";
        }

        protected override void SetFormModeToRead()
        {
            btnDataAction.Visibility = Visibility.Collapsed;
            DisableAllInputs();
        }

        private void DisableAllInputs()
        {
            cbUser.IsEnabled = false;
            cbProduct.IsEnabled = false;
        }

        private void lbImages_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ((SectionWidgetWithImagesViewModel)_sectionWidgetViewModel).TryShowImageForm(lbImages.SelectedItem, Services.ImageFormMode.Read);
        }

        private void cbProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.LoadCurrentItemImages();
            lbImages.ItemsSource = _viewModel.CurrentItemImages;
        }
    }
}