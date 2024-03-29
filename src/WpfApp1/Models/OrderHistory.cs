﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WpfApp1.Models
{
    /// <summary>
    /// Модель, отображающая изменение статуса доставки.
    /// </summary>
    public partial class OrderHistory : ICopied<OrderHistory>
    {
        public int OrderId { get; set; }
        public DateTime StatusChangedAt { get; set; }
        public int? StatusId { get; set; }
        public int? CurrentStorageId { get; set; }
        public bool? IsLastStatus { get; set; }

        public virtual Storages CurrentStorage { get; set; }
        public virtual Orders Order { get; set; }
        public virtual OrderStatuses Status { get; set; }

        public virtual ICollection<Workers> Worker { get; set; }

        public OrderHistory()
        {
            Worker = new HashSet<Workers>();
        }

        public OrderHistory Clone()
        {
            OrderHistory orderHistory = new();
            orderHistory.OrderId = OrderId;
            orderHistory.StatusChangedAt = StatusChangedAt;
            orderHistory.StatusId = StatusId;
            orderHistory.CurrentStorageId = CurrentStorageId;
            orderHistory.IsLastStatus = IsLastStatus;
            orderHistory.CurrentStorage = CurrentStorage;
            orderHistory.Order = Order;
            orderHistory.Status = Status;
            orderHistory.Worker = new List<Workers>(Worker);
            return orderHistory;
        }

        public void Copy(OrderHistory orderHistory)
        {
            OrderId = orderHistory.OrderId;
            StatusChangedAt = orderHistory.StatusChangedAt;
            StatusId = orderHistory.StatusId;
            CurrentStorageId = orderHistory.CurrentStorageId;
            IsLastStatus = orderHistory.IsLastStatus;
            CurrentStorage = orderHistory.CurrentStorage;
            Order = orderHistory.Order;
            Status = orderHistory.Status;
            Worker = new List<Workers>(orderHistory.Worker);
        }
    }
}