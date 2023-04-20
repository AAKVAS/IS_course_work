using System.Windows;
using System.Windows.Controls;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Products;

namespace WpfApp1.Views.Products.Reviews 
{

    public partial class ProductsReviewsItemWithImages : ItemWithImages
    {
        private ProductsReviewsViewModel _viewModel;

        public ProductsReviewsItemWithImages(SectionWidgetViewModel sectionWidgetViewModel) : base(sectionWidgetViewModel)
        {
            InitializeComponent();
            _viewModel = (ProductsReviewsViewModel)_sectionWidgetViewModel;
            DataContext = _viewModel;
            _viewModel.LoadCurrentItemImages();
            lbImages.ItemsSource = _viewModel.CurrentItemFromContext.Images;
        }

        public override ListBox ImagesListBox
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
            tbId.IsReadOnly = true;
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
            tbId.IsReadOnly = true;
            tbReview.IsReadOnly = true;
            tbStars.IsReadOnly = true;
            cbProduct.IsEnabled = false;
            cbUser.IsEnabled = false;
            btnAddImage.IsEnabled = false;
            btnAddImage.Visibility = Visibility.Collapsed;
        }

        private void lbImages_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _viewModel.TryShowImageWindow();
        }
    }
}

