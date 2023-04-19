using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WpfApp1.Views;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views.Orders.OrdersReadyToReceive;
using System.Windows;

namespace WpfApp1.ViewModels.Orders
{
    internal class OrdersReadyToReceiveViewModel : SectionWidgetWithImagesViewModel
    {
        private OrdersReadyToReceiveItemWithImages _itemForm;
        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as OrdersReadyToReceiveItemWithImages;
        }

        private ObservableCollection<dynamic> _sectionData;
        public override ObservableCollection<dynamic> SectionData
        {
            get => _sectionData;
            set => _sectionData = value;
        }

        private dynamic _currentItem;
        public override dynamic? CurrentItem
        {
            get => _currentItem;
            set => _currentItem = value;
        }

        private ProductImage _image;
        public override dynamic CurrentImage 
        {
            get => _image;
            set => _image = value;
        }

        private OrderService _orderService;

        public List<Models.Users> Users { get; set; }
        public List<Models.Products> Products { get; set; }
        public List<Models.Storages> PickUpPoints { get; set; }
        public List<OrderStatuses> Statuses { get; set; }

        public OrdersReadyToReceiveViewModel(SectionWidget sectionWidget) : base(sectionWidget) {
            _orderService = App.OrderService;
            UpdateSectionData();
        }

        protected override void MakeCurrentItemEmpty()
        {
            _currentItem = new OrderHistory();
        }

        protected override void CreateNewItemForm()
        {
            Users = App.Context.Users.ToList();
            Products = App.Context.Products.ToList();
            PickUpPoints = App.StorageService.GetPickUpPoints();
            Statuses = _orderService.GetStatusesForReadyToRecieveOrder();
            _itemForm = new OrdersReadyToReceiveItemWithImages(this);
        }

        protected override void AddCurrentItem()
        {
            throw new NotImplementedException();
        }

	    protected override void DeleteCurrentItem()
        {
            throw new NotImplementedException();
        }

        public override void UpdateSectionData()
        {
            _sectionData = _orderService.GetOrdersReadyToReceive();
        }

        protected override string GetErrors()
        {
            StringBuilder errorBuilder = new StringBuilder();

            return errorBuilder.ToString();
        }

        protected override dynamic CreateNewImage(byte[] image)
        { 
            ProductImage productImage = new();
            productImage.ProductImage1 = image;
            return productImage;
        }

        public override void LoadCurrentItemImages()
        {
            CurrentItemFromContext = _orderService.GetOrderHistoryWithProductImages(CurrentItem);
            CurrentItem = CurrentItemFromContext.Clone();
        }

        protected override void Update()
        {
            CurrentItemFromContext.Copy(CurrentItem);
            var entry = App.Context.Entry(CurrentItemFromContext);
            try
            {
                _orderService.ChangeOrderStatus(CurrentItemFromContext);
                ItemForm.Close();
                UpdateItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Изменение записи завершилось ошибкой");
            }
            finally
            {
                entry.Reload();
                MakeCurrentItemEmpty();
            }
            UpdateItems();
        }
    }
}

