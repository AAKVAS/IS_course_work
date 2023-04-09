using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using WpfApp1.Services;

namespace WpfApp1.Models.DTO
{
    [PrimaryKey(nameof(OrderId), nameof(StatusChangedAt), nameof(WorkerIdNotNullable))]
    public class OrderHistoryDTO : ICopied<OrderHistoryDTO>
    {
        private static OrderService _orderService = App.OrderService;

        private int _orderId;
        public int OrderId 
        { 
            get 
            {
                return _orderId; 
            }
            set 
            { 
                _orderId = value;
                Orders order = _orderService.GetOrderByOrderId(_orderId);
                ProductId = order?.ProductId ?? 0;
            }
        }

        private DateTime? _statusChangedAt;
        public DateTime StatusChangedAt
        {
            get
            {
                return _statusChangedAt ?? DateTime.Now;
            }
            set
            {
                _statusChangedAt = value;
            }
        }

        public bool? IsLastStatus { get; set; }

        public int StatusId { get; set; }
        public string? StatusDescription { get; set; } = null!;

        public int StorageId { get; set; }
        public string? Country { get; set; } 
        public string? FederalSubject { get; set; }
        public string? Locality { get; set; } 
        public string? Street { get; set; } 
        public string? HouseNumber { get; set; } 
        public int? StorageType { get; set; }
        public string? StorageTitle { get; set; }

        public int ProductId { get; set; }
        public string? ProductTitle { get; set; }

        [NotMapped]
        public int? WorkerId { get; set; }
        public string? WorkerLastname { get; set; } 
        public string? WorkerFirstname { get; set; }
        public string? WorkerPatronymic { get; set; }
        public string? WorkerPost { get; set; }

        private int WorkerIdNotNullable
        {
            get
            {
                return WorkerId ?? 0;
            }
            set
            {
                if (value == 0)
                {
                    WorkerId = null;
                }
                else
                {
                    WorkerId = value;
                }
            }
        }

        public OrderHistoryDTO Clone()
        {
            OrderHistoryDTO orderHistoryDTO = new();
            orderHistoryDTO.OrderId = OrderId;
            orderHistoryDTO.StatusChangedAt = StatusChangedAt;
            orderHistoryDTO.IsLastStatus = IsLastStatus;
            orderHistoryDTO.StatusId = StatusId;
            orderHistoryDTO.StatusDescription = StatusDescription;
            orderHistoryDTO.StorageId = StorageId;
            orderHistoryDTO.Country = Country;
            orderHistoryDTO.FederalSubject = FederalSubject;
            orderHistoryDTO.Locality = Locality;
            orderHistoryDTO.Street = Street;
            orderHistoryDTO.HouseNumber = HouseNumber;
            orderHistoryDTO.StorageType = StorageType;
            orderHistoryDTO.StorageTitle = StorageTitle;
            orderHistoryDTO.ProductId = ProductId;
            orderHistoryDTO.ProductTitle = ProductTitle;
            orderHistoryDTO.WorkerId = WorkerId;
            orderHistoryDTO.WorkerLastname = WorkerLastname;
            orderHistoryDTO.WorkerFirstname = WorkerFirstname;
            orderHistoryDTO.WorkerPatronymic = WorkerPatronymic;
            orderHistoryDTO.WorkerPost = WorkerPost;
            return orderHistoryDTO;
        }

        public void Copy(OrderHistoryDTO orderHistoryDTO)
        {
            OrderId = orderHistoryDTO.OrderId;
            StatusChangedAt = orderHistoryDTO.StatusChangedAt;
            IsLastStatus = orderHistoryDTO.IsLastStatus;
            StatusId = orderHistoryDTO.StatusId;
            StatusDescription = orderHistoryDTO.StatusDescription;
            StorageId = orderHistoryDTO.StorageId;
            Country = orderHistoryDTO.Country;
            FederalSubject = orderHistoryDTO.FederalSubject;
            Locality = orderHistoryDTO.Locality;
            Street = orderHistoryDTO.Street;
            HouseNumber = orderHistoryDTO.HouseNumber;
            StorageType = orderHistoryDTO.StorageType;
            StorageTitle = orderHistoryDTO.StorageTitle;
            ProductId = orderHistoryDTO.ProductId;
            ProductTitle = orderHistoryDTO.ProductTitle;
            WorkerId = orderHistoryDTO.WorkerId;
            WorkerLastname = orderHistoryDTO.WorkerLastname;
            WorkerFirstname = orderHistoryDTO.WorkerFirstname;
            WorkerPatronymic = orderHistoryDTO.WorkerPatronymic;
            WorkerPost = orderHistoryDTO.WorkerPost;
        }


    }
}
