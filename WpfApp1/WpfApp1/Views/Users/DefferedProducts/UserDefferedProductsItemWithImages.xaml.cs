using System.Windows;
using System.Windows.Controls;
using WpfApp1.Services;
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
            _viewModel.LoadCurrentItemImages();
            if (_viewModel.CurrentItem.Product != null)
            {
                lbImages.ItemsSource = _viewModel.CurrentItem.Product.Images;
            }
        }

        public override ListBox ListBox 
        { 
            get => lbImages;
            set => lbImages = value; 
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            TryCloseForm();
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
            _viewModel.TryShowImageWindow(ImageWindowMode.Read);
        }

        private void cbProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.LoadDefferedProductImages();
            if (_viewModel.CurrentItem.Product != null)
            {
                lbImages.ItemsSource = _viewModel.CurrentItem.Product.Images;
            }
        }
    }
}