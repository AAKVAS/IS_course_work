﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WpfApp1.Models
{
    public partial class Cards
    {
        public int UserId { get; set; }
        public string CardNumber { get; set; }
        public string Validity { get; set; }
        public string CardOwner { get; set; }
        public int? Cvc { get; set; }

        public virtual Users User { get; set; }
    }
}