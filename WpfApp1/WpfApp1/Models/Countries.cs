﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WpfApp1.Models
{
    public partial class Countries
    {
        public Countries()
        {
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}