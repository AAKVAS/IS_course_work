// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WpfApp1.Models
{
    public partial class Workers
    {
        public Workers()
        {
            StorageWorkerShifts = new HashSet<StorageWorkerShifts>();
            OrderHistory = new HashSet<OrderHistory>();
        }

        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Patronymic { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirthday { get; set; }
        public int? PostId { get; set; }
        public byte[] WorkerPassword { get; set; }
        public bool? IsMale { get; set; }
        public string WorkerLogin { get; set; }

        public virtual Posts Post { get; set; }
        public virtual ICollection<StorageWorkerShifts> StorageWorkerShifts { get; set; }

        public virtual ICollection<OrderHistory> OrderHistory { get; set; }
    }
}