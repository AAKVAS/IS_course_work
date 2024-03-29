﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfApp1.Models
{
    /// <summary>
    /// Модель, описывающая пользователя Интернет-магазина.
    /// </summary>
    public partial class Users : ICopied<Users>
    {
        public int Id { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        private DateTime? _birthday { get; set; }
        public DateTime Birthday 
        { 
            get
            {
                return _birthday ?? DateTime.Now;
            }
            set
            {
                _birthday = value;
            }
        }

        public string Email { get; set; } = null!;
        public int? OrderCode { get; set; }
        public bool IsMale { get;
            set; 
        }
        public int? CountryId { get; set; }

        [NotMapped]
        public string Gender
        {
            get
            {
                return IsMale ? "мужчина" : "женщина";
            }
            set
            {
                IsMale = value == "мужчина";
            }
        }

        [NotMapped]
        public bool IsFemale
        {
            get
            {
                return !IsMale;
            }
        }

        public virtual Countries Country { get; set; }
        public virtual ICollection<Cards> Cards { get; set; }
        public virtual ICollection<DeferredProducts> DeferredProducts { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }

        public Users()
        {
            Cards = new HashSet<Cards>();
            DeferredProducts = new HashSet<DeferredProducts>();
            Orders = new HashSet<Orders>();
        }

        public Users Clone()
        {
            Users user = new Users();
            user.Id = Id;
            user.Firstname = Firstname;
            user.Lastname = Lastname;
            user.Patronymic = Patronymic;
            user.PhoneNumber = PhoneNumber;
            user.Birthday = Birthday;
            user.Email = Email;
            user.OrderCode = OrderCode;
            user.IsMale = IsMale;
            user.CountryId = CountryId;
            user.Country = Country;
            user.Cards = new List<Cards>(Cards);
            user.DeferredProducts = new List<DeferredProducts>(DeferredProducts);
            user.Orders = new List<Orders>(Orders);
            return user;
        }

        public void Copy(Users user)
        {
            Id = Id;
            Firstname = user.Firstname;
            Lastname = user.Lastname;
            Patronymic = user.Patronymic;
            PhoneNumber = user.PhoneNumber;
            Birthday = user.Birthday;
            Email = user.Email;
            OrderCode = user.OrderCode;
            IsMale = user.IsMale;
            CountryId = user.CountryId;
            Country = user.Country;
            Cards = user.Cards;
            DeferredProducts = user.DeferredProducts;
            Orders = user.Orders;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj is not Users)
            {
                return false;
            }
            return (obj as Users).Id == Id;
        }
    }
}